using System.ComponentModel;
using MewsiferConsole.IPC;
using static MewsiferConsole.Common.PipeContract;
using Timer = System.Windows.Forms.Timer;

namespace MewsiferConsole
{
    public partial class Form1 : Form
    {
        private readonly BindingSource bindingSource;
        private readonly BindingList<LogMessageViewModel> messages;
        private readonly LogMessageFilterView filterView;

        private object queueLock = new();
        private List<LogMessage>[] queues =
        {
            new(), new()
        };
        private int incomingIndex = 0;

        private Timer timer;

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
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }

        }

        private readonly Dictionary<string, List<int>> indexRowLookup = new();
        private readonly Dictionary<string, List<int>> byChannel = new();

        public Form1()
        {
            InitializeComponent();
            bindingSource = new();
            messages = new();
            filterView = new(messages);
            dataGridView1.AutoGenerateColumns = true;

            bindingSource.DataSource = filterView;
            dataGridView1.DataSource = filterView;


            Server.Instance.ConsumeAll(msg =>
            {
                lock (queueLock)
                {
                    queues[incomingIndex].Add(msg);
                }
            });

            dataGridView1.Scroll += (obj, evt) =>
            {
                if (evt.NewValue < evt.OldValue)
                    TailToggle.Checked = false;
                if (evt.NewValue > evt.OldValue && IsScrolledToEnd)
                    TailToggle.Checked = true;
            };

            TailToggle.CheckedChanged += (obj, evt) =>
            {
                if (TailToggle.Checked && !IsScrolledToEnd)
                    IsScrolledToEnd = true;
            };

            OmniFilter.TextChanged += (obj, evt) =>
            {
                filterView.Filter = OmniFilter.Text.Trim();
            };

            timer = new();
            timer.Interval = 33;
            timer.Tick += (obj, evt) =>
            {
                List<LogMessage> toProcess;
                lock(queueLock)
                {
                    toProcess = queues[incomingIndex];
                    incomingIndex = (incomingIndex == 0) ? 1 : 0;
                }

                //messages.RaiseListChangedEvents = false;
                int addCount = 0;
                foreach (var rawMessage in toProcess)
                {
                    if (rawMessage.Control) continue;
                    LogMessageViewModel? prev = messages.Count > 0 ? messages.Last() : null;

                    if (prev?.MergesWith(rawMessage) == true)
                    {
                        prev.MergedCount++;
                    }
                    else
                    {
                        LogMessageViewModel newMessage = new(rawMessage);
                        messages.Add(newMessage);

                        AddIndex(indexRowLookup, newMessage.Message, messages.Count);
                        AddIndex(byChannel, newMessage.ChannelName, messages.Count);

                        addCount++;
                    }
                }

                if (addCount > 0 && TailToggle.Checked)
                    IsScrolledToEnd = true;

                //messages.RaiseListChangedEvents = true;
                //bindingSource.ResetBindings(false);
                toProcess.Clear();
            };
            timer.Start();
        }


        private static void AddIndex(Dictionary<string, List<int>> index, string key, int row)
        {
            if (index.TryGetValue(key, out var list))
                list.Add(row);
            else
                index[key] = new() { row };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}