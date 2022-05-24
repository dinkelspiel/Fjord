using Fjord.Modules.Mathf;
using static SDL2.SDL;

namespace Fjord.Modules.Camera {
    public static class camera {
        public static V2f camera_position = new V2f(0, 0);

        public static V2f get() {
            return camera_position;
        }

        public static void set(float x, float y) {
            SDL_Rect rect;
            SDL_RenderGetViewport(game.renderer, out rect);
            camera_position.x = x;
            camera_position.y = y;
        }

        public static void set(V2f position) {
            SDL_Rect rect;
            SDL_RenderGetViewport(game.renderer, out rect);
            camera_position.x = position.x;
            camera_position.y = position.y;
        }

        public static void add(float x, float y) {
            camera_position.x += x;
            camera_position.y += y;
        }

        public static void add(V2f position) {
            camera_position.x += position.x;
            camera_position.y += position.y;
        }

        public static void sub(float x, float y) {
            camera_position.x -= x;
            camera_position.y -= y;
        }

        public static void sub(V2f position) {
            camera_position.x -= position.x;
            camera_position.y -= position.y;
        }
    }
}