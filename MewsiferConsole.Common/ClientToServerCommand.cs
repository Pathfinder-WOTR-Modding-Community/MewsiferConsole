using Newtonsoft.Json;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// Commands send from the client to the server. Only one field should be populated.
  /// </summary>
  public class ClientToServerCommand : IPipeObject
  {
    [JsonProperty]
    public readonly ConnectionTest ConnectionTest;

    [JsonProperty]
    public readonly VersionCheck VersionCheck;

    public static ClientToServerCommand TestConnection()
    {
      return new ClientToServerCommand(connectionTest: new());
    }

    [JsonConstructor]
    internal ClientToServerCommand(ConnectionTest connectionTest = null, VersionCheck versionCheck = null)
    {
      ConnectionTest = connectionTest;
      VersionCheck = versionCheck;
    }

    public void WriteToJson(JsonTextWriter writer)
    {
      writer.WriteStartObject();

      if (ConnectionTest is null)
      {
        writer.WritePropertyName(nameof(VersionCheck));
        VersionCheck.WriteToJson(writer);
      }
      else
      {
        writer.WritePropertyName(nameof(ConnectionTest));
        ConnectionTest.WriteToJson(writer);
      }

      writer.WriteEndObject();
    }
  }

  /// <summary>
  /// Command sent from the client to the server to test if the connection is still open. Used since the client pipe
  /// will remain "connected" even after the server disconnects.
  /// </summary>
  public class ConnectionTest : IPipeObject
  {
    public void WriteToJson(JsonTextWriter writer)
    {
      writer.WriteStartObject();
      writer.WriteEndObject();
    }
  }

  /// <summary>
  /// Intro command to indicate which version of the contract the client uses.
  /// </summary>
  public class VersionCheck : IPipeObject
  {
    [JsonProperty]
    public readonly string Version = PipeContract.ContractVersion;

    [JsonConstructor]
    internal VersionCheck() { }

    public void WriteToJson(JsonTextWriter writer)
    {
      writer.WriteStartObject();

      writer.WritePropertyName(nameof(Version));
      writer.WriteValue(Version);

      writer.WriteEndObject();
    }
  }
}
