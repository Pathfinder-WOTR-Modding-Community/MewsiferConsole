using System;
using static UnityModManagerNet.UnityModManager;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MewsiferConsole.Mod
{
  public static class Main
  {
    public static ModLogger Logger;
    private static ConsoleLogSink LogSink;

    public static bool Load(ModEntry modEntry)
    {
      try
      {
        Logger = modEntry.Logger;
        modEntry.OnUnload = OnUnload;

        LogSink = new();
        Owlcat.Runtime.Core.Logging.Logger.Instance.AddLogger(LogSink);

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
      LogSink?.Destroy();
      LogSink = null;
      return true;
    }
  }
}
