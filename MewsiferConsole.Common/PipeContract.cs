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
    /// Struct with log message details. Used to serialize/deserialize JSON.
    /// </summary>
    public struct LogMessage
    {
      [JsonProperty]
      public bool Control;

      [JsonProperty]
      public string Timestamp;

      [JsonProperty]
      public string Severity;

      [JsonProperty]
      public string ChannelName;


      [JsonProperty]
      public List<string> Message;
    }
  }
}
