using HarmonyLib;
using System;
using static UnityModManagerNet.UnityModManager.ModEntry;
using static UnityModManagerNet.UnityModManager;
using Kingmaker.Blueprints.JsonSystem;

namespace MewsiferConsole.Menu
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

        Harmony = new(modEntry.Info.Id);
        Harmony.PatchAll();

        Logger.Log("Finished loading.");
      }
      catch (Exception e)
      {
        Logger.LogException("Failed to load", e);
        return false;
      }
      return true;
    }

    public static bool OnUnload(ModEntry modEntry)
    {
      Logger.Log("Unloading.");
      Harmony?.UnpatchAll();
      return true;
    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCache_Patch
    {
      [HarmonyPriority(Priority.First)]
      [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
      static void Postfix()
      {
        Settings.Initialize();
      }
    }
  }
}
