using SDL2;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

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

        public static void round_rect(IntPtr renderer, SDL.SDL_Rect rect, byte r, byte g, byte b, byte a, int border_radius, bool fill) {
            SDL.SDL_Color old_color;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out old_color.r, out old_color.g, out old_color.b, out old_color.a);
            SDL.SDL_SetRenderDrawColor(game_manager.renderer, r, g, b, a);

            SDL.SDL_Rect horizontal;
            horizontal.x = rect.x;
            horizontal.y = rect.y + border_radius;
            horizontal.w = rect.w;
            horizontal.h = rect.h - border_radius * 2;

            SDL.SDL_Rect vertical1;
            vertical1.x = rect.x + border_radius;
            vertical1.y = rect.y;
            vertical1.w = rect.w - border_radius * 2;
            vertical1.h = border_radius;

            SDL.SDL_Rect vertical2;
            vertical2.x = rect.x + border_radius;
            vertical2.y = rect.y + rect.h - border_radius;
            vertical2.w = rect.w - border_radius * 2;
            vertical2.h = border_radius;

            if(fill) {

                SDL.SDL_RenderFillRect(game_manager.renderer, ref horizontal);
                SDL.SDL_RenderFillRect(game_manager.renderer, ref vertical1);
                SDL.SDL_RenderFillRect(game_manager.renderer, ref vertical2);

                draw.quarter(game_manager.renderer, rect.x + border_radius, rect.y + border_radius, border_radius, r, g, b, a, 1);
                draw.quarter(game_manager.renderer, rect.x + rect.w - border_radius - 1, rect.y + border_radius, border_radius, r, g, b, a, 2);
                draw.quarter(game_manager.renderer, rect.x + rect.w - border_radius - 1, rect.y + rect.h - border_radius - 1, border_radius, r, g, b, a, 3);
                draw.quarter(game_manager.renderer, rect.x + border_radius, rect.y + rect.h - border_radius - 1, border_radius, r, g, b, a, 4);

            } else {
                SDL.SDL_RenderDrawRect(game_manager.renderer, ref rect);
            }

            SDL.SDL_SetRenderDrawColor(game_manager.renderer, old_color.r, old_color.g, old_color.b, old_color.a);
        }

        public static void circle(IntPtr renderer, int x, int y, int radius, byte r, byte g, byte b, byte a){
            SDL.SDL_Color oldcolor;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out oldcolor.r, out oldcolor.g, out oldcolor.b, out oldcolor.a);
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);
            for (int w = 0; w < radius * 2; w++)
            {
                for (int h = 0; h < radius * 2; h++)
                {
                    int dx = radius - w; // horizontal offset
                    int dy = radius - h; // vertical offset
                    if ((dx*dx + dy*dy) <= (radius * radius))
                    {
                        SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                    }
                }
            }
            SDL.SDL_SetRenderDrawColor(renderer, oldcolor.r, oldcolor.g, oldcolor.b, oldcolor.a);
        }  

        public static void quarter(IntPtr renderer, int x, int y, int radius, byte r, byte g, byte b, byte a, int side){
            SDL.SDL_Color oldcolor;
            SDL.SDL_GetRenderDrawColor(game_manager.renderer, out oldcolor.r, out oldcolor.g, out oldcolor.b, out oldcolor.a);
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);
            for (int w = 0; w < radius * 2; w++)
            {
                for (int h = 0; h < radius * 2; h++)
                {
                    int dx = radius - w; // horizontal offset
                    int dy = radius - h; // vertical offset
                    if ((dx*dx + dy*dy) <= (radius * radius))
                    {
                        if(w > radius && h > radius && side == 1) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w < radius && h > radius && side == 2) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w < radius && h < radius && side == 3) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                        else if(w > radius && h < radius && side == 4) {
                            SDL.SDL_RenderDrawPoint(renderer, x + dx, y + dy);
                        }
                    }
                }
            }
            SDL.SDL_SetRenderDrawColor(renderer, oldcolor.r, oldcolor.g, oldcolor.b, oldcolor.a);
        } 
    
        public static void polygon(IntPtr renderer, byte r, byte g, byte b, byte a, Vector4[] lines) {

        }
    }
}