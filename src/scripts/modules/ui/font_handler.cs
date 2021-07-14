using SDL2;
using System;
using System.Collections.Generic;

namespace Proj.Modules.Ui
{
    public class font_handler
    {
        private static Dictionary<string, dynamic> fonts = new Dictionary<string, dynamic>();

        public static bool load_font(string id, string font, int font_size) {
            if(!fonts.ContainsKey(id)) {
                fonts.Add(id, SDL_ttf.TTF_OpenFont(game_manager.executable_path + "\\src\\resources\\" + game_manager.asset_pack + "\\assets\\fonts\\" + font + ".ttf", font_size));
                return true;
            }
            return false;
        }

        public static void get_text_and_rect(IntPtr renderer, int x, int y, string text, string font_id, out dynamic texture, out SDL.SDL_Rect rect) {
            int text_width;
            int text_height;
            dynamic surface;
            SDL.SDL_Color textColor;
            dynamic font = fonts[font_id];
            textColor.r = textColor.g = textColor.b = 255;
            textColor.a = 0;    

            surface = SDL_ttf.TTF_RenderText_Solid(font, text, textColor);
            texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
            text_width = 300;
            text_height = 300;
            SDL.SDL_FreeSurface(surface);
            rect.x = x;
            rect.y = y;
            rect.w = text_width;
            rect.h = text_height;
        }
    }
}