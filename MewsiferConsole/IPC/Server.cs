using Newtonsoft.Json;
using System.IO.Pipes;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.IPC
{
  internal class Server
  {
    private readonly NamedPipeServerStream pipe;
    private readonly BinaryReader reader;

    private static Server? _Instance;
    public static Server Instance => _Instance ??= new();

    public Server()
    {
      Console.WriteLine("Creating pipe with name: " + PipeName);
      pipe = new(PipeName, PipeDirection.In, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
      reader = new(pipe);
    }

    public void ConsumeAll(Action<LogMessage> callback)
    {
      Task.Run(() =>
      {
        pipe.WaitForConnection();

        while (pipe.IsConnected)
        {
          try
          {
            var raw = reader.ReadString();
            callback(JsonConvert.DeserializeObject<LogMessage>(raw));
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
        }
      });
    }
  }
}
