using System.ComponentModel;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole
{
    internal class LogMessageViewModel : INotifyPropertyChanged
    {
        public readonly LogMessage Model;

        public LogMessageViewModel(LogMessage model)
        {
            Model = model;
        }

        public string ChannelName => Model.ChannelName;
        public string Message => BuildMessage(Model);

        private static string BuildMessage(LogMessage model)
        {
            return model.Message.Count > 0 ? model.Message[0] : "-";
        }

        internal bool MergesWith(LogMessage msg)
        {
            return msg.ChannelName == Model.ChannelName && BuildMessage(msg) == Message;
        }

        private int _MergedCount = 1;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int MergedCount
        {
            get => _MergedCount;
            set
            {
                if (_MergedCount == value) return;
                _MergedCount = value;
                PropertyChanged?.Invoke(this, new(nameof(MergedCount)));
            }
        }
    }
}
