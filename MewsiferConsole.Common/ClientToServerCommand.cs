using Newtonsoft.Json;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// Commands send from the client to the server. Only one field should be populated.
  /// </summary>
  public class ClientToServerCommand
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
  }

  /// <summary>
  /// Command sent from the client to the server to test if the connection is still open. Used since the client pipe
  /// will remain "connected" even after the server disconnects.
  /// </summary>
  public class ConnectionTest { }

  /// <summary>
  /// Intro command to indicate which version of the contract the client uses.
  /// </summary>
  public class VersionCheck
  {
    [JsonProperty]
    public readonly string Version = PipeContract.ContractVersion;

    [JsonConstructor]
    internal VersionCheck() { }
  }
}
