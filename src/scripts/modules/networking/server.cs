using WebSocketSharp;
using WebSocketSharp.Server;
using Fjord.Modules.Debug;

namespace Fjord.Modules.Networking
{
    public class ws_server
    {
        public string url;
        public WebSocketServer wssv;

        public ws_server(string url) {
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