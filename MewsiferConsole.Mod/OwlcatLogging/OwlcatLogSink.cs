using MewsiferConsole.Common;
using MewsiferConsole.Mod.IPC;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using MewsiferSeverity = MewsiferConsole.Common.LogSeverity;
using OwlcatSeverity = Owlcat.Runtime.Core.Logging.LogSeverity;

namespace MewsiferConsole.Mod.OwlcatLogging
{
  /// <summary>
  /// Forwards log events to MewsiferConsole using <see cref="Client"/>.
  /// </summary>
  internal class OwlcatLogSink : ILogSink
  {
    private readonly ILogEventHandler LogEventHandler;

    public OwlcatLogSink(ILogEventHandler logEventHandler)
    {
      LogEventHandler = logEventHandler;
    }

    public void Log(LogInfo logInfo)
    {
      try
      {
        if (!string.IsNullOrEmpty(logInfo?.Message) && logInfo.Severity != OwlcatSeverity.Disabled)
        {
          LogEventHandler.OnLogEvent(CreateLogEvent(logInfo));
        }
      }
      catch (Exception e)
      {
        Main.Logger.LogException("Failed to process owlcat log.", e);
      }
    }

    private static LogEvent CreateLogEvent(LogInfo logInfo)
    {
      var stackTrace = new List<string>();
      if (logInfo.Callstack is not null && logInfo.Callstack.Any())
      {
        stackTrace.AddRange(logInfo.Callstack.Select(frame => frame.GetFormattedMethodName()));
      }

      return new(GetSeverity(logInfo.Severity), logInfo.Channel.Name, logInfo.Message, stackTrace);
    }

    private static MewsiferSeverity GetSeverity(OwlcatSeverity severity)
    {
      return severity switch
      {
        OwlcatSeverity.Message => MewsiferSeverity.Info,
        OwlcatSeverity.Warning => MewsiferSeverity.Warning,
        OwlcatSeverity.Error => MewsiferSeverity.Error,
        _ => throw new ArgumentOutOfRangeException($"Unsupported Owlcat LogSeverity: {severity}")
      };
    }

    public void Destroy() { }
  }
}
