using MewsiferConsole.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.Mod.IPC
{
  /// <summary>
  /// Client for MewsiferConsole. Forwards log messages to MewsiferConsole.
  /// </summary>
  internal class Client : IDisposable
  {
    /// <summary>
    /// Max messages in the queue after which they will be discarded.
    /// </summary>
    private const int MaxQueue = 20000;

    private static Client _instance;
    internal static Client Instance => _instance ??= new();

    private bool Enabled;
    private NamedPipeClientStream Stream; 
    private Thread Thread;
    // Queue of PipeMessage
    private readonly ConcurrentQueue<PipeMessage> MessageQueue = new();

    internal void Initialize()
    {
      if (Thread is not null)
      {
        Dispose();
      }

      // First thing is to check the version
      MessageQueue.Enqueue(PipeContract.VersionCheck);

      Enabled = true;
      Thread = new Thread(new ThreadStart(InitializeAsync));
      Thread.IsBackground = true;
      Thread.Start();
    }

    /// <summary>
    /// Adds a message to the queue for send to the console.
    /// </summary>
    internal void SendMessage(PipeMessage message)
    {
      MessageQueue.Enqueue(message);
      if (MessageQueue.Count > MaxQueue)
      {
        MessageQueue.TryDequeue(out _);
      }
    }

    public void Dispose()
    {
      Enabled = false;
      Stream?.Dispose();
      if ((bool)(Thread?.IsAlive))
      {
        Thread.Abort();
      }
    }

    /// <summary>
    /// Since async pipes aren't available just loop waiting for input in a thread. This is the outer loop which
    /// connects and reconnects, WriteStream is the inner loop which writes output.
    /// </summary>
    private void InitializeAsync()
    {
      while (Enabled)
      {
        try
        {
          Stream?.Dispose();
          Main.Logger.NativeLog($"Connecting to MewsiferConsole: {PipeName}");
          Stream = new(".", PipeName, PipeDirection.Out);
          Stream.Connect();
          Main.Logger.Log("Connection established.");

          WriteStream();
        }
        catch (Win32Exception)
        {
          // No server available, sleep with backoff
          Thread.Sleep(10 * 1000);
        }
        catch (IOException)
        {
          Main.Logger.Log("MewsiferConsole died, waiting for its return.");
          Thread.Sleep(10 * 1000);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Error while connecting to MewsiferConsole.", e);
          break;
        }
      }
    }

    internal const int MaxMessagesPerFrame = 5;
    internal const int FrameDelay = 17; // Approximately 1 frame every 17ms ~ 60fps.
    private void WriteStream()
    {
      using (var writer = new BinaryWriter(Stream))
      {
        int written = 0;
        while (Enabled)
        {
          if (MessageQueue.Any() && MessageQueue.TryPeek(out PipeMessage message))
          {
            written++;

            writer.Write(JsonConvert.SerializeObject(message));
            writer.Flush();
            MessageQueue.TryDequeue(out _);

            if (written >= MaxMessagesPerFrame)
            {
              written = 0;
              // Sleep to minimize CPU usage
              Thread.Sleep(FrameDelay);
            }
          }
          else
          {
            written = 0;
            // Wait for more messages
            Thread.Sleep(1000);
          }
        }
      }
    }
  }
}
