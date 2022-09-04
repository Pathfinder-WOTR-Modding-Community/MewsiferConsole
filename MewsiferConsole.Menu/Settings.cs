using Kingmaker.Localization;
using Kingmaker.UI.Common;
using MewsiferConsole.Mod;
using ModMenu.Settings;
using System.IO;
using System.Threading;
using UnityEngine;
using static Kingmaker.UI.MessageModalBase;
using Button = ModMenu.Settings.Button;

namespace MewsiferConsole.Menu
{
  internal static class Settings
  {
    private static readonly string KeyRoot = "ModMenu.MewsiferConsole";

    internal static void Initialize()
    {
      ModMenu.ModMenu.AddSettings(
        SettingsBuilder.New(KeyRoot, GetString(GetKey("title"), "Mewsifer Console"))
          .AddImage(Helpers.GetMenuBanner(), 200)
          .AddButton(
            Button.New(
                GetString(GetKey("button-desc"), "Generate a mod bug report"),
                GetString(GetKey("button-text"), "Mod Bug Report"),
                OnClick)
              .WithLongDescription(
                GetString(
                  GetKey("button-desc-long"),
                  "Exports all log events from the current game session. When reporting a bug to a mod developers you "
                  + "can send the generated report, which can be read using MewsiferConsole: "
                  + "https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole.\n\n"
                  + "Do not use this to send bug reports to Owlcat."))));
      Main.Logger.NativeLog("Settings initialized.");
    }

    private static string GetKey(string partialKey)
    {
      return $"{KeyRoot}.{partialKey}";
    }

    private static LocalizedString GetString(string partialKey, string text)
    {
      return Helpers.CreateString(GetKey(partialKey), text);
    }

    private static void OnClick()
    {
      var filename = Path.GetRandomFileName();
      filename = filename.Substring(0, filename.Length - 4); // Remove the random file extension
      new Thread(new ThreadStart(() => GenerateReport(filename))).Start();
      UIUtility.ShowMessageBox(
        "Generating report. This may take a few seconds. Another dialog will display when it is ready.",
        ModalType.Message,
        onClose: null);
    }

    private static void GenerateReport(string filename)
    {
      Main.Logger.NativeLog($"Generating bug report: {filename}");
      var task = Mewsifer.GenerateReport(filename);

      if (task.Wait(60 * 1000))
      {
        Main.Logger.Error($"Failed to generate bug report after 60 seconds.");
        return;
      }

      var filepath = task.Result;
      Main.Logger.NativeLog($"Finished generating mod bug report: {filepath}.");
      GUIUtility.systemCopyBuffer = filepath;
      UIUtility.ShowMessageBox(
        $"Mod Bug Report saved to {filepath}. Copied to clipboard.",
        ModalType.Message,
        onClose: null);
    }
  }
}
