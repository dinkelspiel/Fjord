using System.Reflection;
using System.ComponentModel;
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

        public static void text(string text, string id=null) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.text'!");
            draw.text(windows[current_window].offset + windows[current_window].position, font_id, font_size, text, windows[current_window].color_text);
            windows[current_window].offset.y += draw.get_text_rect(windows[current_window].offset, font_id, font_size, text).w + 10;
            
            if(windows[current_window].offset.y > windows[current_window].size.y - 20)
                windows[current_window].size.y = windows[current_window].offset.y;
        }

        public static bool button(string text, string id=null) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.button'!");
            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, text).z + 80, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, text).w + 20);
            
            V4 color = windows[current_window].color_foreground;
            if(helpers.mouse_inside(rect) && mouse.button_pressed(mb.left) && (selected_input == "" || selected_input == (id == null ? text : id))) {
                color = windows[current_window].color_darkerforeground;
                selected_input = (id == null ? text : id);
            } else if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left)) {
                selected_input = (id == null ? text : id);
            } else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, text, windows[current_window].color_text);

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 20)
                windows[current_window].size.y = windows[current_window].offset.y;

            if(rect.z + 40 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 40;

            if(selected_input == (id == null ? text : id))
                return true;
            else
                return false;
        }

        public static void input_box(string title, ref string value, string id=null) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.input_box'!");

            if(value == null)
                return;

            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             Math.Clamp(draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, value).z + 20, 320, 600), 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, "value").w + 20);

            if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left))
                selected_input = selected_input == (id == null ? title : id) ? "" : (id == null ? title : id);

            if(selected_input == (id == null ? title : id)) {
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

            if(mouse.button_just_pressed(mb.left) && selected_input == (id == null ? title : id) && !helpers.mouse_inside(rect)) {
                selected_input = "";
            }                                                                                                                                                               

            V4 color = windows[current_window].color_foreground;
            if(selected_input == (id == null ? title : id))
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value, windows[current_window].color_text);
            draw.text(new V2(rect.x + rect.z + 10, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, title, windows[current_window].color_text);

            V4 text_rect = draw.get_text_rect(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value);
            if(selected_input == (id == null ? title : id))
                draw.rect(new V4(windows[current_window].offset.x + windows[current_window].position.x + text_rect.z + 6, windows[current_window].offset.y + windows[current_window].position.y + 2, 4, rect.w - 4), windows[current_window].color_text);

            if(windows[current_window].size.x - 20 < rect.z + 10 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z)
                windows[current_window].size.x = rect.z + 30 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z;

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 10)
                windows[current_window].size.y = windows[current_window].offset.y;

            if(rect.z + 20 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 40;
        }

        public static void slider_int(string title, ref int value, V2 range, string id=null) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.slider_int'!");

            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             320, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, "value").w + 20);

            if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left))
                selected_input = (id == null ? title : id);

            V4 color = windows[current_window].color_foreground;
            if(selected_input == (id == null ? title : id))
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            int width = draw.get_text_rect(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value.ToString()).z;
            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + (rect.z / 2) - (width / 2) + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, value.ToString(), windows[current_window].color_text);
            draw.text(new V2(rect.x + rect.z + 10, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, title, windows[current_window].color_text);

            if(windows[current_window].size.x - 20 < rect.z + 10 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z)
                windows[current_window].size.x = rect.z + 30 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z;

            // Calculate handle
            // TODO: Allow values between 0 and 100

            V2 range_norm_zero = new V2(0, 0);

            int offset = 0;
            float normalize_value = 0;

            if(Math.Abs(range.x) != range.x) {
                offset = Math.Abs(range.x);
            } else {
                offset = -range.x;
            }

            range_norm_zero.y = range.y + offset;
            normalize_value = (range_norm_zero.y / 100);
            range_norm_zero = new V2((int)(range_norm_zero.x / normalize_value), (int)(range_norm_zero.y / normalize_value));

            int value_norm = value;

            value_norm += offset;
            value_norm = (int)(value_norm / normalize_value);

            if(selected_input == (id == null ? title : id))
                draw.rect(new V4(rect.x + (int)(value_norm * 3.2 - 2), rect.y, 4, rect.w), windows[current_window].color_text);

            // Calculate click
            // TODO: Allow values between 0 and 100

            if(mouse.button_just_pressed(mb.left) && selected_input == (id == null ? title : id) && !helpers.mouse_inside(rect)) {
                selected_input = "";
            }

            if(mouse.button_pressed(mb.left) && selected_input == (id == null ? title : id)) {
                V2 fixed_mousepos = new V2(mouse.position.x - rect.x, mouse.position.y - rect.y);
                fixed_mousepos.x = (int)(fixed_mousepos.x / 3.2f);
                value = (int)(fixed_mousepos.x * normalize_value) - offset;
                value = Math.Clamp(value, range.x, range.y);
            }

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 10)
                windows[current_window].size.y = windows[current_window].offset.y;

            if(rect.z + 20 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 20;
        }

        public static void slider_float(string title, ref float value, V2f range, int decimal_points = 2, string id=null) {
            Debug.Debug.assert(current_window != "", "Window must be set in 'fui.slider_float'!");

            V4 rect = new V4(windows[current_window].offset.x + windows[current_window].position.x, 
                             windows[current_window].offset.y + windows[current_window].position.y, 
                             320, 
                             draw.get_text_rect(windows[current_window].offset, font_id, font_size - 12, "value").w + 20);

            if(helpers.mouse_inside(rect) && mouse.button_just_pressed(mb.left))
                selected_input = (id == null ? title : id);

            V4 color = windows[current_window].color_foreground;
            if(selected_input == (id == null ? title : id))
                color = windows[current_window].color_darkerforeground;
            else if(helpers.mouse_inside(rect)) 
                color = new V4(windows[current_window].color_foreground.x + 20, windows[current_window].color_foreground.y + 20, windows[current_window].color_foreground.z + 20, 255); 

            draw.rect(rect, color);
            string new_value = value.ToString();
            if(decimal_points > 0) {
                if(value.ToString().Contains(".")) {
                    new_value = value.ToString().Split(".")[0] + ".";
                    string decimal_val = value.ToString().Split(".")[1];
                    while(decimal_val.Length < decimal_points + 1) {
                        decimal_val += "0";
                    }
                    decimal_val = decimal_val.Substring(0, decimal_points);
                    new_value += decimal_val;
                } else {
                    new_value += ".";
                    for(var i = 0; i < decimal_points; i ++) {
                        new_value += "0";
                    }
                }
            } else {
                if(value.ToString().Contains(".")) {
                    new_value = value.ToString().Split()[0];
                }
            }

            int width = draw.get_text_rect(new V2(windows[current_window].offset.x + windows[current_window].position.x + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, new_value.ToString()).z;

            draw.text(new V2(windows[current_window].offset.x + windows[current_window].position.x + (rect.z / 2) - (width / 2) + 4, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, new_value, windows[current_window].color_text);
            
            draw.text(new V2(rect.x + rect.z + 10, windows[current_window].offset.y + windows[current_window].position.y + 2), font_id, font_size, title, windows[current_window].color_text);

            if(windows[current_window].size.x - 20 < rect.z + 10 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z)
                windows[current_window].size.x = rect.z + 30 + draw.get_text_rect(new V2(0, 0), font_id, font_size, title).z;

            // Calculate handle
            // TODO: Allow values between 0 and 100

            V2 range_norm_zero = new V2(0, 0);

            int offset = 0;
            int normalize_value = 0;

            if(Math.Abs(range.x) != range.x) {
                offset = Math.Abs((int)range.x);
            } else {
                offset = -(int)range.x;
            }

            range_norm_zero.y = (int)range.y + offset;
            normalize_value = (range_norm_zero.y / 100);
            range_norm_zero /= normalize_value;

            float value_norm = value;

            value_norm += offset;
            value_norm /= normalize_value;

            if(selected_input == (id == null ? title : id))
                draw.rect(new V4(rect.x + (int)(value_norm * 3.2 - 2), rect.y, 4, rect.w), windows[current_window].color_text);

            // Calculate click
            // TODO: Allow values between 0 and 100

            if(mouse.button_just_pressed(mb.left) && selected_input == (id == null ? title : id) && !helpers.mouse_inside(rect)) {
                selected_input = "";
            }

            if(mouse.button_pressed(mb.left) && selected_input == (id == null ? title : id)) {
                V2f fixed_mousepos = new V2f(mouse.position.x - rect.x, mouse.position.y - rect.y);
                fixed_mousepos.x = (fixed_mousepos.x / 3.2f);
                value = (fixed_mousepos.x * normalize_value) - offset;
                value = Math.Clamp(value, range.x, range.y);
            }

            windows[current_window].offset.y += rect.w + 10;
            if(windows[current_window].offset.y > windows[current_window].size.y - 10)
                windows[current_window].size.y = windows[current_window].offset.y;

            if(rect.z + 20 > windows[current_window].size.x)
                windows[current_window].size.x = rect.z + 20;
        }
    }
}