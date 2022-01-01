using Fjord.Modules.Mathf;

namespace Fjord.Modules.Input {
    public enum mb {
        left,
        right
    }

    public static class mouse {
        public static V2 position = new V2(0, 0);
        public static V2 game_position = new V2(0, 0);
        
        public static bool lmb, rmb;
        public static bool llmb, lrmb;
        
        public static bool wheel_up, wheel_down;

        public static bool button_pressed(mb button, string input_state=null) {
            if(input_state != null) {
                switch(button) {
                    case mb.left:
                        return lmb;
                    case mb.right:
                        return rmb;
                }
                return false;
            } else if(input_state == input.get_input_state()) { 
                switch(button) {
                    case mb.left:
                        return lmb;
                    case mb.right:
                        return rmb;
                }
                return false;
            } 
            return false;
        }

        public static bool button_just_pressed(mb button, string input_state=null) {
            if(input_state != null) {
                switch(button) {
                    case mb.left:
                        return lmb && !llmb;
                    case mb.right:
                        return rmb && !lrmb;
                }
                return false;    
            } else if(input_state == input.get_input_state()) { 
                switch(button) {
                    case mb.left:
                        return lmb && !llmb;
                    case mb.right:
                        return rmb && !lrmb;
                }
                return false;    
            }
            return false;
        }

        public static bool any_button_pressed(string input_state=null) {
            if(input_state != null)
                return lmb || rmb;
            else
                return (lmb || rmb) && input_state == input.get_input_state();
        }

        public static bool scrolling(int key) {
            switch(key) {
                case 0:
                    if(wheel_up) {
                        return true;
                    } 
                    return false;
                case 1:
                    if(wheel_down) {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}