using SDL2;
using System;

namespace Proj.Modules.Ui {
    public static class draw {
        public static void rect(IntPtr renderer, SDL.SDL_Rect rect, byte r, byte g, byte b, byte a, bool fill) {
            SDL.SDL_Color old_color;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            if(fill) {
                SDL.SDL_RenderFillRect(game_manager.renderer, ref rect);
            } else {
                SDL.SDL_RenderDrawRect(game_manager.renderer, ref rect);
            }

            SDL.SDL_SetRenderDrawColor(game_manager.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }
    }
}