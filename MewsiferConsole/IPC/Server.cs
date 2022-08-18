using Newtonsoft.Json;
using System.IO.Pipes;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole.IPC
{
  internal class Server
  {
    private NamedPipeServerStream? Pipe;
    private BinaryReader? Reader;
    private bool Enabled = true; 

    private static Server? _Instance;
    public static Server Instance => _Instance ??= new();

    public void ConsumeAll(Action<LogMessage> callback)
    {
      Task.Run(() =>
      {
        while (Enabled)
        {
          Console.WriteLine("Creating pipe with name: " + PipeName);
          Pipe = new(PipeName, PipeDirection.In, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
          Reader = new(Pipe);
          Pipe.WaitForConnection();

          while (Pipe.IsConnected)
          {
            try
            {
              var raw = Reader.ReadString();
              callback(JsonConvert.DeserializeObject<LogMessage>(raw));
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
          }

          Reader.Dispose();
          Pipe.Dispose();
        }
      });
    }
  }
}
