using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Proj.Game {
    public class text_editor : scene {

        List<String> text = new List<string>();

        int current_line = 0;
        int current_char = 0;

        IntPtr line_texture;
        SDL.SDL_Rect line_rect, letter_rect;

        SDL.SDL_Rect header;

        string font = "FiraCode";

        public text_editor() {

            font_handler.load_font("Cozette", "Cozette", 22);
            font_handler.load_font("Nunito", "Nunito", 22);
            font_handler.load_font("FiraCode", "FiraCode", 22);
            text.Add("");

            font_handler.get_text_and_rect(game_manager.renderer, "a", font, out line_texture, out letter_rect, 0, 0);

            header.x = header.y = 0;
            header.w = 1280;
            header.h = 25;
        }

        public override void update() {
            if(input.get_any_key_just_pressed() != -1) {
                if(input.get_any_key_just_pressed() == input.key_backspace) {
                    if(text[current_line].Length > 0) {
                        if(!input.get_key_pressed(input.key_lctrl)) {
                            var edit_text = text[current_line].Substring(0, current_char);
                            var remainder_text = text[current_line].Substring(current_char, text[current_line].Length - current_char);

                            if(edit_text.Length > 0) {
                                edit_text = edit_text.Substring(0, edit_text.Length - 1);
                            }
                            
                            text[current_line] = edit_text + remainder_text;
                            current_char -= current_char > 0 ? 1 : 0;
                        } else {
                            var edit_text = text[current_line].Substring(0, current_char);
                            var remainder_text = text[current_line].Substring(current_char, text[current_line].Length - current_char);

                            var text_arr = edit_text.Split(" ");
                            text_arr = text_arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            text_arr = text_arr.SkipLast(1).ToArray();
                            edit_text = string.Join(" ", text_arr);

                            text[current_line] = edit_text + remainder_text;
                            current_char = text[current_line].Length;
                        }
                    }
                } else if(input.get_any_key_just_pressed() == input.key_space) {
                    text[current_line] += " ";
                    current_char += 1;
                } else if(input.get_any_key_just_pressed() == input.key_return) {
                    text.Add("");
                    current_line += 1;
                    current_char = text[current_line].Length;
                } else if(input.get_any_key_just_pressed() == input.key_tab) {
                    text[current_line] += "    ";
                } else if(input.get_any_key_just_pressed() == input.key_up) {
                    if(current_line > 0) {
                        current_line -= 1;
                        current_char = text[current_line].Length;
                    }
                } else if(input.get_any_key_just_pressed() == input.key_down) {
                    if(current_line < text.Count - 1) {
                        current_line += 1;
                        current_char = text[current_line].Length;
                    }
                } else if(input.get_any_key_just_pressed() == input.key_left) {
                    if(current_char > 0) {
                        current_char -= 1;
                    } else {
                        if(current_line > 0) {
                            current_line -= 1;
                            current_char = text[current_line].Length;
                        }
                    }
                } else if(input.get_any_key_just_pressed() == input.key_right) {
                    current_char += current_char < text[current_line].Length ? 1 : 0;
                } else if(input.get_key_pressed(input.key_lctrl)) {
                    if(input.get_key_just_pressed(input.key_s)) {
                        
                    }
                } else {
                    if(input.get_key(input.get_any_key_just_pressed()).Length == 1) 
                        text[current_line] += input.get_key(input.get_any_key_just_pressed());
                        current_char += 1;
                }
            }     
        }

        public override void render() {

            draw.rect(game_manager.renderer, header, 100, 100, 100, 255, true);

            int i = 0;
            foreach(string line in text) {
            
                font_handler.get_text_and_rect(game_manager.renderer, line, font, out line_texture, out line_rect, 0, 0, 110, 219, 115, 255);
                SDL.SDL_Rect dest;
                dest.x = 5;
                dest.y = line_rect.h * i - i * 5 + 30;
                dest.w = line_rect.w;
                dest.h = line_rect.h;
                SDL.SDL_RenderCopy(game_manager.renderer, line_texture, ref line_rect, ref dest); 

                if(i == current_line) {
                    var length = text[current_line].Substring(0, current_char);
                    font_handler.get_text_and_rect(game_manager.renderer, length, font, out line_texture, out line_rect, 0, 0, 110, 219, 115, 255);

                    SDL.SDL_Rect marker;
                    marker.x = line_rect.w + 7;
                    marker.y = dest.y + dest.h / 6;
                    marker.w = 2;
                    marker.h = dest.h - dest.h / 3;
                    draw.rect(game_manager.renderer, marker, 255, 255, 255, 255, true);
                }
                i++;
            }

            IntPtr header_texture;
            SDL.SDL_Rect header_src, header_dest;
            var header_text = "*unsaved*";
            font_handler.get_text_and_rect(game_manager.renderer, header_text, font, out header_texture, out header_src, 0, 0, 0, 0, 0, 255);
            header_dest.x = 5;
            header_dest.y = 0;
            header_dest.w = header_src.w;
            header_dest.h = header_src.h;
            SDL.SDL_RenderCopy(game_manager.renderer, header_texture, ref header_src, ref header_dest);
        }
    }
}