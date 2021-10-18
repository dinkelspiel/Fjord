using System;
using System.Diagnostics;
using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Debug {
    public static class debug_gui
    {
        public static void draw_fps() {
            V2 tmp_res = game.resolution;
            game.set_render_resolution(game.renderer, 1920, 1080);
            draw.text(game.renderer, 10, 10, "default", 42, "FPS: " + game.get_fps().ToString());
            game.set_render_resolution(game.renderer, (int)tmp_res.x, (int)tmp_res.y);
        }
    }
}
