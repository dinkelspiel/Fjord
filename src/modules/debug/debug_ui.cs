using System;
using System.Diagnostics;
using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Debug {
    public static class debug_gui
    {
        public static void draw_fps() {
            V2 tmp_res = game_manager.resolution;
            game_manager.set_render_resolution(game_manager.renderer, 1920, 1080);
            draw.text(game_manager.renderer, 10, 10, "default", 42, "FPS: " + game_manager.get_fps().ToString());
            game_manager.set_render_resolution(game_manager.renderer, (int)tmp_res.x, (int)tmp_res.y);
        }
    }
}
