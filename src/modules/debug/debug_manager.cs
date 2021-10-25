using System;
using System.Diagnostics;

namespace Fjord.Modules.Debug {
    public static class Debug
    {
        public static string last_message = "";
        public static int last_message_streak = 0;

        public static void send(dynamic message, string funcoverride=null, string prefix=null) {
            message = message.ToString();
            
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            string method;

            if(funcoverride == null) 
                method = sf.GetMethod().Name;
            else
                method = funcoverride;
            
            string prefixstr;
            
            if(prefix == null) 
                prefixstr = "";
            else
                prefixstr = "[" + prefix + "]";

            string time = DateTime.Now.ToString("HH:mm:ss");
            if(message != last_message) {
                string msg = String.Format("[{0}]{1} {2} -> {3}", time, prefixstr, method, message);
                Console.WriteLine(msg);

                game.log.Add(msg);  

                last_message_streak = 0;
            } else {
                Console.SetCursorPosition(0, Console.CursorTop -1);

                string msg = String.Format(prefixstr + "[{0}]{1} {2}x {3} -> {4}", time, prefixstr, (last_message_streak + 1).ToString(), method, message);
                Console.WriteLine(msg); 

                game.log[game.log.Count - 1] = msg;
                
                last_message_streak += 1;
            }  
            last_message = message;       
        }

        public static void error(dynamic message, bool stop=true) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            send(message, sf.GetMethod().Name, "Error");
            
            if(stop)
                game.stop();
        }

        public static void warn(dynamic message) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            send(message, sf.GetMethod().Name, "Warn");
        }
    }
}
