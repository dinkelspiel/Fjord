using System.Numerics;
using static SDL2.SDL;

namespace Fjord.Modules.Camera {
    public static class camera {
        public static Vector2 camera_position = new Vector2(0, 0);

        public static void set_viewport(float x, float y) {
            SDL_Rect rect;
            SDL_RenderGetViewport(game_manager.renderer, out rect);
            camera_position.X = x - rect.w / 2;
            camera_position.Y = y - rect.h / 2;
        }
    }
}