using Newtonsoft.Json;
using System.IO;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// Common interface for any objects sent over the pipe.
  /// </summary>
  public interface IPipeObject
  {
    /// <summary>
    /// Writes the object to the text writer. Use manual serialization.
    /// </summary>
    void WriteToJson(JsonTextWriter writer);
  }

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
    public class PipeMessage : IPipeObject
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

      public void WriteToJson(JsonTextWriter writer)
      {
        writer.WriteStartObject();

        if (ServerCommand is null)
        {
          writer.WritePropertyName(nameof(LogEvent));
          LogEvent.WriteToJson(writer);
        }
        else
        {
          writer.WritePropertyName(nameof(ServerCommand));
          ServerCommand.WriteToJson(writer);
        }

        writer.WriteEndObject();
      }

      public string ToJson()
      {
        using (var stringWriter = new StringWriter())
        {
          using (var writer = new JsonTextWriter(stringWriter))
          {
            WriteToJson(writer);
            return stringWriter.ToString();
          }
        }
      }
    }
  }
}
