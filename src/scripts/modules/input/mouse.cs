namespace Proj.Modules.Input {
    public static class mouse {
        public static int x, y;
        public static bool lmb, rmb;
        public static bool llmb, lrmb;

        public static bool button_pressed(string button) {
            switch(button) {
                case "lmb":
                    return lmb;
                case "rmb":
                    return rmb;
            }
            return false;
        }

        public static bool button_just_pressed(string button) {
            switch(button) {
                case "lmb":
                    return lmb && !llmb;
                case "rmb":
                    return rmb && !lrmb;
            }
            return false;            
        }

        public static bool any_button_pressed() {
            return lmb || rmb;
        }
    }
}