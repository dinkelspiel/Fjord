using Fjord.Modules.Mathf;

namespace Fjord.Modules.Input {
    public enum mb {
        left,
        right,
        scroll_up,
        scroll_down
    }

    public static class mouse {
        public static V2 screen_position = new V2(0, 0);
        public static V2 game_position = new V2(0, 0);
        
        public static bool lmb, rmb;
        public static bool llmb, lrmb;
        
        public static bool wheel_up, wheel_down;

        public static bool pressed(mb button, string input_state=null) {
            if(input_state == null) {
                switch(button) {
                    case mb.left:
                        return lmb;
                    case mb.right:
                        return rmb;
                    case mb.scroll_up:
                        return wheel_up;
                    case mb.scroll_down:
                        return wheel_down;
                }
                return false;
            } else if(input_state == input.get_input_state()) { 
                switch(button) {
                    case mb.left:
                        return lmb;
                    case mb.right:
                        return rmb;
                    case mb.scroll_up:
                        return wheel_up;
                    case mb.scroll_down:
                        return wheel_down;
                }
                return false;
            } 
            return false;
        }

        public static bool just_pressed(mb button, string input_state=null) {
            if(input_state == null) {
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

        public static bool just_released(mb button, string input_state=null) {
            if(input_state == null) {
                switch(button) {
                    case mb.left:
                        return !lmb && llmb;
                    case mb.right:
                        return !rmb && lrmb;
                }
                return false;    
            } else if(input_state == input.get_input_state()) { 
                switch(button) {
                    case mb.left:
                        return !lmb && llmb;
                    case mb.right:
                        return !rmb && lrmb;
                }
                return false;    
            }
            return false;
        }

        public static bool any_pressed(string input_state=null) {
            if(input_state == null)
                return lmb || rmb;
            else
                return (lmb || rmb) && input_state == input.get_input_state();
        }
    }
}