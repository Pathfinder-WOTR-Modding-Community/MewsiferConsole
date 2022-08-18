using MewsiferConsole.Mod.IPC;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using static MewsiferConsole.Common.PipeContract;
using MewsiferSeverity = MewsiferConsole.Common.LogSeverity;
using OwlcatSeverity = Owlcat.Runtime.Core.Logging.LogSeverity;

namespace MewsiferConsole.Mod.OwlcatLogging
{
  /// <summary>
  /// Forwards log events to MewsiferConsole using <see cref="Client"/>.
  /// </summary>
  public class ConsoleLogSink : ILogSink
  {
    public ConsoleLogSink()
    {
      Client.Instance.Initialize();
    }

    public void Log(LogInfo logInfo)
    {
      if (!string.IsNullOrEmpty(logInfo?.Message))
      {
        Client.Instance.SendMessage(CreatePipeMessage(logInfo));
      }
    }

    public void Destroy()
    {
      Main.Logger.Log("Destroying log sink.");
      Client.Instance.Dispose();
    }

    private static PipeMessage CreatePipeMessage(LogInfo logInfo)
    {
      var stackTrace = new List<string>();
      if (logInfo.Callstack is not null && logInfo.Callstack.Any())
      {
        stackTrace.AddRange(logInfo.Callstack.Select(frame => frame.GetFormattedMethodName()));
      }

      return PipeMessage.ForLogEvent(
        GetSeverity(logInfo.Severity), logInfo.Channel.Name, logInfo.Message, stackTrace);
    }

    private static MewsiferSeverity GetSeverity(OwlcatSeverity severity)
    {
      return severity switch
      {
        OwlcatSeverity.Message => MewsiferSeverity.Info,
        OwlcatSeverity.Warning => MewsiferSeverity.Warning,
        OwlcatSeverity.Error => MewsiferSeverity.Error,
        OwlcatSeverity.Disabled => MewsiferSeverity.Verbose,
        _ => throw new ArgumentOutOfRangeException($"Unknown Owlcat LogSeverity: {severity}")
      };
    }
  }
}
