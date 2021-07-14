using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;
using System;

namespace Proj.Game {
    public class text_editor : scene {

        string text;

        IntPtr texture1;
        SDL.SDL_Rect rect1;

        public text_editor() {

        }

        public override void update() {
            if(input.get_any_key_just_pressed() != -1) {
                if(input.get_any_key_just_pressed() != input.key_backspace && input.get_any_key_just_pressed() != input.key_space) {
                    text += input.get_key(input.get_any_key_just_pressed());
                } else if(input.get_any_key_just_pressed() == input.key_backspace) {
                    text = text.Remove(text.Length - 1);
                } else if(input.get_any_key_just_pressed() == input.key_space) {
                    text += " ";
                }
            }

            font_handler.get_text_and_rect(game_manager.renderer, 0, 0, text, "default", out texture1, out rect1);
        }

        public override void render() {
            SDL.SDL_Rect src;
            src.x = src.y = 0;
            src.w = rect1.w;
            src.h = rect1.h;
            SDL.SDL_RenderCopy(game_manager.renderer, texture1, ref src, ref rect1);  
        }
    }
}