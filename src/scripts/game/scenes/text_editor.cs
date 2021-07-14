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

        IntPtr texture1;
        SDL.SDL_Rect rect1;

        public text_editor() {
            font_handler.load_font("Cozette", "Cozette", 32);
            text.Add("");
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
                } else if(input.get_any_key_just_pressed() == input.key_tab) {
                    text[current_line] += "    ";
                } else if(input.get_any_key_just_pressed() == input.key_up) {
                    if(current_line > 0) {
                        current_line -= 1;
                    }
                } else if(input.get_any_key_just_pressed() == input.key_down) {
                    if(current_line < text.Count - 1) {
                        current_line += 1;
                    }
                }
            }
        }

        public override void render() {
            int i = 0;
            foreach(string line in text) {
                font_handler.get_text_and_rect(game_manager.renderer, 0, 0, line, "Cozette", out texture1, out rect1);
                SDL.SDL_Rect dest;
                dest.x = 10;
                dest.y = rect1.h * i + 10;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture1, ref rect1, ref dest); 

                SDL.SDL_Rect marker;
                marker.x = rect1.w + 15;
                marker.y = dest.h * i + dest.h;
                marker.h = 3;
                marker.w = 12;

                if(i == current_line) {
                    draw.rect(game_manager.renderer, marker, 255, 255, 255, 255, true);
                }

                i++;
            } 
        }
    }
}