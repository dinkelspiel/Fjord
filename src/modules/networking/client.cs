using WebSocketSharp;

namespace Fjord.Modules.Networking
{
    public class ws_client
    {
        public WebSocket ws;

        public ws_client(string url) {
            ws = new WebSocket(url);
        }

        public void connect() {
            ws.Connect();
        }
    }
}