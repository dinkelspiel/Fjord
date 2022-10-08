using System;
using System.Diagnostics;

#nullable enable

namespace Fjord.Modules.Debug {
    public static class Debug
    {
        public static string last_message = "";
        public static int last_message_streak = 0;

        public static bool ShowEnginePrefix = true;

        public static void Assert(bool condition, string message) {
            if(!condition) {
                Error(message, true);
            }
        }

        public static void Send(dynamic message, string? funcoverride=null, string? prefix=null) {
            message = message.ToString();
            
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            string method;

            if(sf is null) {
                return;
            }

            if(sf.GetMethod() is null) {
                return;
            }

            if(funcoverride is null) 
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

                Game.Log.Add(msg);  

                last_message_streak = 0;
            } else {
                Console.SetCursorPosition(0, Console.CursorTop -1);

                string msg = String.Format(prefixstr + "[{0}]{1} {2}x {3} -> {4}", time, prefixstr, (last_message_streak + 2).ToString(), method, message);
                Console.WriteLine(msg); 

                Game.Log[Game.Log.Count - 1] = msg;
                
                last_message_streak += 1;
            }  
            last_message = message;       
        }

        public static void Error(dynamic message, bool stop=true) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Send(message, sf.GetMethod().Name, "Error");
            
            if(stop)
                Game.Stop(1);
        }

        public static void Warn(dynamic message) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            Send(message, sf.GetMethod().Name, "Warn");
        }



        public static void SendInternal(dynamic message, string? funcoverride=null, string? prefix=null) {
            if(!ShowEnginePrefix) {
                var _st = new StackTrace();
                var _sf = _st.GetFrame(1);
                Send(message, _sf.GetMethod().Name, prefix);
                return;
            }

            message = message.ToString();
            
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            string method;

            if(funcoverride is null) 
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
                string msg = String.Format("[{0}]{1} Fjord -> {2}", time, prefixstr, message);
                Console.WriteLine(msg);

                Game.Log.Add(msg);  

                last_message_streak = 0;
            } else {
                Console.SetCursorPosition(0, Console.CursorTop -1);

                string msg = String.Format(prefixstr + "[{0}]{1} {2}x Fjord -> {3}", time, prefixstr, (last_message_streak + 1).ToString(), message);
                Console.WriteLine(msg); 

                Game.Log[Game.Log.Count - 1] = msg;
                
                last_message_streak += 1;
            }  
            last_message = message;       
        }

        public static void ErrorInternal(dynamic message, bool stop=true) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            SendInternal(message, sf.GetMethod().Name, "Error");
            
            if(stop)
                Game.Stop(1);
        }

        public static void WarnInternal(dynamic message) {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            SendInternal(message, sf.GetMethod().Name, "Warn");
        }
    }
}