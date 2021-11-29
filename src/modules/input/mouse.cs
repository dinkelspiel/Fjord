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

        public static bool button_pressed(mb button) {
            switch(button) {
                case mb.left:
                    return lmb;
                case mb.right:
                    return rmb;
            }
            return false;
        }

        public static bool button_just_pressed(mb button) {
            switch(button) {
                case mb.left:
                    return lmb && !llmb;
                case mb.right:
                    return rmb && !lrmb;
            }
            return false;            
        }

        public static bool any_button_pressed() {
            return lmb || rmb;
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