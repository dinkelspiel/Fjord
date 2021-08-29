using WebSocketSharp;
using WebSocketSharp.Server;
using Proj.Modules.Debug;

namespace Proj.Modules.Networking
{
    public class server
    {
        public string url;
        public WebSocketServer wssv;

        public server(string url) {
            this.url = url;
            wssv = new WebSocketServer(url);
        }

        public void start() {
            wssv.Start();
        }

        public void stop() {
            wssv.Stop();
        }
    }

    public class template_echo : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Debug.send("Recieved message from client: " + e.Data);
            Send(e.Data);
        }
    }
}