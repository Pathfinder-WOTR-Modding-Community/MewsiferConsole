using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using static BetterConsole.Common.PipeContract;

namespace BetterConsole.Mod.IPC
{
  /// <summary>
  /// Client for BetterConsole. Forwards log messages to BetterConsole.
  /// </summary>
  /// 
  /// <remarks>
  /// Originally I tried to use H.Pipes which provides a wrapper for easier use. Unfortunately it is built entirely
  /// on async pipes which is not supported in Unity. Now it's using RawPipes though converting to JSON would be easy
  /// if needed.
  /// </remarks>
  public class Client : IDisposable
  {
    /// <summary>
    /// Max log lines in the queue after which they will be discarded.
    /// </summary>
    private const int MaxQueue = 250;

    private static Client _instance;
    public static Client Instance => _instance ??= new();

    private bool Enabled;
    private NamedPipeClientStream Stream;
    private Thread Thread;
    // Queue of LogMessage serialized to Json
    private readonly ConcurrentQueue<string> LogQueue = new();

    public void Initialize()
    {
      if (Thread is not null)
      {
        Dispose();
      }

      Enabled = true;
      Thread = new Thread(new ThreadStart(InitializeAsync));
      Thread.Start();
    }

    /// <summary>
    /// Reports a log message which is added to the queue for send.
    /// </summary>
    public void ReportLog(LogMessage message)
    {
      LogQueue.Enqueue(JsonConvert.SerializeObject(message));
      if (LogQueue.Count > MaxQueue)
      {
        LogQueue.TryDequeue(out _);
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
          Main.Logger.Log("Connecting to BetterConsole.");
          Stream = new(PipeName);
          Stream.Connect();
          Main.Logger.Log("Connection established.");

          WriteStream();
        }
        catch (Win32Exception)
        {
          // No server available
          Thread.Sleep(10000);
        }
        catch (IOException)
        {
          Main.Logger.Log("BetterConsole died, waiting for its return.");
          Thread.Sleep(10000);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Error while connecting to BetterConsole.", e);
          break;
        }
      }
    }

    /// <summary>
    /// Every loop calls TestConnection() because Stream.IsConnected never returns false once a connection is
    /// established. Testing involves sending a control command to the server, which will throw an IOException caught
    /// in InitializeAsync if the server died.
    /// </summary>
    private void WriteStream()
    {
      using (var writer = new BinaryWriter(Stream))
      {
        while (Enabled)
        {
          TestConnection(writer);
          if (LogQueue.Any() && LogQueue.TryDequeue(out string message))
          {
            writer.Write(message);
            writer.Flush();
          }
          else
          {
            // Wait for more messages
            Thread.Sleep(1000);
          }
        }
      }
    }

    private static readonly string TestMessage =  JsonConvert.SerializeObject(new LogMessage() { Control = true });
    private void TestConnection(BinaryWriter writer)
    {
      writer.Write(TestMessage);
      writer.Flush();
    }
  }
}
