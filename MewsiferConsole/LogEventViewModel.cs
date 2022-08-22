using MewsiferConsole.Common;
using System.ComponentModel;

namespace MewsiferConsole
{
  public class LogEventViewModel : INotifyPropertyChanged
  {
    public readonly LogEvent Model;

    public LogEventViewModel(LogEvent model)
    {
      Model = model;
      Severity = GetSeverityLabel(model.Severity);
    }

    public int RequiredExtraHeight = -1;
    public bool Expanded = false;

    public string ChannelName => Model.Channel;
    public LogEventViewModel Message => this;
    public string MessageText => Model.Message;
    public string StackTraceText => string.Join("\n", Model.StackTrace.Select(line => " ⁍ " + line));
    public string Severity { get; }

    internal bool MergesWith(LogEvent evt)
    {
      return evt.Channel == Model.Channel && evt.Message == MessageText;
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

    public bool HasStackTrace => Model.StackTrace?.Count > 0;

    private static string GetSeverityLabel(LogSeverity severity) => severity switch
    {
      LogSeverity.Info => "I",
      LogSeverity.Warning => "W",
      LogSeverity.Error => "E",
      LogSeverity.Verbose => "V",
      _ => throw new ArgumentOutOfRangeException($"Unknown log severity: {severity}")
    };
  }
}
