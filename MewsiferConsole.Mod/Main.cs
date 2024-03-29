﻿using HarmonyLib;
using MewsiferConsole.Mod.IPC;
using MewsiferConsole.Mod.OwlcatLogging;
using MewsiferConsole.Mod.UMMLogging;
using System;
using static UnityModManagerNet.UnityModManager;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MewsiferConsole.Mod
{
  public static class Main
  {
    internal static ModLogger Logger;
    internal static LogEventHandler LogEventHandler;

    private static Harmony Harmony;

    public static bool Load(ModEntry modEntry)
    {
      try
      {
        Logger = modEntry.Logger;
        modEntry.OnUnload = OnUnload;

        // Initialize log sinks before harmony patches or it might result in exceptions thrown.
        LogEventHandler = new LogEventHandler();
        UMMLoggingPatches.Initialize(new(LogEventHandler));

        Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(new OwlcatLogSink(LogEventHandler));

        Harmony = new(modEntry.Info.Id);
        Harmony.PatchAll();

        LogEventHandler.Init();
        Client.Instance.Initialize();

        // Test code
        //new Thread(new ThreadStart(LogReport)).Start();
        Logger.Log("Finished loading.");
      }
      catch (Exception e)
      {
        Logger.LogException("Failed to load", e);
        return false;
      }
      return true;
    }

    // Test code
    //private static void LogReport()
    //{
    //  Thread.Sleep(5 * 60 * 1000);
    //  var report = Mewsifer.GenerateReport("TestReport");
    //  report.Wait();
    //  Logger.Log($"Generated report: {report.Result}");
    //}

    public static bool OnUnload(ModEntry modEntry)
    {
      Logger.Log("Unloading.");
      Harmony?.UnpatchAll();
      Client.Instance?.Dispose();
      return true;
    }
  }
}
