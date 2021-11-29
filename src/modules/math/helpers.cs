using static SDL2.SDL;
using Fjord.Modules.Input;

namespace Fjord.Modules.Mathf {
    public static class helpers {
        public static bool mouse_inside(SDL_Rect rect, int margin=0) {
            if ((mouse.position.x > rect.x - margin && mouse.position.x < rect.x + rect.w + margin) && (mouse.position.y > rect.y - margin && mouse.position.y < rect.y + rect.h + margin))
                return true;
            else
                return false;
        }

        public static SDL_Rect v4_to_sdl(V4 rect) {
            return new SDL_Rect(rect.x, rect.y, rect.z, rect.w);
        }

        public static V4 sdl_to_v4(SDL_Rect rect) {
            return new V4(rect.x, rect.y, rect.w, rect.h);
        }
    }
}