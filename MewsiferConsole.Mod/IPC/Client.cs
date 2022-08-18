﻿using MewsiferConsole.Common;
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
    /// Max log lines in the queue after which they will be discarded.
    /// </summary>
    private const int MaxQueue = 100000;

    private static Client _instance;
    internal static Client Instance => _instance ??= new();

    private static readonly JsonSerializerSettings SerializerSettings =
      new()
      {
        NullValueHandling = NullValueHandling.Ignore,
      };

    private bool Enabled;
    private NamedPipeClientStream Stream; 
    private Thread Thread;
    // Queue of LogMessage serialized to Json
    private readonly ConcurrentQueue<string> LogQueue = new();

    internal void Initialize()
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
    /// Adds a message to the queue for send to the console.
    /// </summary>
    internal void SendMessage(PipeMessage message)
    {
      LogQueue.Enqueue(JsonConvert.SerializeObject(message, SerializerSettings));
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
          Main.Logger.Log("Connecting to MewsiferConsole.");
          Main.Logger.Log($"Connecting to MewsiferConsole: {PipeName}");
          Stream = new(".", PipeName, PipeDirection.Out);
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
          Main.Logger.Log("MewsiferConsole died, waiting for its return.");
          Thread.Sleep(10000);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Error while connecting to MewsiferConsole.", e);
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
            Main.Logger.NativeLog(message);
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

    private static readonly string ConnectionTest =
      JsonConvert.SerializeObject(
        PipeMessage.ForServerCommand(ClientToServerCommand.TestConnection()),
        SerializerSettings);
    private void TestConnection(BinaryWriter writer)
    {
      writer.Write(ConnectionTest);
      writer.Flush();
    }
  }
}
