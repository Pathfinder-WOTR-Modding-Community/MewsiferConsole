using MewsiferConsole.Common;
using MewsiferConsole.Mod.IPC;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.Mod
{
  public interface ILogEventHandler
  {
    void OnLogEvent(LogEvent logEvent);
  }

  /// <summary>
  /// Handler for processing all log events.
  /// </summary>
  internal class LogEventHandler : ILogEventHandler
  {
    /// <summary>
    /// Max log events in the queue before dumping to the log file.
    /// </summary>
    private const int MaxQueue = 5000;

    /// <summary>
    /// Temporary file used to store logs when the queue is full.
    /// </summary>
    private readonly string LogTempFile = Path.GetTempFileName();

    // Queue for dumping to the log file. Contains JSON serialized PipeMessage for each log event.
    private readonly ConcurrentQueue<string> LogFileQueue = new();

    private int _fileOpen = 0;
    private bool FileOpen
    {
      get { return Interlocked.CompareExchange(ref _fileOpen, 1, 1) == 1; }
      set
      {
        if (value) { Interlocked.CompareExchange(ref _fileOpen, 1, 0); }
        else { Interlocked.CompareExchange(ref _fileOpen, 0, 1); }
      }
    }

    public void Init()
    {
      try
      {
        Main.Logger.Log($"Logging to {LogTempFile}");
        File.WriteAllLines(LogTempFile, new string[] { Client.VersionCheck });
      }
      catch (Exception e)
      {
        Main.Logger.LogException(e);
      }
    }

    public void OnLogEvent(LogEvent logEvent)
    {
      LogFileQueue.Enqueue(Client.Instance.SendMessage(PipeMessage.ForLogEvent(logEvent)));
      if (LogFileQueue.Count > MaxQueue && !FileOpen)
      {
        FileOpen = true;
        // Write on another thread so it doesn't block the thread triggering log events from the game.
        new Thread(new ThreadStart(WriteToTempFile)).Start();
      }
    }

    private void WriteToTempFile()
    {
      Main.Logger.Log($"Writing logs to file: {LogTempFile}");
      using (var writer = new StreamWriter(LogTempFile, append: true))
      {
        while (LogFileQueue.Count > MaxQueue / 2)
        {
          if (LogFileQueue.TryDequeue(out string message))
          {
            writer.WriteLine(message);
          }
        }
      }
      FileOpen = false;
    }
  }
}
