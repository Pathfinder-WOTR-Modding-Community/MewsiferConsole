using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MewsiferConsole.Mod
{
  internal class DPasteClient
  {
    private const string Url = "https://dpaste.org/api/";
    private const string ContentKey = "content";
    private const string ExpiresKey = "expires";
    private const string ExpiresValue = "2592000";

    private static readonly HttpClient Client = new();

    internal static async Task<string> PostReport(string logDump)
    {
      try
      {
        Main.Logger.Log("Posting log dump.");
        var httpResponse = await Client.PostAsync(Url, EncodeLogDump(logDump));

        var responseContent = await httpResponse.Content.ReadAsStringAsync();
        if (httpResponse.IsSuccessStatusCode)
        {
          return responseContent;
        }
        Main.Logger.Error($"dpaste.org request failed: {httpResponse.StatusCode} - {responseContent}");
      } catch (Exception e)
      {
        Main.Logger.LogException(e);
      }
      return string.Empty;
    }

    /// <summary>
    /// Normally you might just use FormUrlEncodedContent(), but that actually enforces some pretty strict limits on
    /// data size. No way we can upload logs using it. Instead just encode ourselves.
    /// </summary>
    private static StringContent EncodeLogDump(string logDump)
    {
      var encodedContentKey = WebUtility.UrlEncode(ContentKey);
      var encodedContent = WebUtility.UrlEncode(logDump);
      var encodedExpiresKey = WebUtility.UrlEncode(ExpiresKey);
      var encodedExpires = WebUtility.UrlEncode(ExpiresValue);
      return new(
        $"{encodedContentKey}={encodedContent}&{encodedExpiresKey}={encodedExpires}",
        null,
        "application/x-www-form-urlencoded");
    }
  }
}
