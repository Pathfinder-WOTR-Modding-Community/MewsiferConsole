using Newtonsoft.Json;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// API for pipe IPC between MewsiferConsole and MewsiferConsole.Mod.
  /// </summary>
  public class PipeContract
  {
    public const string PipeName = "MewsiferConsole.Pipe";

    public const string ContractVersion = "1.0";
    public static readonly PipeMessage VersionCheck = PipeMessage.ForServerCommand(new(versionCheck: new()));

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

      public static PipeMessage ForLogEvent(LogEvent logEvent)
      {
        return new PipeMessage(logEvent: logEvent);
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
