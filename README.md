# MewsiferConsole
An improved remote console for reading logs in Pathfinder: Wrath of the Righteous.

For reporting bugs to WotR mod devs, read [So you want to report a WotR mod bug?](#so-you-want-to-report-a-wotr-mod-bug)

**Mod Developers: Add `MewsiferConsole.Mod` to `LoadAfter` in your `Info.json` to ensure all of your logs are captured.**

```json
"LoadAfter": ["MewsiferConsole.Mod"]
```

![Console screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/console.png)

## Features

* Captures Unity Mod Manager and Game logs
* Filtering by Channel, Severity / Level, and text
* De-dupe log events to reduce log spam
* Bug report API to export a session which can be loaded into the console
* Better performance than RemoteConsole

## Setup

1. Install [Unity Mod Manager](https://github.com/newman55/unity-mod-manager) (UMM), minimum version 0.23.0, and configure for use with Wrath
2. Install [ModFinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder) and use it to search for Mewsifer Console
3. Click "Install"
4. (Optional) Search for and install Mewsifer Console Menu and its dependency, [ModMenu]()
    * Adds a button to the Mods menu which generates a bug report containing all logs from the current game session
    * This is a great way to get bug reports from users

![Menu options screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/menu.png)

If you don't want to use ModFinder you can download the [latest release](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/releases) and install normally using UMM.

## Usage

After installation run `%WrathPath%/Mods/MewsiferConsole.Mod/MewsiferConsole.exe` where `%WrathPath%` is the base game directory, `C:\Program Files (x86)\Steam\steamapps\common\Pathfinder Second Adventure` by default for Steam users.

You will be prompted to select a log source:

![Startup screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/startup.png)

Select "Connect to game" to view logs for a currently active game instance. You can also start the game after starting the console.

Select "Load from file" to load a `*.mew` file created using the bug report API. See below for instructions on creating a bug report.

**Important:** The console relies on a mod to send log events. MewsiferConsole.Mod needs to be enabled for it to function.

### Filters

Filter by text:

![Text include screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/text_include.png)

Filter by channel (use "-" to exclude):

![Channel include screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/channel_include.png)
![Channel exclude screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/channel_exclude.png)

Filter by log level / severity:

![Severity include screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/severity_include.png)
![Severity exclude screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/severity_exclude.png)

When combining filters a log is shown if it matches at least one include filter and *zero* exclude filters. For example, if your filter was `ch:Mews ch:Tabletop -sev:E -sev:V`:

| Log Event                                       | Shown? | Why?                |
| ----------------------------------------------- | :----: | ------------------- |
| (I) [MewsiferConsole.Mod] Connected             | Yes    | Matching channel    |
| (E) [MewsiferConsole.Mod] An exception occurred | No     | Severity excluded   |
| (V) [MewsiferConsole.Mod] Sending message       | No     | Severity excluded   |
| (I) [TabletopTweaks-Core] Adding                | Yes    | Matching channel    |
| (I) [TabletopTweaks-Rework] Adding              | Yes    | Matching channel    |
| (I) [System] Await                              | No     | No matching channel |

### Log Level / Severity

For UMM logs:

| Logging Method             | Severity  |
| -------------------------- | :-------: |
| `ModLogger.Log()`          | (I)nfo    |
| `ModLogger.Warning()`      | (W)arning |
| `ModLogger.Error()`        | (E)rror   |
| `ModLogger.Critical()`     | (E)rror   |
| `ModLogger.LogException()` | (E)rror   |
| `ModLogger.NativeLog()`    | (V)erbose |

For Game logs:

| Logging Method           | Severity  |
| ------------------------ | :-------: |
| `LogChannel.Log()`       | (I)nfo    |
| `LogChannel.Verbose()`   | (I)nfo    |
| `LogChannel.Warning()`   | (W)arning |
| `LogChannel.Error()`     | (E)rror   |
| `LogChannel.Exception()` | (E)rror   |

Notably, Game logs do not support `Verbose`, so calls to `LogChannel.Verbose()` use the same severity as `LogChannel.Log()`.

### Bug Report API

If you want to capture bug reports for your mod you can call `Mewsifer.GenerateReport(fileName)` ([Source](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/MewsiferConsole.Mod/Mewsifer.cs)):

1. Add a reference to `MewsiferConsole.Mod.dll` to your project
2. Call `Mewsifer.GenerateReport(fileName)` supplying the name of the resulting report
3. Wait for the returned `Task` to complete
    * The result contains the full file path
4. Instruct the user to upload the resulting file to the appropriate location, or upload it automatically in your mod

Example:

```C#
// Request in a new thread to prevent blocking the current thread from executing when Wait() is called
new Thread(new ThreadStart(LogReport)).Start();

private static void LogReport()
{
  var report = Mewsifer.GenerateReport("MyModTestReport");
  report.Wait();
  // Prints the report file path to the log
  Logger.Log($"Report generated: {report.Result}");
}
```

Once you have the file you can open it by launching the console and selecting "Load from file" at startup.

## So you want to report a WotR mod bug?

Follow these step by step instructions and you'll be that mod dev's favorite user.

### 1. Figure out the reproduction (or repro) steps

Telling someone "this mod doesn't work" or "your mod breaks my game" isn't helpful. Answer these questions:

* What were you doing when the problem occurred?
    * `I tried to respec Ciar`
* What was the problem? Describe it.
    * `The game crashed`
* Can you reproduce the issue? If so, describe the steps you took.
    * `Yes. I opened up the Respec mod options, selected Respec for Ciar, and the game crashes.`

### 2. Identify the mod or mods responsible

Disable as many mods as you can while still seeing the problem. Anything disabled is not at fault. Hopefully you can narrow it down to a single mod, but that's not always possible.

*Tip: disable half your mods and test. Repeat this (disabling half the current enabled mods) until the problem stops. Now enable only the last set of mods you disabled. Repeat this until you have the minimum set of mods enabled to reproduce the problem.*

#### Example

Installed Mods: `TTT-Core, TTT-Base, TTT-Reworks, ToyBox, BubbleBuffs, BubbleTweaks`

1. Disable `TTT-Core`, `TTT-Base`, and `TTT-Reworks`
2. If the problem doesn't occur:
    * Disable `ToyBox`, `BubbleBuffs`, and `BubbleTweaks`
    * Enable only `TTT-Core` and test
    * Enable only `TTT-Core` and `TTT-Base` and test
    * Enable only `TTT-Core` and `TTT-Reworks` and test
3. If the problem occurs: disable `ToyBox` and `BubbleBuffs`
4. If the problem doesn't occur:
    * Enable only `ToyBox` and test
    * Enable only `BubbleBuffs` and test
5. If not: disable `BubbleTweaks`
6. If the problem doesn't occur: `BubbleTweaks` is at fault
7. Otherwise: this is a base game issue, use Alt + B in-game to report a bug

### 3. Collect a Bug Report

1. Install [MewsiferConsole](#setup) including the optional step to install MewsiferConsole.Menu.
2. Reproduce the issue in-game
3. Open the options menu, go to the Mods menu, expand Mewsifer Console
4. Click Mod Bug Report
5. Copy the file path to the generated report (it is automatically copied, you can just paste it somewhere)

![Menu options screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/menu.png)

If you have problems with this, you can instead copy the logs manually:

`%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\GameLogFull.txt`
`%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\Player.log`

### 4. Report the Bug

1. Reach out to the dev on [Discord](https://discord.com/invite/owlcat) in #mod-user-general, their Nexus mods page, or their GitHub repo
    * In [ModFinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder) you can open their homepage from the overflow menu
2. Share your answers from [Step 1](#1-figure-out-the-reproduction-or-repro-steps), mods enabled in [Step 2](#2-identify-the-mod-or-mods-responsible), and the bug report from [Step 3](#3-collect-a-bug-report)
3. If possible, share a save file used to reproduce the problem
    * Save files are in `%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\Saved Games`
4. Pat yourself on the back. It is a lot of work to report bugs, but so is creating and maintaining mods. Your help is appreciated.

## Acknowledgements

* The modding community on [Discord](https://discord.com/invite/owlcat), an invaluable and supportive resource for help modding.
* All the Owlcat modders who came before me, wrote documents, and open sourced their code.
* Cyrix on Discord for the banner illustration used in MewsiferConsole.Menu

## Interested in modding?

* Check out the [OwlcatModdingWiki](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki).
* Join us on [Discord](https://discord.com/invite/wotr).
