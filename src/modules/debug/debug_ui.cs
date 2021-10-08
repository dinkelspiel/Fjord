using System;
using System.Diagnostics;
using Fjord.Modules.Graphics;

namespace Fjord.Modules.Debug {
    public static class debug_gui
    {
        public static void draw_fps() {
            draw.text(game_manager.renderer, 10, 10, "default", 42, "FPS: " + game_manager.get_fps().ToString());
        }
    }
}
