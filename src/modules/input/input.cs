namespace Fjord.Modules.Input {
    public static class input {
        public static string input_state = "general";
    
        public static void set_input_state(string input_state_set) {
            input.input_state = input_state_set;
        }

        public static string get_input_state() {
            return input_state;
        }
    }
}