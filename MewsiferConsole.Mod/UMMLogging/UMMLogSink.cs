using MewsiferConsole.Common;
using MewsiferConsole.Mod.IPC;
using System;
using System.Collections.Generic;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.Mod.UMMLogging
{
  public class UMMLogSink
  {
    public void Log(LogSeverity severity, string channel, string message)
    {
      try
      {
        Client.Instance.SendMessage(CreatePipeMessage(severity, channel, message));
      }
      catch (Exception e)
      {
        Main.Logger.LogException("Failed to process UMM log.", e);
      }
    }

    public void Log(string channel, Exception ex, string key = "")
    {
      try
      {
        var message =
          string.IsNullOrEmpty(key)
            ? $"An exception ({ex.GetType().Name}) occurred."
            : $"An exception ({ex.GetType().Name} - {key}) occurred.";
        Client.Instance.SendMessage(CreatePipeMessage(LogSeverity.Error, channel, message, ex));
      }
      catch (Exception e)
      {
        Main.Logger.LogException("Failed to process UMM log.", e);
      }
    }

    private static PipeMessage CreatePipeMessage(
      LogSeverity severity, string channel, string message, Exception ex = null)
    {
      var stackTrace = new List<string>();
      while (ex is not null)
      {
        stackTrace.Add(ex.Message);
        ex = ex.InnerException;
        stackTrace.Add("-- Inner: ");
      }
      return PipeMessage.ForLogEvent(severity, channel, message, stackTrace);
    }
  }
}
