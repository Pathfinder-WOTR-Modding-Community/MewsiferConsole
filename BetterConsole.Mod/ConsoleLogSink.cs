using BetterConsole.Mod.IPC;
using Kingmaker.Utility;
using Owlcat.Runtime.Core.Logging;
using System.Collections.Generic;
using System.Linq;
using static BetterConsole.Common.PipeContract;

namespace BetterConsole.Mod
{
  /// <summary>
  /// Forwards log events to BetterConsole using <see cref="IPC.Client"/>.
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
        Client.Instance.ReportLog(CreateLogMessage(logInfo));
      }
    }

    public void Destroy()
    {
      Main.Logger.Log("Destroying log sink.");
      Client.Instance.Dispose();
    }

    private static LogMessage CreateLogMessage(LogInfo logInfo)
    {
      var message = new List<string>();
      message.Add(logInfo.Message);
      if (logInfo.Callstack is not null && logInfo.Callstack.Any())
      {
        foreach (var frame in logInfo.Callstack)
        {
          message.Add(frame.GetFormattedMethodName());
        }
      }

      return new()
      {
        ChannelName = logInfo.Channel.Name,
        Severity = logInfo.Severity.ToString(),
        Message = message
      };
    }
  }
}
