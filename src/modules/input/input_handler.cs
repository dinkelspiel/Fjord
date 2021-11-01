namespace Fjord.Modules.Input {
    public static class input {
        public static bool[] pressed_keys = new bool[77];
        public static bool[] last_frame = new bool[77];
        private static List<string> key_references = new List<string>{"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12", "escape", "backquote", "minus", "equals", "backspace", "tab", "leftbracket", "rightbracket", "backslash", "capslock", "semicolon", "quote", "return", "lshift", "comma", "period", "rshift", "lctrl", "lalt", "space", "ralt", "application", "rctrl", "up", "down", "left", "right"};

        public static string input_state = "general";

        #region keys
        public const int key_a = 0;
        public const int key_b = 1;
        public const int key_c = 2;
        public const int key_d = 3;
        public const int key_e = 4;
        public const int key_f = 5;
        public const int key_g = 6;
        public const int key_h = 7;
        public const int key_i = 8;
        public const int key_j = 9;
        public const int key_k = 10;
        public const int key_l = 11;
        public const int key_m = 12;
        public const int key_n = 13;
        public const int key_o = 14;
        public const int key_p = 15;
        public const int key_q = 16;
        public const int key_r = 17;
        public const int key_s = 18;
        public const int key_t = 19;
        public const int key_u = 20;
        public const int key_v = 21;
        public const int key_w = 22;
        public const int key_x = 23;
        public const int key_y = 24;
        public const int key_z = 25;
        public const int key_1 = 26;
        public const int key_2 = 27;
        public const int key_3 = 28;
        public const int key_4 = 29;
        public const int key_5 = 30;
        public const int key_6 = 31;
        public const int key_7 = 32;
        public const int key_8 = 33;
        public const int key_9 = 34;
        public const int key_0 = 35;
        public const int key_f1 = 36;
        public const int key_f2 = 37;
        public const int key_f3 = 38;
        public const int key_f4 = 39;
        public const int key_f5 = 40;
        public const int key_f6 = 41;
        public const int key_f7 = 42;
        public const int key_f8 = 43;
        public const int key_f9 = 44;
        public const int key_f10 = 45;
        public const int key_f11 = 46;
        public const int key_f12 = 47;
        public const int key_escape = 48;
        public const int key_backquote = 49;
        public const int key_minus = 50;
        public const int key_equals = 51;
        public const int key_backspace = 52;
        public const int key_tab = 53;
        public const int key_leftbracket = 54;
        public const int key_rightbracket = 55;
        public const int key_backslash = 56;
        public const int key_capslock = 57;
        public const int key_semicolon = 58;
        public const int key_quote = 59;
        public const int key_return = 60;
        public const int key_lshift = 61;
        public const int key_comma = 62;
        public const int key_period = 63;
        public const int key_rshift = 64;
        public const int key_lctrl = 65;
        public const int key_lalt = 66;
        public const int key_space = 67;
        public const int key_ralt = 68;
        public const int key_application = 69;
        public const int key_rctrl = 70;
        public const int key_up = 71;
        public const int key_down = 72;
        public const int key_left = 73;
        public const int key_right = 74;
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