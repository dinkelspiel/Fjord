using Fjord.Modules.Graphics;
using Fjord.Modules.Input;
using Fjord.Modules.Debug;
using SDL2;
using System;
using System.Numerics;

namespace Fjord.Modules.Ui
{
    public static class zgui
    {

        static Vector2 corrected_pos = new Vector2(0, 0);
        static bool drag = false;

        public static void init() {
            font_handler.load_font("text", "Cozette", 12);
        }

        public static void window(SDL.SDL_Rect rect, SDL.SDL_Color bg, SDL.SDL_Color o1, SDL.SDL_Color o2, SDL.SDL_Color o3, string font, string title, bool show_label) {
            SDL.SDL_RenderSetLogicalSize(game_manager.renderer, (int)game_manager.window_resolution.X, (int)game_manager.window_resolution.Y);
            
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

                font_handler.get_texture(game_manager.renderer, title, font, out texture, 0, 0, 0, 0, 0, 255);
                //draw.texture_ext(game_manager.renderer, texture, rect.x + 13, rect.y - 5, 0);
                dest.x = rect.x + 13;
                dest.y = rect.y - 5;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture, ref rect1, ref dest);

                font_handler.get_texture(game_manager.renderer, title, font, out texture);
                //draw.texture_ext(game_manager.renderer, texture, rect.x + 11, rect.y - 7, 0);
                dest.x = rect.x + 11;
                dest.y = rect.y - 7;
                dest.w = rect1.w;
                dest.h = rect1.h;
                SDL.SDL_RenderCopy(game_manager.renderer, texture, ref rect1, ref dest);
                
            }

            SDL.SDL_RenderSetLogicalSize(game_manager.renderer, (int)game_manager.resolution.X, (int)game_manager.resolution.Y);
        }

        public static void slider(int x, int y, int width, ref float value, float min_value, float max_value) {
            int ix = x + 100;
            int yi = y + 4;

            if ((mouse.x > ix) && (mouse.x < ix + width) && (mouse.y > yi) && (mouse.y < yi + 6) && (mouse.button_pressed(0)))
		        value = (mouse.x - ix) / ((float)width / (float)max_value);

            SDL.SDL_Rect rect;
            rect.x = ix;
            rect.y = yi;
            rect.w = width;
            rect.h = 6;

            draw.rect(game_manager.renderer, rect, 36, 36, 36, 255, true);
            rect.w = (int)(value * ((float)width / (float)max_value)); 
            draw.rect(game_manager.renderer, rect, 52, 134, 235, 255, true);
        }

        public static void check_box(int x, int y, ref bool value) {
            int w = 10, h = 10;

            if ((mouse.x > x) && (mouse.x < x + w) && (mouse.y > y) && (mouse.y < y + h) && mouse.button_just_pressed(0))
		        value = !value;

            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;

            if(value) {
                draw.rect(game_manager.renderer, rect, 52, 134, 235, 255, true);
            } else {
                draw.rect(game_manager.renderer, rect, 36, 36, 36, 255, true);
            }
        }

        public static void button(int x, int y, int w, int h, ref bool value, string font, string text) {
            if ((mouse.x > x) && (mouse.x < x + w) && (mouse.y > y) && (mouse.y < y + h) && mouse.button_just_pressed(0))
		        value = !value;

            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;

            if(value) {
                draw.rect(game_manager.renderer, rect, 52, 134, 235, 255, true);
            } else {
                draw.rect(game_manager.renderer, rect, 36, 36, 36, 255, true);
            }

            IntPtr tex;
            font_handler.get_texture(game_manager.renderer, text, font, out tex);
            draw.texture(game_manager.renderer, tex, x + w / 2, y + h / 2, 0);
        }

        public static void input_box (int x, int y, int w, int h, string font, ref string value, string default_value, string input_state) {
            if(value == null) 
                return;

            if(input.input_state == input_state && input.get_any_key_just_pressed() > -1) { 
                if(input.get_key(input.get_any_key_just_pressed()).Length == 1) {
                    value += !input.get_key_pressed(input.key_lshift) ? input.get_key(input.get_any_key_just_pressed()) : input.get_key(input.get_any_key_just_pressed()).ToUpper();
                } else if(input.get_key_just_pressed(input.key_backspace)) {
                    if(!input.get_key_pressed(input.key_lctrl)) {
                        if(value.Length > 0) {
                            value = value.Substring(0, value.Length - 1);
                        }
                    } else {
                        if(value.Length > 0) {
                            var valarr = value.Split(" ");
                            if(valarr.Any()) {
                                valarr = valarr.SkipLast(1).ToArray();
                            }
                            value = String.Join(" ", valarr);
                        }
                    }
                } else if(input.get_key_just_pressed(input.key_space)) {
                    value += " ";
                } else if(input.get_key_just_pressed(input.key_backslash)) {
                    value += "/";
                } else if(input.get_key_just_pressed(input.key_period)) {
                    value += ".";
                }
            }

            IntPtr tex;
            uint i;
            int j, wi, hi;
            if(value != "") {
                font_handler.get_texture(game_manager.renderer, value, font, out tex, 0, 0);
            } else {
                font_handler.get_texture(game_manager.renderer, default_value, font, out tex, 0, 0);
            }
            SDL.SDL_QueryTexture(tex, out i, out j, out wi, out hi);

            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = wi < w ? w : wi + 10;
            rect.h = h;

            if(input.input_state == input_state) {
                draw.rect(game_manager.renderer, rect, 52, 134, 235, 255, true);
            } else {
                draw.rect(game_manager.renderer, rect, 36, 36, 36, 255, true);
            }

            draw.texture(game_manager.renderer, tex, x + wi / 2 + 5, y + hi / 2 + 2, 0);
        }

        public static void text_box (int x, int y, int w, int h, string font, ref string value) {
            IntPtr tex;
            uint i;
            int j, wi, hi;
            
            font_handler.get_texture(game_manager.renderer, value, font, out tex, 0, 0);
            
            SDL.SDL_QueryTexture(tex, out i, out j, out wi, out hi);

            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = wi < w ? w : wi + 10;
            rect.h = h;

            draw.texture(game_manager.renderer, tex, x + wi / 2 + 5, y + hi / 2 + 2, 0);
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
            } else {
                drag = false;
            }
        }
    }
}