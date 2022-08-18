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
    public readonly CaptureBugReport CaptureBugReport;

    public static ClientToServerCommand TestConnection()
    {
      return new ClientToServerCommand(connectionTest: new());
    }

    [JsonConstructor]
    private ClientToServerCommand(CaptureBugReport bugReport = null, ConnectionTest connectionTest = null)
    {
      ConnectionTest = connectionTest;
    }
  }

  /// <summary>
  /// Command sent from the client to the server to test if the connection is still open. Used since the client pipe
  /// will remain "connected" even after the server disconnects.
  /// </summary>
  public class ConnectionTest { }

  public class CaptureBugReport
  {

  }
}
