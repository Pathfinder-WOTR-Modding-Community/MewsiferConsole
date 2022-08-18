using HarmonyLib;
using MewsiferConsole.Common;
using System;
using System.Reflection;
using static UnityModManagerNet.UnityModManager.ModEntry;

namespace MewsiferConsole.Mod.UMMLogging
{
  internal class UMMLoggingPatches
  {
    private static readonly UMMLogSink LogSink = new();
    private static readonly FieldInfo Prefix = AccessTools.Field(typeof(ModLogger), "Prefix");

    [HarmonyPatch(typeof(ModLogger))]
    static class ModLogger_Patch
    {
      [HarmonyPatch(nameof(ModLogger.Log)), HarmonyPostfix]
      static void Log_Postfix(ModLogger __instance, string str)
      {
        try
        {
          LogSink.Log(LogSeverity.Info, (string)Prefix.GetValue(__instance), str);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Log_Postfix", e);
        }
      }

      [HarmonyPatch(nameof(ModLogger.Warning)), HarmonyPostfix]
      static void Warning_Postfix(ModLogger __instance, string str)
      {
        try
        {
          LogSink.Log(LogSeverity.Warning, (string)Prefix.GetValue(__instance), str);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Warning_Postfix", e);
        }
      }

      [HarmonyPatch(nameof(ModLogger.Error)), HarmonyPostfix]
      static void Error_Postfix(ModLogger __instance, string str)
      {
        try
        {
          LogSink.Log(LogSeverity.Error, (string)Prefix.GetValue(__instance), str);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Error_Postfix", e);
        }
      }

      [HarmonyPatch(nameof(ModLogger.Critical)), HarmonyPostfix]
      static void Critical_Postfix(ModLogger __instance, string str)
      {
        try
        {
          LogSink.Log(LogSeverity.Error, (string)Prefix.GetValue(__instance), str);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("Critical_Postfix", e);
        }
      }

      [HarmonyPatch(nameof(ModLogger.NativeLog)), HarmonyPostfix]
      static void NativeLog_Postfix(ModLogger __instance, string str)
      {
        try
        {
          LogSink.Log(LogSeverity.Error, (string)Prefix.GetValue(__instance), str);
        }
        catch (Exception e)
        {
          Main.Logger.LogException("NativeLog_Postfix", e);
        }
      }

      [HarmonyPatch(nameof(ModLogger.LogException), typeof(Exception)), HarmonyPostfix]
      static void LogException_Postfix(ModLogger __instance, Exception e)
      {
        try
        {
          LogSink.Log((string)Prefix.GetValue(__instance), e);
        }
        catch (Exception ex)
        {
          Main.Logger.LogException("LogException_Postfix", ex);
        }
      }

      [HarmonyPatch(nameof(ModLogger.LogException), typeof(string), typeof(Exception)), HarmonyPostfix]
      static void LogExceptionWithKey_Postfix(ModLogger __instance, string key, Exception e)
      {
        try
        {
          LogSink.Log((string)Prefix.GetValue(__instance), e, key);
        }
        catch (Exception ex)
        {
          Main.Logger.LogException("LogExceptionWithKey_Postfix", ex);
        }
      }
    }
  }
}
