using System.ComponentModel;
using System.Diagnostics;
using MewsiferConsole.Common;
using Timer = System.Windows.Forms.Timer;

namespace MewsiferConsole
{
  public partial class MainConsoleForm : Form
  {
    private readonly BindingList<LogEventViewModel> Messages;
    private readonly LogMessageFilterView FilterView;

    private readonly object QueueLock = new();
    private readonly List<LogEvent>[] Queues = { new(), new() };
    private int IncomingIndex = 0;

    private readonly Timer? Timer;

    private bool IsScrolledToEnd
    {
      get
      {
        int firstDisplayed = logTable.FirstDisplayedScrollingRowIndex;
        int lastVisible = firstDisplayed + logTable.DisplayedRowCount(true) - 1;
        return lastVisible == logTable.RowCount - 1;
      }
      set
      {
        if (logTable.RowCount > 0)
        {
          logTable.FirstDisplayedScrollingRowIndex = logTable.RowCount - 1;
        }
      }
    }

    private readonly Dictionary<string, List<int>> IndexRowLookup = new();
    private readonly Dictionary<string, List<int>> ByChannel = new();

    private readonly Count infoCountVal;
    private readonly Count warnCountVal;
    private readonly Count errCountVal;
    private readonly Count verCountVal;

    public MainConsoleForm()
    {
      InitializeComponent();
      Messages = new();
      FilterView = new(Messages);
      logTable.AutoGenerateColumns = false;

      var messageCol = new MessageStackColumn
      {
        HeaderText = "Message",
        Name = "Message",
        DataPropertyName = "Message",
        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
        FillWeight = 200
        
      };
      logTable.Columns.Add(messageCol);
      logTable.AllowUserToResizeRows = false;

      logTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

      logTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      logTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
      logTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

      infoCountVal = new(infoCount, "I");
      warnCountVal = new(warnCount, "W");
      errCountVal = new(errCount, "E");
      verCountVal = new(verCount, "V");

      logTable.DataSource = FilterView;
      logTable.RowsAdded += (_, _) => OnRowsAddedOrRemoved();
      logTable.RowsRemoved += (_, _) => OnRowsAddedOrRemoved();

      logTable.SelectionChanged += (_, evt) => {
        if (logTable.SelectedRows.Count == 1 && logTable.SelectedRows[0].DataBoundItem is LogEventViewModel vm)
          detailView.ViewModel = vm;
        else
          detailView.ViewModel = null;
      };

      tooltip.SetToolTip(infoCount, "Total number of messages with the severity level: info");
      tooltip.SetToolTip(warnCount, "Total number of messages with the severity level: warning");
      tooltip.SetToolTip(errCount, "Total number of messages with the severity level: error");

      tooltip.SetToolTip(shownCount, "Displayed messages / Total message");

      var watch = new Stopwatch();
      watch.Start();

      if (App.MessageSource.IsBounded)
      {
        Messages.RaiseListChangedEvents = false;
        App.MessageSource.Completed += () =>
        {
          BeginInvoke(() =>
          {
            var doneLoading = watch.Elapsed;
            watch.Restart();
            ProcessPendingMessages();
            var doneProcessing = watch.Elapsed;
            watch.Restart();

            Messages.RaiseListChangedEvents = true;
            Messages.ResetBindings();
            FilterView.RemoveFilter();
            RenderCountLabels();
            var doneRendering = watch.Elapsed;

            Console.WriteLine($"   Loading: ${doneLoading}");
            Console.WriteLine($"Processing: ${doneProcessing}");
            Console.WriteLine($" Rendering: ${doneRendering}");
          });
        };
        TailToggle.Checked = false;
        TailToggle.Enabled = false;
        Clear.Enabled = false;
      }
      else
      {
        Timer = new()
        {
          Interval = 33
        };

        Timer.Tick += (_, _) => ProcessPendingMessages();
        Timer.Start();
      }

      App.MessageSource.LogEvent += logEvent =>
      {
        lock (QueueLock)
        {
          Queues[IncomingIndex].Add(logEvent);
        }
      };

      App.MessageSource.TitleChanged += title =>
      {
        Console.WriteLine(title);
        BeginInvoke(() => this.Text = $"MewsiferConsole - {title}");
      };

      App.MessageSource.Start();

      logTable.Scroll += (obj, evt) =>
      {
        if (!TailToggle.Enabled) return;
        if (evt.NewValue < evt.OldValue) { TailToggle.Checked = false; }
        if (evt.NewValue > evt.OldValue && IsScrolledToEnd) { TailToggle.Checked = true; }
      };

      TailToggle.CheckedChanged += (obj, evt) =>
      {
        if (TailToggle.Checked && !IsScrolledToEnd) { IsScrolledToEnd = true; }
      };

      OmniFilter.TextChanged += (obj, evt) =>
      {
        FilterView.Filter = OmniFilter.Text.Trim();
      };
    }

    private void RenderCountLabels()
    {
      infoCountVal.Render();
      warnCountVal.Render();
      errCountVal.Render();
      verCountVal.Render();
    }

    private void OnRowsAddedOrRemoved()
    {
      shownCount.Text = $"{FilterView.Count}/{Messages.Count}";
    }

    private static void AddIndex(Dictionary<string, List<int>> index, string key, int row)
    {
      if (index.TryGetValue(key, out var list)) { list.Add(row); }
      else { index[key] = new() { row }; }
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (sender is not DataGridView dgv) return;

      if (dgv.Columns["Severity"].Index == e.ColumnIndex)
      {
        if (dgv.Rows[e.RowIndex].DataBoundItem is not LogEventViewModel viewModel) return;

        switch (viewModel.Severity)
        {
          case "I":
            e.CellStyle.BackColor = Color.FromArgb(182, 215, 168);
            break;
          case "W":
            e.CellStyle.BackColor = Color.FromArgb(249, 203, 156);
            break;
          case "E":
            e.CellStyle.BackColor = Color.FromArgb(221, 126, 107);
            break;
        }
      }
    }

    private void Clear_Click(object sender, EventArgs e)
    {
      FilterView.Clear();
      IndexRowLookup.Clear();
      ByChannel.Clear();

      infoCountVal.Reset();
      warnCountVal.Reset();
      errCountVal.Reset();
      verCountVal.Reset();
    }

    private void ProcessPendingMessages()
    {
      List<LogEvent> toProcess;
      lock (QueueLock)
      {
        toProcess = Queues[IncomingIndex];
        IncomingIndex = (IncomingIndex == 0) ? 1 : 0;
      }

      //messages.RaiseListChangedEvents = false;
      int addCount = 0;
      foreach (var rawEvent in toProcess)
      {
        LogEventViewModel? prev = Messages.Count > 0 ? Messages.Last() : null;

        if (prev?.MergesWith(rawEvent) == true)
        {
          prev.MergedCount++;
        }
        else
        {
          LogEventViewModel newMessage = new(rawEvent);
          Messages.Add(newMessage);

          AddIndex(IndexRowLookup, newMessage.MessageText, Messages.Count);
          AddIndex(ByChannel, newMessage.ChannelName, Messages.Count);

          var count = rawEvent.Severity switch
          {
            LogSeverity.Info => infoCountVal,
            LogSeverity.Warning => warnCountVal,
            LogSeverity.Error => errCountVal,
            LogSeverity.Verbose => verCountVal,
            _ => throw new NotImplementedException(),
          };

          count.Value++;

          addCount++;
        }
      }

      if (addCount > 0 && Messages.RaiseListChangedEvents)
      {
        RenderCountLabels();
        if (TailToggle.Checked)
          IsScrolledToEnd = true;
      }

      //messages.RaiseListChangedEvents = true;
      //bindingSource.ResetBindings(false);
      toProcess.Clear();
    }

    public class Count
    {
      public int Value = 0;
      public readonly Label Label;
      public readonly string Prefix;

      public Count(Label label, string prefix)
      {
        Label = label;
        Prefix = prefix;
      }

      internal void Render()
      {
        Label.Text = $"{Prefix}: {Value}";
      }

      internal void Reset()
      {
        Value = 0;
        Render();
      }
    }
  }
}