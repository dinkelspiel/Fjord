using System;
using System.Diagnostics;

namespace Fjord.Modules.Debug {
    public static class Debug
    {
        public static string last_message = "";
        public static int last_message_streak = 0;

        public static void send(dynamic message) {
            message = message.ToString();
            
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            string method = sf.GetMethod().Name;

            string time = DateTime.Now.ToString("HH:mm:ss");
            if(message != last_message) {
                Console.WriteLine("[{0}] {1} -> {2}", time, method, message);  
                last_message_streak = 0;
            } else {
                last_message_streak += 1;
                Console.SetCursorPosition(0, Console.CursorTop -1);
                Console.WriteLine("[{0}] {1}x {2} -> {3}", time, (last_message_streak + 1).ToString(), method, message);  
            }  
            last_message = message;       
        }
    }
}
