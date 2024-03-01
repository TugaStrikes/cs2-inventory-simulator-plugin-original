# CS2 Inventory Simulator Plugin

A simple plugin for integrating with [CS2 Inventory Simulator](https://inventory.cstrike.app). It features basically all we know (publically) so far to display economy items in-game, so it's full of hacks of all sorts and is missing a lot of features.

> [!CAUTION]
> This plugin has not been fully and thoroughly tested. Compatibility with other plugins has also not been tested. Use at your own risk.

> [!CAUTION]
> As you probably know, Valve can ban your server for using plugins like this one, so be advised. [See more information on Valve Guidelines...](https://blog.counter-strike.net/index.php/server_guidelines)

## Current Features

- Weapon/Knife
  - Paint Kit, Wear, Seed, Name tag, StatTrak.
- Gloves
  - Paint Kit, Wear, Seed. 
- Agents
- Music Kit

## Feature Roadmap

- [ ] StatTrak increment
- [ ] ⛔ Weapon Stickers
- [ ] ⛔ Pins
- [ ] ⛔ Agent Patches
- [ ] ⛔ Graffiti

> [!IMPORTANT]  
> ⛔ means I'm not aware of a way to modify using CSSharp or C++ and is very unlikely to be implemented any time soon.

> [!WARNING]  
> Right now I'm open to issue reports, please don't open feature request or suggestion issues - they will be closed. I may take your comments into account, but the issue is going to remain closed.

## Installation

1. Make sure `FollowCS2ServerGuidelines` is `false` in `addons/counterstrikesharp/configs/core.json`.
2. Add the contents of `gamedata/gamedata.json` to `addons/counterstrikesharp/gamedata/gamedata.json`.
3. [Download](https://github.com/ianlucas/cs2-InventorySimulatorPlugin/releases) the latest release.
4. Extract the .zip file into `addons/counterstrikesharp`.

### Configuration?

Not right now. I'm planning on adding options for the Inventory Simulator endpoint and `cslib`'s endpoints (so you can point to yours). So right now you depend on my online services or a fork of the project.

### Commands?

Not right now. I'm planning on adding a command for refreshing the inventory, but it's not really high priority for me as I'm going to use this on competitive matches, and I don't want players messing with skins mid-game, so right now the skins are only fetched when the player connects to the server.

### Known issues

- All knives will have the rare deploy animation (Windows-only).
- Drop buy won't apply skins.
- Sometimes the skin name won't be displayed on HUD. (Fixed?)

## See also

If you are looking for a plugin that gives you more control, please see [cs2-WeaponPaints](https://github.com/Nereziel/cs2-WeaponPaints).
