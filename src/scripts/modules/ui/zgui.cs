using Proj.Modules.Graphics;
using Proj.Modules.Input;
using Proj.Modules.Debug;
using SDL2;
using System;
using System.Numerics;

namespace Proj.Modules.Ui
{
    public static class zgui
    {

        static Vector2 corrected_pos = new Vector2(0, 0);
        static bool drag = false;

        public static void init() {
            font_handler.load_font("text", "Cozette", 12);
        }

        public static void window(SDL.SDL_Rect rect, SDL.SDL_Color bg, SDL.SDL_Color o1, SDL.SDL_Color o2, SDL.SDL_Color o3, string font, string title, bool show_label) {
            SDL.SDL_Rect rect1;
            rect1.x = rect.x - 3;
            rect1.y = rect.y - 3;
            rect1.w = rect.w + 6;
            rect1.h = rect.h + 6;
            draw.rect(game_manager.renderer, rect1, o3.r, o3.g, o3.b, o3.a, false);

            rect1.x = rect.x - 2;
            rect1.y = rect.y - 2;
            rect1.w = rect.w + 4;
            rect1.h = rect.h + 4;
            draw.rect(game_manager.renderer, rect1, o2.r, o2.g, o2.b, o2.a, false);

            rect1.x = rect.x - 1;
            rect1.y = rect.y - 1;
            rect1.w = rect.w + 2;
            rect1.h = rect.h + 2;
            draw.rect(game_manager.renderer, rect1, o1.r, o1.g, o1.b, o1.a, false);

            draw.rect(game_manager.renderer, rect, bg.r, bg.g, bg.b, bg.a, true);

            if (show_label) {
                // render::text(x + 13, y - 5, font, string, false, color::black());
                // render::text(x + 11, y - 7, font, string, false, color::white());
                IntPtr texture;
                SDL.SDL_Rect dest;

                font_handler.get_text_and_rect(game_manager.renderer, title, font, out texture, out rect1, 0, 0, 0, 0, 0, 255);
                //draw.texture_ext(game_manager.renderer, texture, rect.x + 13, rect.y - 5, 0);
                dest.x = rect.x + 13;
                dest.y = rect.y - 5;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture, ref rect1, ref dest);

                font_handler.get_text_and_rect(game_manager.renderer, title, font, out texture, out rect1);
                //draw.texture_ext(game_manager.renderer, texture, rect.x + 11, rect.y - 7, 0);
                dest.x = rect.x + 11;
                dest.y = rect.y - 7;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture, ref rect1, ref dest);
                
            }
        }
    
        public static void window_movement(ref int x, ref int y, ref int w, ref int h) {
            if(mouse.button_just_pressed(0)) {
                corrected_pos.X = mouse.x - x;
			    corrected_pos.Y = mouse.y - y;
                if(mouse.x > x && mouse.x < x + w) {
                    if(mouse.y > y && mouse.y < y + h) {
                        drag = true;
                    }
                }
            }
            if(mouse.button_pressed(0)) {
                if(drag) {
                    x = mouse.x - (int)corrected_pos.X;
                    y = mouse.y - (int)corrected_pos.Y;
                }
            }
        }
    }
}