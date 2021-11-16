using static SDL2.SDL;
using Fjord.Modules.Input;

namespace Fjord.Modules.Mathf {
    public static class helpers {
        public static bool mouse_inside(SDL_Rect rect, int margin=0) {
            if ((mouse.x > rect.x - margin && mouse.x < rect.x + rect.w + margin) && (mouse.y > rect.y - margin && mouse.y < rect.y + rect.h + margin))
                return true;
            else
                return false;
        }
    }
}