# MewsiferConsole
An improved remote console for reading logs in Pathfinder: Wrath of the Righteous.

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

1. Download and install [Unity Mod Manager](https://github.com/newman55/unity-mod-manager) (UMM), minimum version 0.23.0
    * Run UMM and set it up for use with Wrath
2. Download the [latest release](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/releases)
3. Install by dragging the zip file for the latest release into the UMM window under the Mods tab

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

## Acknowledgements

* The modding community on [Discord](https://discord.com/invite/wotr), an invaluable and supportive resource for help modding.
* All the Owlcat modders who came before me, wrote documents, and open sourced their code.

## Interested in modding?

* Check out the [OwlcatModdingWiki](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki).
* Join us on [Discord](https://discord.com/invite/wotr).
