using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MewsiferConsole.Mod
{
  internal class DPasteClient
  {
    private const string Url = "https://dpaste.org/api/";
    private const string ContentKey = "content";

    private static readonly HttpClient Client = new();

    internal static async Task<string> PostReport(string logDump)
    {
      Main.Logger.Log("Posting log dump.");
      var requestContent = new Dictionary<string, string>() { { ContentKey, logDump } };
      var httpResponse = await Client.PostAsync(Url, new FormUrlEncodedContent(requestContent));

      var responseContent = await httpResponse.Content.ReadAsStringAsync();
      if (httpResponse.IsSuccessStatusCode)
      {
        return responseContent;
      }
      Main.Logger.Error($"dpaste.org request failed: {httpResponse.StatusCode} - {responseContent}");
      return string.Empty;
    }
  }
}
