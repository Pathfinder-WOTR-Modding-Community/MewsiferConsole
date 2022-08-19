using System.Threading.Tasks;

namespace MewsiferConsole.Mod
{
  /// <summary>
  /// API for mods to do stuff.
  /// </summary>
  public class Mewsifer
  {
    /// <summary>
    /// Generates a report, capturing all of the log events and storing them on https://dpaste.org/.
    /// </summary>
    /// 
    /// <remarks>
    /// This is asynchronous as it may takes some time. It is not recommended to convert the result to a synchronous
    /// call unless you run it on your own thread.
    /// </remarks>
    /// 
    /// <returns>The URL for the report, or empty if the request failed</returns>
    public static async Task<string> GenerateReport()
    {
      return await DPasteClient.PostReport(Main.LogEventHandler.GetLogDump());
    }
  }
}
