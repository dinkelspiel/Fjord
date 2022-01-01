using System;
using System.Linq;
using System.Linq.Expressions;
using Fjord;
using Fjord.Modules.Debug;
using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Input;
using Fjord.Modules.Mathf;
using Fjord.Modules.Sound;
using static SDL2.SDL;

namespace Fjord.Modules.Ui
{
    public static class devgui
    {
        private static class MemberInfoGetting
        {
            public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
            {
                MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
                return expressionBody.Member.Name;
            }
        }

        private static string selected_input = "";

        private static V4 on_color = new V4(192, 175, 250, 255);
        private static V4 off_color = new V4(255, 255, 255, 255);
        private static V4 text_color = new V4(0, 0, 0, 255);

        public static void slider(V4 rect, ref int value, int max, V4 off, V4 on) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_pressed(0))) {
		        value = (int)((mouse.position.x - rect.x) / ((float)rect.w / max));
            }

            value = Math.Clamp(value, 1, max);

            draw.rect(rect, off);
            rect.w = (int)(value * ((float)rect.w / (float)max)); 
            draw.rect(rect, on);
        }

        public static void slider(V4 rect, ref int value, int max) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_pressed(0))) {
		        value = (int)((mouse.position.x - rect.x) / ((float)rect.w / max));
            }

            value = Math.Clamp(value, 1, max);

            draw.rect(rect, off_color);
            rect.w = (int)(value * ((float)rect.w / (float)max)); 
            draw.rect(rect, on_color);
        }

        public static void button(V4 rect, ref bool value, string font, string text, V4 off, V4 on, V4 text_color) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) 
                value = !value;

            if(!value)
                draw.rect(rect, off);
            else 
                draw.rect(rect, on);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, text, text_color);
        }

        public static void button(V4 rect, ref bool value, string font, string text) {
            
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) 
                value = !value;

            if(!value)
                draw.rect(rect, off_color);
            else 
                draw.rect(rect, on_color);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, text, text_color);
        }

        public static void num_input_box(V4 rect, ref int value, string id, string font, V4 off, V4 on, V4 text_color) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) 
                selected_input = selected_input == id ? "" : id;

            if(selected_input == id) {
                switch(input.get_any_key_just_pressed()) {
                    case input.key_backspace:
                        Int32.TryParse(value.ToString().Length > 0 ? value.ToString().Substring(0, value.ToString().Length - 1) : "0", out value);
                        break;
                    case input.key_0:
                        Int32.TryParse(value.ToString() + "0", out value);
                        break;
                    case input.key_1:
                        Int32.TryParse(value.ToString() + "1", out value);
                        break;
                    case input.key_2:
                        Int32.TryParse(value.ToString() + "2", out value);
                        break;
                    case input.key_3:
                        Int32.TryParse(value.ToString() + "3", out value);
                        break;
                    case input.key_4:
                        Int32.TryParse(value.ToString() + "4", out value);
                        break;
                    case input.key_5:
                        Int32.TryParse(value.ToString() + "5", out value);
                        break;
                    case input.key_6:
                        Int32.TryParse(value.ToString() + "6", out value);
                        break;
                    case input.key_7:
                        Int32.TryParse(value.ToString() + "7", out value);
                        break;
                    case input.key_8:
                        Int32.TryParse(value.ToString() + "8", out value);
                        break;
                    case input.key_9:
                        Int32.TryParse(value.ToString() + "9", out value);
                        break;
                }
            }

            if(selected_input != id)
                draw.rect(rect, off);
            else 
                draw.rect(rect, on);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, value.ToString(), text_color);
        }

        public static void num_input_box(V4 rect, ref int value, string id, string font) {
            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) 
                selected_input = selected_input == id ? "" : id;

            if(selected_input == id) {
                switch(input.get_any_key_just_pressed()) {
                    case input.key_backspace:
                        Int32.TryParse(value.ToString().Length > 0 ? value.ToString().Substring(0, value.ToString().Length - 1) : "0", out value);
                        break;
                    case input.key_0:
                        Int32.TryParse(value.ToString() + "0", out value);
                        break;
                    case input.key_1:
                        Int32.TryParse(value.ToString() + "1", out value);
                        break;
                    case input.key_2:
                        Int32.TryParse(value.ToString() + "2", out value);
                        break;
                    case input.key_3:
                        Int32.TryParse(value.ToString() + "3", out value);
                        break;
                    case input.key_4:
                        Int32.TryParse(value.ToString() + "4", out value);
                        break;
                    case input.key_5:
                        Int32.TryParse(value.ToString() + "5", out value);
                        break;
                    case input.key_6:
                        Int32.TryParse(value.ToString() + "6", out value);
                        break;
                    case input.key_7:
                        Int32.TryParse(value.ToString() + "7", out value);
                        break;
                    case input.key_8:
                        Int32.TryParse(value.ToString() + "8", out value);
                        break;
                    case input.key_9:
                        Int32.TryParse(value.ToString() + "9", out value);
                        break;
                }
            }

            if(selected_input != id)
                draw.rect(rect, off_color);
            else 
                draw.rect(rect, on_color);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, value.ToString(), text_color);
        }

        public static void input_box (V4 rect, string font, ref string value, string input_state, string id, string default_value, V4 off, V4 on, V4 text_color) {
            if(value == null) 
                return;

            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) {
                selected_input = selected_input == id ? "" : id;

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

            if(selected_input != id)
                draw.rect(rect, off);
            else 
                draw.rect(rect, on);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, value != "" ? value : default_value, text_color);
        }

        public static void input_box (V4 rect, string font, ref string value, string input_state, string id, string default_value) {
            if(value == null) 
                return;

            if (helpers.mouse_inside(rect, 2) && (mouse.button_just_pressed(mb.left))) {
                selected_input = selected_input == id ? "" : id;

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

            if(selected_input != id)
                draw.rect(rect, off_color);
            else 
                draw.rect(rect, on_color);

            draw.text(new V2(rect.x + 5, rect.y + 5), font, rect.w - 10, value != "" ? value : default_value, text_color);
        }
    }
}