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

        IntPtr texture1;
        SDL.SDL_Rect rect1, letter;

        SDL.SDL_Rect marker, marker_to;

        int fallback_y;

        public text_editor() {
            font_handler.load_font("Cozette", "Cozette", 32);
            font_handler.load_font("Nunito", "Nunito", 32);
            text.Add("");

            font_handler.get_text_and_rect(game_manager.renderer, 0, 0, "a", "Nunito", out texture1, out letter);
        }

        public override void update() {
            if(input.get_any_key_just_pressed() != -1) {
                if(input.get_key(input.get_any_key_just_pressed()).Length == 1) {
                    text[current_line] += input.get_key_pressed(input.key_lshift) ? input.get_key(input.get_any_key_just_pressed()).ToUpper() : input.get_key(input.get_any_key_just_pressed());
                } else if(input.get_any_key_just_pressed() == input.key_backspace) {
                    if(input.get_key_pressed(input.key_lctrl)) {
                        var text_arr = text[current_line].Split(" ");
                        if(text_arr.Length > 1) {
                            text_arr = text_arr.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            text_arr[text_arr.Length - 1] = "";
                            text[current_line] = string.Join(" ", text_arr);
                        } else if(current_line > 0) {
                            current_line -= 1;
                            text.RemoveAt(text.Count - 1);
                        } else {
                            text_arr[0] = "";
                            text[current_line] = string.Join(" ", text_arr);
                        }
                    } else {
                        if(text[current_line].Length > 0) {
                            text[current_line] = text[current_line].Remove(text[current_line].Length - 1);
                        } else {
                            if(current_line > 0) {
                                current_line -= 1;
                                text.RemoveAt(text.Count - 1);
                            }
                        }
                    }
                } else if(input.get_any_key_just_pressed() == input.key_space) {
                    text[current_line] += " ";
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

                } else if(input.get_any_key_just_pressed() == input.key_right) {

                }
            }

            marker.x += (marker_to.x - marker.x) / 2;
            marker.y += (marker_to.y - marker_to.y) / 3;         
        }

        public override void render() {
            int i = 0;
            foreach(string line in text) {
                font_handler.get_text_and_rect(game_manager.renderer, 0, 0, line, "Nunito", out texture1, out rect1);
                SDL.SDL_Rect dest;
                dest.x = 10;
                dest.y = rect1.h * i - i * 10 + 30;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture1, ref rect1, ref dest); 

                marker_to.x = rect1.w + 18 - letter.w * (current_char + 1);
                
                marker.h = 20;
                marker.w = 2;
                
                if(current_line == i)
                    marker.y = letter.h * i - i * 10 + 45;
                
                draw.rect(game_manager.renderer, marker, 255, 255, 255, 255, true);

                i++;
            } 
        }
    }
}