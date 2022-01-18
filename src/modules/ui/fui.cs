using System.Collections.Immutable;
using System.Security.AccessControl;
using System;
using System.Linq;
using System.Collections.Generic;
using Fjord;
using Fjord.Modules.Debug;
using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Input;
using Fjord.Modules.Mathf;
using Fjord.Modules.Sound;
using static SDL2.SDL;


namespace Fjord.Modules.Ui {
    public static class fui {

        public enum window_type {
            static_window
        }

        public class window_options {
            public bool window_bar = false;
            public window_type type = window_type.static_window;
        }

        class window {
            public string title = "";
            public window_type type = window_type.static_window;
            public V2 position = new V2(0, 0);
            public V2 size = new V2(0, 0);

            public string input_state = "general";

            public V2 offset = new V2(20, 20);

            public V4 color_background = new V4(20, 20, 20, 255);
            public V4 color_text =  new V4(255, 255, 255, 255);
            public V4 color_foreground = new V4(40, 75, 120, 255);
            public V4 color_darkerforeground = new V4(30, 50, 80, 255);
        }

        private static Dictionary<string, window> windows = new Dictionary<string, window>();
        private static string current_window = "";

        private static string selected_input = "";

        public static int font_size = 24;
        public static string font_id = "";

        public static void begin(string title, window_options options) {
            Debug.Debug.assert(current_window == "", "Window must not be set when calling 'fui.begin'!");

            current_window = title;
            if(!windows.Keys.ToImmutableArray().Contains(title)) {
                windows.Add(title, new window() {
                    title = title,
                    type = options.type
                });
            } else {
                windows[title].offset = new V2(10, 10);
            }

            draw.rect(new V4(windows[current_window].position.x, windows[current_window].position.y, windows[current_window].size.x, windows[current_window].size.y), windows[current_window].color_background);
            if(options.window_bar) {
                int window_bar_height = font_size + 6;

                V4 rect = new V4(windows[current_window].position.x, windows[current_window].position.y - window_bar_height, windows[current_window].size.x, window_bar_height);
                draw.rect(rect, windows[current_window].color_foreground);
                draw.text(new V2(windows[current_window].position.x + 3, windows[current_window].position.y - window_bar_height + 3), font_id, font_size, title);
            }
        }

        public static void end() {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.end'!");
            current_window = "";
        }

        public static void set_position(V2 position) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.set_position'!");
            windows[current_window].position = position;
        }

        public static void set_size(V2 size) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.set_size'!");
            windows[current_window].size = size;
        }

        public static void set_font(string font) {
            font_id = font;
        }

        public static void set_input_state(string input_state) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.set_input_state'!");
            windows[current_window].input_state = input_state;
        }

        public static void text(string text) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.text'!");
            draw.text(windows[current_window].offset + windows[current_window].position, font_id, font_size, text, windows[current_window].color_text);
            windows[current_window].offset.y += draw.get_text_rect(windows[current_window].offset, font_id, font_size, text).w + 10;
            
            if(windows[current_window].offset.y > windows[current_window].size.y - 20)
                windows[current_window].offset.y += 40;
        }

        public static bool button(string text) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.button'!");
            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, text).z + 80, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, text).w + 20);
            
            V4 color = windows[current_window].color_foreground;
            if(helpers.mouse_inside(rect) && mouse.button_pressed(mb.left))
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, text, windows[current_window].color_text);

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 20)
                windows[current_window].size.y += 40;

            if(rect.z + 40 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 40;

            if(mouse.llmb && !mouse.lmb)
                return true;
            else
                return false;
        }

        public static void input_box(string title, ref string value) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.input_box'!");

            if(value == null)
                return;

            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             Math.Clamp(draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, value).z + 20, 320, 600), 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, "value").w + 20);

            if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left))
                selected_input = selected_input == title ? "" : title;

            if(selected_input == title) {
                if(input.get_key_just_pressed(input.key_backspace)) {
                    if(!input.get_key_pressed(input.key_lctrl))
                        if(value.Length > 0)
                            value = value.Substring(0, value.Length - 1);
                    else {
                        if(value.Length > 0) {
                            var valarr = value.Split(" ");
                            if(valarr.Any())
                                valarr = valarr.SkipLast(1).ToArray();
                            value = String.Join(" ", valarr);
                        }
                    }
                } else {
                    value += input.get_human_input();
                }
            }

            V4 color = windows[current_window].color_foreground;
            if(selected_input == title)
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value, windows[current_window].color_text);

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 10)
                windows[current_window].size.y += 20;

            if(rect.z + 20 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 20;
        }

        public static void slider_int(string title, ref int value, V2 range) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.slider_int'!");

            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             320, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, "value").w + 20);

            if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left))
                selected_input = selected_input == title ? "" : title;

            V4 color = windows[current_window].color_foreground;
            if(selected_input == title)
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);

            int width = draw.get_text_rect(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value.ToString()).z;
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + (rect.z / 2) - (width / 2) + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value.ToString(), windows[current_window].color_text);

            int normalize_val = 0;
            if(range.x != Math.Abs(range.x)) {
                normalize_val = range.x;
            } else {
                normalize_val = -range.x;
            }

            V2 new_range = new V2(range.x + normalize_val, range.y + normalize_val);
            int new_value = value;

            new_value += normalize_val;

            int multiplier = (new_range.y / 100);

            new_range.y = new_range.y / multiplier;
            new_value /= multiplier;

            new_value = (int)(new_value * 3.2f);

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 10)
                windows[current_window].size.y += 20;

            if(rect.z + 20 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 20;
        }
    }
}