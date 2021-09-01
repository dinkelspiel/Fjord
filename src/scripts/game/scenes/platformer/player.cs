using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Game;
using System.Collections.Generic;
using SDL2;
using System;
using Newtonsoft.Json;
using Proj.Modules.Tools;
using System.Numerics;

namespace Proj.Game
{
    public class player : entity {
        public player() {
            game_manager.set_asset_pack("platformer");
            texture = texture_handler.load_texture("grass.png", game_manager.renderer);
        }

        public override void update() {
            
        }

        public override void render() {
            base.render();
        }
    }
}