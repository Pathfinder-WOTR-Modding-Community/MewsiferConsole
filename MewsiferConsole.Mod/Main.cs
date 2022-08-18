using HarmonyLib;
using MewsiferConsole.Mod.IPC;
using MewsiferConsole.Mod.OwlcatLogging;
using MewsiferConsole.Mod.UMMLogging;
using Owlcat.Runtime.Core.Logging;
using System;
using static UnityModManagerNet.UnityModManager;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MewsiferConsole.Mod
{
  public static class Main
  {
    public static ModLogger Logger;

    private static OwlcatLogSink OwlcatLogSink;
    private static UMMLogSink UMMLogSink;
    private static Harmony Harmony;

    public static bool Load(ModEntry modEntry)
    {
      try
      {
        Logger = modEntry.Logger;
        modEntry.OnUnload = OnUnload;

        Harmony = new(modEntry.Info.Id);
        Harmony.PatchAll();

        OwlcatLogSink = new();
        Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(OwlcatLogSink);

        UMMLogSink = new();

        Client.Instance.Initialize();
        Logger.Log("Finished loading.");
      }
      catch (Exception e)
      {
        Logger.LogException("Failed to load", e);
      }
      return true;
    }

    private static bool OnUnload(ModEntry modEntry)
    {
      Logger.Log("Unloading.");
      Harmony?.UnpatchAll();

      Client.Instance?.Dispose();
      UMMLogSink = null;
      OwlcatLogSink = null;

      return true;
    }
  }
}
