using MewsiferConsole.Common;
using System.ComponentModel;

namespace MewsiferConsole
{
  internal class LogEventViewModel : INotifyPropertyChanged
  {
    public readonly LogEvent Model;

    public LogEventViewModel(LogEvent model)
    {
      Model = model;
    }

    public string ChannelName => Model.Channel;
    public string Message => Model.Message;

    internal bool MergesWith(LogEvent evt)
    {
      return evt.Channel == Model.Channel && evt.Message == Message;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private int _MergedCount = 1;
    public int MergedCount
    {
      get => _MergedCount;
      set
      {
        if (_MergedCount == value) { return; }

        _MergedCount = value;
        PropertyChanged?.Invoke(this, new(nameof(MergedCount)));
      }
    }
  }
}
