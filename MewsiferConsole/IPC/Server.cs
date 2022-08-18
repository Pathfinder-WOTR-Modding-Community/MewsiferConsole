using Newtonsoft.Json;
using System.IO.Pipes;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.IPC
{
  internal class Server : IRawMessageSource
  {
    private bool Enabled = true;

    public event HandleRawMessage? RawMessage;
    public event TitleChanged? TitleChanged;


    public void Start()
    {
      while (Enabled)
      {
        TitleChanged?.Invoke("Waiting for connection from game");
        Console.WriteLine("Creating pipe with name: " + PipeName);
        using NamedPipeServerStream Pipe = new(PipeName, PipeDirection.In, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
        using BinaryReader Reader = new(Pipe);

        Pipe.WaitForConnection();
        TitleChanged?.Invoke("Connected to game");

        while (Pipe.IsConnected)
        {
          try
          {
            var raw = Reader.ReadString();
            RawMessage?.Invoke(raw);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
        }
      }
    }
  }
}
