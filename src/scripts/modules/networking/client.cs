using WebSocketSharp;

namespace Proj.Modules.Networking
{
    public class client
    {
        public WebSocket ws;

        public client(string url) {
            ws = new WebSocket(url);
        }

        public void connect() {
            ws.Connect();
        }
    }
}