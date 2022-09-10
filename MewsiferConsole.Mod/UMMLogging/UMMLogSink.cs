using MewsiferConsole.Common;
using Owlcat.Runtime.Core.Logging;
using System;
using System.Collections.Generic;
using LogSeverity = MewsiferConsole.Common.LogSeverity;

namespace MewsiferConsole.Mod.UMMLogging
{
  public class UMMLogSink
  {
    private readonly ILogEventHandler LogEventHandler;

    public UMMLogSink(ILogEventHandler logEventHandler)
    {
      LogEventHandler = logEventHandler;
    }

    public void Log(LogSeverity severity, string channel, string message)
    {
      try
      {
        LogEventHandler.OnLogEvent(CreateLogEvent(severity, channel, message));
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
        LogEventHandler.OnLogEvent(CreateLogEvent(LogSeverity.Error, channel, message, ex));
      }
      catch (Exception e)
      {
        Main.Logger.LogException("Failed to process UMM log.", e);
      }
    }

    private static LogEvent CreateLogEvent(
      LogSeverity severity, string channel, string message, Exception ex = null)
    {
      var stackTrace = new List<string>();
      while (ex is not null)
      {
        stackTrace.Add(ex.Message);
        if (ex.StackTrace is not null)
        {
          stackTrace.AddRange(
            ex.StackTrace.Split(new string[] { Logger.UnityInternalNewLine }, StringSplitOptions.None));
        }
        ex = ex.InnerException;
        if (ex is not null)
        {
          stackTrace.Add($"-- Inner: {ex.Message}");
        }
      }
      return new(severity, channel, message, stackTrace);
    }
  }
}
