using Fjord.Modules.Mathf;
using static SDL2.SDL;

namespace Fjord.Modules.Camera {
    public static class camera {
        public static V2f camera_position = new V2f(0, 0);

        public static void set_viewport(float x, float y) {
            SDL_Rect rect;
            SDL_RenderGetViewport(game_manager.renderer, out rect);
            camera_position.x = x - rect.w / 2;
            camera_position.y = y - rect.h / 2;
        }
    }
}