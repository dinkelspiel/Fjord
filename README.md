<img src=https://imgur.com/dhYU9ni>
<img src=https://img.shields.io/github/workflow/status/willmexe/Fjord/build/main>

# Fjord

V2 of my <a href=https://github.com/willmexe/Game-Engine>old engine</a> now in C# from Python
 
## Usage

The two folders that are important if you are making a game are
`./src/scripts/game/` and `./src/resources/`.

The first path is for where you put all your scripts. 
The already present project is the boiler plate to load the game in the engine but doesn't do anything.

The latter is where you put your resources in the form of assetpacks.
Assetpacks have a structure of:
* `[asset_pack]/assets/images/` for images
* `[asset_pack]/assets/fonts/` for fonts
* `[asset_pack]/data/lang/` for language files
* `[asset_pack]/data/tilemaps/` for tilemaps

Assetpacks are loaded using the `game_manager.set_asset_pack(string asset_pack);` function.

Help with the modules can be found at [the wiki](https://github.com/willmexe/Fjord/wiki).

## Projects that use Fjord

[Get the last](https://github.com/willmexe/Mini-Jam-88) - A game made for the Mini Jam 88 Game Jam.
