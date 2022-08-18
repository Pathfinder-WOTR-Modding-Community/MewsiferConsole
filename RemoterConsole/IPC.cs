using BetterConsole.Common;
using Newtonsoft.Json;
using System.Buffers;
using System.IO.Pipes;
using static BetterConsole.Common.PipeContract;

namespace RemoterConsole
{
    internal class IPC
    {
        private readonly NamedPipeServerStream pipe;
        private readonly MemoryPool<byte> pool;
        private readonly BinaryReader reader;

        private static IPC? _Instance;
        public static IPC Instance => _Instance ??= new();
        public IPC()
        {
            Console.WriteLine("Creating pipe with name: " + PipeContract.PipeName);
            pipe = new(PipeContract.PipeName, PipeDirection.In, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
            pool = MemoryPool<byte>.Shared;
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
