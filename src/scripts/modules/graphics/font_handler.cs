using SDL2;
using System;
using System.Collections.Generic;

namespace Fjord.Modules.Graphics
{
    public static class font_handler
    {
        private static Dictionary<string, dynamic> fonts = new Dictionary<string, dynamic>();

        public static void init() {
            string ass = game_manager.asset_pack;
            game_manager.set_asset_pack("general");
            fonts.Add("default", SDL_ttf.TTF_OpenFont(game_manager.executable_path + "\\src\\resources\\general\\assets\\fonts\\FiraCode.ttf", 22));
            game_manager.set_asset_pack(ass);
        }

        public static bool load_font(string id, string font, int font_size) {
            if(!fonts.ContainsKey(id)) {
                fonts.Add(id, SDL_ttf.TTF_OpenFont(game_manager.executable_path + "\\src\\resources\\" + game_manager.asset_pack + "\\assets\\fonts\\" + font + ".ttf", font_size));
                return true;
            }
            return false;
        }

        public static void get_text_and_rect(IntPtr renderer, string text, string font_id, out IntPtr texture, out SDL.SDL_Rect rect, int x = 0, int y = 0, byte r = 255, byte g = 255, byte b = 255, byte a = 255) {
            int text_width;
            int text_height;
            IntPtr surface;
            SDL.SDL_Color textColor;
            dynamic font = fonts[font_id];
            textColor.r = r;
            textColor.g = g;
            textColor.b = b;
            textColor.a = a;    

            surface = SDL_ttf.TTF_RenderText_Solid(font, text, textColor);
            texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
            uint pog; int pog2;
            SDL.SDL_QueryTexture(texture, out pog, out pog2, out text_width, out text_height);
            SDL.SDL_FreeSurface(surface);
            rect.x = x;
            rect.y = y;
            rect.w = text_width;
            rect.h = text_height;
        }
    }
}