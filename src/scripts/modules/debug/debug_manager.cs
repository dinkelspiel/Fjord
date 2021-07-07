using System;
using SDL2;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Proj.Modules.Debug {
    public static class Debug
    {
        public static string last_message = "";
        public static int last_message_streak = 0;

        public static string get()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }

        public static void send(string method, string message) {
            string time = DateTime.Now.ToString("HH:mm:ss");
            if(message != last_message) {
                Console.WriteLine("[{0}] {1} -> {2}", time, method, message);  
                last_message_streak = 0;
            } else {
                last_message_streak += 1;
                Console.SetCursorPosition(0, Console.CursorTop -1);
                Console.WriteLine("[{0}] {1}x {2} -> {3}", time, last_message_streak.ToString(), method, message);  
            }  
            last_message = message;       
        }
    }
}
