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
    internal static ModLogger Logger;
    private static Harmony Harmony;

    public static bool Load(ModEntry modEntry)
    {
      try
      {
        Logger = modEntry.Logger;
        modEntry.OnUnload = OnUnload;

        // Initialize log sinks before harmony patches or it might result in exceptions thrown.
        var logEventHandler = new LogEventHandler();
        UMMLoggingPatches.Initialize(new(logEventHandler));

        Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(new OwlcatLogSink(logEventHandler));

        Harmony = new(modEntry.Info.Id);
        Harmony.PatchAll();

        logEventHandler.Init();
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
      return true;
    }
  }
}
