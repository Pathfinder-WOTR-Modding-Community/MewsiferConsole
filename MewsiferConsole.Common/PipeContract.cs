using Newtonsoft.Json;
using System.Collections.Generic;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// API for pipe IPC between MewsiferConsole and MewsiferConsole.Mod.
  /// </summary>
  public class PipeContract
  {
    public const string PipeName = "MewsiferConsole.Pipe";

    /// <summary>
    /// Messages sent across the pipe. Serialized/deserialized using JSON.
    /// </summary>
    /// 
    /// <remarks>
    /// All fields should be marked with [JsonProperty] and readonly. This ensures serialize/deserialize works and
    /// these should be immutable objects.
    /// </remarks>
    public class PipeMessage
    {
      [JsonProperty]
      public readonly ClientToServerCommand ServerCommand;

      [JsonProperty]
      public readonly LogEvent LogEvent;

      public static PipeMessage ForServerCommand(ClientToServerCommand serverCommand)
      {
        return new PipeMessage(serverCommand: serverCommand);
      }

      public static PipeMessage ForLogEvent(
        LogSeverity severity, string channel, string message, List<string> stackTrace = null)
      {
        return new PipeMessage(logEvent: new(severity, channel, message, stackTrace));
      }

      [JsonConstructor]
      private PipeMessage(ClientToServerCommand serverCommand = null, LogEvent logEvent = null)
      {
        ServerCommand = serverCommand;
        LogEvent = logEvent;
      }
    }
  }
}
