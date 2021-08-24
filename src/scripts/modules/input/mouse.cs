namespace Proj.Modules.Input {
    public static class mouse {
        public static int x, y;
        public static bool lmb, rmb;
        public static bool llmb, lrmb;

        public static bool button_pressed(int button) {
            switch(button) {
                case 0:
                    return lmb;
                case 1:
                    return rmb;
            }
            return false;
        }

        public static bool button_just_pressed(int button) {
            switch(button) {
                case 0:
                    return lmb && !llmb;
                case 1:
                    return rmb && !lrmb;
            }
            return false;            
        }

        public static bool any_button_pressed() {
            return lmb || rmb;
        }
    }
}