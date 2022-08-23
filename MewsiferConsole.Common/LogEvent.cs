using Newtonsoft.Json;
using System.Collections.Generic;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// Common log severity definition for both Owlcat and UMM logging.
  /// </summary>
  public enum LogSeverity
  {
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// Owlcat: <c>LogChannel.Log()</c>
    /// </item>
    /// <item>
    /// UMM: <c>ModLogger.Log()</c>
    /// </item>
    /// </list>
    /// </summary>
    Info,
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// Owlcat: <c>LogChannel.Warning()</c>
    /// </item>
    /// <item>
    /// UMM: <c>ModLogger.Warning()</c>
    /// </item>
    /// </list>
    /// </summary>
    Warning,
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// Owlcat: <c>LogChannel.Error()</c> or <c>LogChannel.Exception()</c>
    /// </item>
    /// <item>
    /// UMM: <c>ModLogger.Error()</c>, <c>ModLogger.Critical()</c>, or <c>ModLogger.LogException()</c>
    /// </item>
    /// </list>
    /// </summary>
    Error,
    /// <summary>
    /// <list type="bullet">
    /// <item>
    /// Owlcat: Unsupported.
    /// </item>
    /// <item>
    /// UMM: <c>ModLogger.NativeLog()</c>
    /// </item>
    /// </list>
    /// </summary>
    Verbose
  }

  /// <summary>
  /// Represents a log event from either UMM's <c>ModLogger</c> or Owlcat's <c>LogChannel</c>.
  /// </summary>
  public class LogEvent : IPipeObject
  {
    [JsonProperty]
    public readonly LogSeverity Severity;
    [JsonProperty]
    public readonly string Channel;
    [JsonProperty]
    public readonly string Message;
    [JsonProperty]
    public readonly List<string> StackTrace;

    [JsonConstructor]
    public LogEvent(LogSeverity severity, string channel, string message, List<string> stackTrace = null)
    {
      Severity = severity;
      Channel = channel;
      Message = message;
      StackTrace = stackTrace ?? new();
    }

    public void WriteToJson(JsonTextWriter writer)
    {
      writer.WriteStartObject();

      writer.WritePropertyName(nameof(Severity));
      writer.WriteValue((int)Severity);

      writer.WritePropertyName(nameof(Channel));
      writer.WriteValue(Channel);

      writer.WritePropertyName(nameof(Message));
      writer.WriteValue(Message);

      writer.WritePropertyName(nameof(StackTrace));
      writer.WriteStartArray();
      foreach (string trace in StackTrace)
      {
        writer.WriteValue(trace);
      }
      writer.WriteEndArray();

      writer.WriteEndObject();
    }
  }
}
