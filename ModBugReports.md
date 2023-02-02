# So you want to report a WotR mod bug?

Follow these step by step instructions and you'll be that mod dev's favorite user.

## Special Case: Unable to Load Save

If you can't load a save the first thing to do is verify the integrity of the game files: [Using GOG](https://support.gog.com/hc/en-us/articles/360003930017-My-game-data-is-corrupt-How-can-I-repair-my-game-?product=gog), [Using Steam](https://help.steampowered.com/en/faqs/view/0C48-FCBD-DA71-93EB).

If that doesn't work either:

* A mod you have installed is failing to configure something
* You have removed a mod required for the save

If you know you removed a mod, try reinstalling / re-enabling it.

If you still have a problem, skip to [Step 3a](#3a-collect-logs-manually) and report the issue in Discord. There's no easy way to identify which mod is at fault so it's best to share in Discord and someone will help you.

Note: If you're not using MewsiferConsole to generate the bug report the important log for this is `GameLogFull.txt`.

## 1. Figure out the reproduction (or repro) steps

Telling someone "this mod doesn't work" or "your mod breaks my game" isn't helpful. Answer these questions:

* What were you doing when the problem occurred?
    * `I tried to respec Ciar`
* What was the problem? Describe it.
    * `The game crashed`
* Can you reproduce the issue? If so, describe the steps you took.
    * `Yes. I opened up the Respec mod options, selected Respec for Ciar, and the game crashes.`

## 2. Identify the mod or mods responsible

Disable as many mods as you can while still seeing the problem. Anything disabled is not at fault. Hopefully you can narrow it down to a single mod, but that's not always possible.

*Tip: disable half your mods and test. Repeat this (disabling half the current enabled mods) until the problem stops. Now enable only the last set of mods you disabled. Repeat this until you have the minimum set of mods enabled to reproduce the problem.*

### Example

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

## 3. Collect a Bug Report

1. Install [MewsiferConsole](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/README.md#setup) including the optional step to install MewsiferConsole.Menu.
2. Reproduce the issue in-game
3. Open the options menu, go to the Mods menu, expand Mewsifer Console
4. Click Mod Bug Report
5. Copy the file path to the generated report (it is automatically copied, you can just paste it somewhere)

![Menu options screenshot](https://github.com/Pathfinder-WOTR-Modding-Community/MewsiferConsole/blob/main/screenshots/menu.png)

### 3a. Collect Logs Manually 

If you can't get the game to load at all or don't want to use MewsiferConsole, provide these log files:

`%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\GameLogFull.txt`
`%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\Player.log`

## 4. Report the Bug

1. Reach out to the dev on [Discord](https://discord.com/invite/owlcat) in #mod-user-general, their Nexus mods page, or their GitHub repo
    * In [ModFinder](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder) you can open their homepage from the overflow menu
2. Share your answers from [Step 1](#1-figure-out-the-reproduction-or-repro-steps), mods enabled in [Step 2](#2-identify-the-mod-or-mods-responsible), and the bug report from [Step 3](#3-collect-a-bug-report)
3. If possible, share a save file used to reproduce the problem
    * Save files are in `%UserProfile%\AppData\LocalLow\Owlcat Games\Pathfinder Wrath of the Righteous\Saved Games`
4. Pat yourself on the back. It is a lot of work to report bugs, but so is creating and maintaining mods. Your help is appreciated.
