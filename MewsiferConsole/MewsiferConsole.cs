using System.ComponentModel;
using MewsiferConsole.Common;
using MewsiferConsole.IPC;
using Timer = System.Windows.Forms.Timer;

namespace MewsiferConsole
{
  public partial class MewsiferConsole : Form
  {
    private readonly BindingSource BindingSource;
    private readonly BindingList<LogEventViewModel> Messages;
    private readonly LogMessageFilterView FilterView;

    private readonly object QueueLock = new();
    private readonly List<LogEvent>[] Queues = { new(), new() };
    private int IncomingIndex = 0;

    private readonly Timer Timer;

    private bool IsScrolledToEnd
    {
      get
      {
        int firstDisplayed = dataGridView1.FirstDisplayedScrollingRowIndex;
        int lastVisible = firstDisplayed + dataGridView1.DisplayedRowCount(true) - 1;
        return lastVisible == dataGridView1.RowCount - 1;
      }
      set
      {
        if (dataGridView1.RowCount > 0)
        {
          dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
        }
      }
    }

    private readonly Dictionary<string, List<int>> IndexRowLookup = new();
    private readonly Dictionary<string, List<int>> ByChannel = new();

    public MewsiferConsole()
    {
      InitializeComponent();
      BindingSource = new();
      Messages = new();
      FilterView = new(Messages);
      dataGridView1.AutoGenerateColumns = true;

      BindingSource.DataSource = FilterView;
      dataGridView1.DataSource = FilterView;

      Server.Instance.ConsumeAll(msg =>
      {
        if (msg.LogEvent is not null)
        {
          lock (QueueLock)
          {
            Queues[IncomingIndex].Add(msg.LogEvent);
          }
        }
      });

      dataGridView1.Scroll += (obj, evt) =>
      {
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

      Timer = new()
      {
        Interval = 33
      };

      Timer.Tick += (obj, evt) =>
      {
        List<LogEvent> toProcess;
        lock(QueueLock)
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

            AddIndex(IndexRowLookup, newMessage.Message, Messages.Count);
            AddIndex(ByChannel, newMessage.ChannelName, Messages.Count);

            addCount++;
          }
        }

        if (addCount > 0 && TailToggle.Checked) { IsScrolledToEnd = true; }

        //messages.RaiseListChangedEvents = true;
        //bindingSource.ResetBindings(false);
        toProcess.Clear();
      };
      Timer.Start();
    }

    private static void AddIndex(Dictionary<string, List<int>> index, string key, int row)
    {
      if (index.TryGetValue(key, out var list)) { list.Add(row); }
      else { index[key] = new() { row }; }
    }

    private void Console_Load(object sender, EventArgs e) { }

    private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
  }
}