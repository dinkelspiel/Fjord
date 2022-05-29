using System.Collections.Generic;

namespace Fjord.Modules.Input {
    public static class keyboard {
        #nullable enable
        public static bool[] pressed_keys = new bool[78];
        public static bool[] last_frame = new bool[78];
        private static List<string> key_references = new List<string>{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "escape", "backquote", "minus", "equals", "backspace", "tab", "leftbracket", "rightbracket", "backslash", "capslock", "semicolon", "quote", "return", "lshift", "comma", "period", "slash", "rshift", "lctrl", "lalt", "space", "ralt", "application", "rctrl", "up", "down", "left", "right"};

        #region keys

        #endregion

        public static bool pressed(key key, string? input_state_check=null) {
            if(input_state_check == null) {
                return pressed_keys[(int)key];
            } else {
                return pressed_keys[(int)key] && input.input_state == input_state_check;
            }
        }

        public static bool just_pressed(key key, string? input_state_check=null) {
            if(input_state_check == null) {
                return pressed_keys[(int)key] && !last_frame[(int)key];
            } else {
                return pressed_keys[(int)key] && !last_frame[(int)key] && input.input_state == input_state_check;
            }
        }

        public static key any_pressed(string? input_state_check=null) {
            if(input_state_check != null && input_state_check != input.input_state)
                return key.none;

            for(var i = 0; i < pressed_keys.Length; i++) {
                if(pressed_keys[i]) {
                    return (key)i;
                }
            }
            return key.none;
        }

        public static key any_just_pressed(string? input_state_check=null) {
            if(input_state_check != null && input_state_check != input.input_state)
                return key.none;

            for(var i = 0; i < pressed_keys.Length; i++) {
                if(pressed_keys[i] && !last_frame[i]) {
                    return (key)i;
                }
            }
            return key.none;
        }
        
        public static string get_key(key key) {
            return key_references[(int)key];
        }

        public static int get_key(string key) {
            return key_references.IndexOf(key);
        }

        public static string keyboard_input() {
            if(keyboard.pressed(key.lshift)) {
                if(keyboard.any_just_pressed() != key.none) {
                    switch(keyboard.any_just_pressed()) {
                        case key.one:
                            return "!";
                        case key.two:
                            return "@";
                        case key.three:
                            return "#";
                        case key.four:
                            return "$";
                        case key.five:
                            return "%";
                        case key.six:
                            return "^";
                        case key.seven:
                            return "&";
                        case key.eight:
                            return "*";
                        case key.nine:
                            return "(";
                        case key.zero:
                            return ")";
                        case key.leftbracket:
                            return "{";
                        case key.rightbracket:
                            return "}";
                        case key.semicolon:
                            return ":";
                        case key.quote:
                            return "\"";
                        case key.minus:
                            return "_";
                        case key.period:
                            return ">";
                        case key.comma:
                            return "<";
                        case key.equals:
                            return "+";
                        case key.backslash:
                            return "|";
                        case key.slash:
                            return "?";
                        default:
                            if(keyboard.get_key(keyboard.any_just_pressed()).Length == 1)
                                return keyboard.get_key(keyboard.any_just_pressed()).ToUpper();
                            break;
                    }
                }
            } else {
                if(keyboard.any_just_pressed() != key.none) {
                    switch(keyboard.any_just_pressed()) {
                        case key.space:
                            return " ";
                        case key.tab:
                            return "    ";
                        case key.leftbracket:
                            return "[";
                        case key.rightbracket:
                            return "]";
                        case key.semicolon:
                            return";";
                        case key.quote:
                            return "'";
                        case key.minus:
                            return "-";
                        case key.period:
                            return ".";
                        case key.comma:
                            return ",";
                        case key.equals:
                            return "=";
                        case key.backslash:
                            return "\\";
                        case key.slash:
                            return "/";
                        default: 
                            if(keyboard.get_key(keyboard.any_just_pressed()).Length == 1)
                                return keyboard.get_key(keyboard.any_just_pressed()).ToLower();
                            break;
                    }
                }
            }
            return "";
        }
        #nullable disable
    }
}