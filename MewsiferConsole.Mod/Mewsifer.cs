using System.Threading.Tasks;

namespace MewsiferConsole.Mod
{
  /// <summary>
  /// API for mods to do stuff.
  /// </summary>
  public class Mewsifer
  {
    /// <summary>
    /// Generates a report which captures all log events and can be opened using MewsiferConsole.
    /// </summary>
    /// 
    /// <remarks>
    /// This is an asynchronous that may take some time. It is not recommended to convert the result to a synchronous
    /// call unless you run it on your own thread.
    /// </remarks>
    /// 
    /// <param name="reportFileName">Name used for the generated report file, without extension.</param>
    /// <returns>The file path for the generated report.</returns>
    public static async Task<string> GenerateReport(string reportFileName)
    {
      return await Main.LogEventHandler.GetLogDump(reportFileName);
    }
  }
}
