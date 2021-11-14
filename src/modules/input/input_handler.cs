using System.Collections.Generic;

namespace Fjord.Modules.Input {
    public static class input {
        public static bool[] pressed_keys = new bool[77];
        public static bool[] last_frame = new bool[77];
        private static List<string> key_references = new List<string>{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "escape", "backquote", "minus", "equals", "backspace", "tab", "leftbracket", "rightbracket", "backslash", "capslock", "semicolon", "quote", "return", "lshift", "comma", "period", "rshift", "lctrl", "lalt", "space", "ralt", "application", "rctrl", "up", "down", "left", "right"};

        public static string input_state = "general";

        #region keys
        public static readonly int key_a = 0;
        public static readonly int key_b = 1;
        public static readonly int key_c = 2;
        public static readonly int key_d = 3;
        public static readonly int key_e = 4;
        public static readonly int key_f = 5;
        public static readonly int key_g = 6;
        public static readonly int key_h = 7;
        public static readonly int key_i = 8;
        public static readonly int key_j = 9;
        public static readonly int key_k = 10;
        public static readonly int key_l = 11;
        public static readonly int key_m = 12;
        public static readonly int key_n = 13;
        public static readonly int key_o = 14;
        public static readonly int key_p = 15;
        public static readonly int key_q = 16;
        public static readonly int key_r = 17;
        public static readonly int key_s = 18;
        public static readonly int key_t = 19;
        public static readonly int key_u = 20;
        public static readonly int key_v = 21;
        public static readonly int key_w = 22;
        public static readonly int key_x = 23;
        public static readonly int key_y = 24;
        public static readonly int key_z = 25;
        public static readonly int key_1 = 26;
        public static readonly int key_2 = 27;
        public static readonly int key_3 = 28;
        public static readonly int key_4 = 29;
        public static readonly int key_5 = 30;
        public static readonly int key_6 = 31;
        public static readonly int key_7 = 32;
        public static readonly int key_8 = 33;
        public static readonly int key_9 = 34;
        public static readonly int key_0 = 35;
        public static readonly int key_f1 = 36;
        public static readonly int key_f2 = 37;
        public static readonly int key_f3 = 38;
        public static readonly int key_f4 = 39;
        public static readonly int key_f5 = 40;
        public static readonly int key_f6 = 41;
        public static readonly int key_f7 = 42;
        public static readonly int key_f8 = 43;
        public static readonly int key_f9 = 44;
        public static readonly int key_f10 = 45;
        public static readonly int key_f11 = 46;
        public static readonly int key_f12 = 47;
        public static readonly int key_escape = 48;
        public static readonly int key_backquote = 49;
        public static readonly int key_minus = 50;
        public static readonly int key_equals = 51;
        public static readonly int key_backspace = 52;
        public static readonly int key_tab = 53;
        public static readonly int key_leftbracket = 54;
        public static readonly int key_rightbracket = 55;
        public static readonly int key_backslash = 56;
        public static readonly int key_capslock = 57;
        public static readonly int key_semicolon = 58;
        public static readonly int key_quote = 59;
        public static readonly int key_return = 60;
        public static readonly int key_lshift = 61;
        public static readonly int key_comma = 62;
        public static readonly int key_period = 63;
        public static readonly int key_rshift = 64;
        public static readonly int key_lctrl = 65;
        public static readonly int key_lalt = 66;
        public static readonly int key_space = 67;
        public static readonly int key_ralt = 68;
        public static readonly int key_application = 69;
        public static readonly int key_rctrl = 70;
        public static readonly int key_up = 71;
        public static readonly int key_down = 72;
        public static readonly int key_left = 73;
        public static readonly int key_right = 74;
        #endregion

        public static bool get_key_pressed(int key, string input_state_check=null) {
            if(input_state_check == null) {
                return pressed_keys[key];
            } else {
                return pressed_keys[key] && input_state == input_state_check;
            }
        }

        public static bool get_key_just_pressed(int key, string input_state_check=null) {
            if(input_state_check == null) {
                return pressed_keys[key] && !last_frame[key];
            } else {
                return pressed_keys[key] && !last_frame[key] && input_state == input_state_check;
            }
        }

        public static int get_any_key_pressed() {
            for(var i = 0; i < pressed_keys.Length; i++) {
                if(pressed_keys[i]) {
                    return i;
                }
            }
            return -1;
        }

        public static int get_any_key_just_pressed() {
            for(var i = 0; i < pressed_keys.Length; i++) {
                if(pressed_keys[i] && !last_frame[i]) {
                    return i;
                }
            }
            return -1;
        }

        public static void set_input_state(string input_state_set) {
            input_state = input_state_set;
        }

        public static string get_input_state() {
            return input_state;
        }

        public static string get_key(int key) {
            return key_references[key];
        }

        public static int get_key(string key) {
            return key_references.IndexOf(key);
        }
    }
}