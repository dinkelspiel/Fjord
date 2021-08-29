using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Networking;
using System.Collections.Generic;
using SDL2;
using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;

namespace Proj.Game {
    public class game_server_echo : WebSocketBehavior {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.send("Recieved message from client: " + e.Data);
            if(e.Data == "Connected") {
                game_server_handle.connected_players += 1;
                Send("ID§" + game_server_handle.connected_players.ToString());
                Sessions.Broadcast("NEWPLAYER§" + game_server_handle.connected_players.ToString());
            }

            if(e.Data.Split('§')[0] == "JSON") {
                Sessions.Broadcast("PACKET§" + e.Data.Split('§')[1]);
            }
        }
    }

    public static class game_server_handle {
        public static int connected_players = 0;
    }

    public class game_server : scene {

        string url = "ws://127.0.0.1:5100/";
        server ws_server;

        public game_server() {
            ws_server = new server(url);
            ws_server.wssv.AddWebSocketService<game_server_echo>("/Echo");
        }

        public override void on_load() {
            ws_server.start();
        }

        public override void on_unload() {
            ws_server.stop();
        }

        public override void update() {

        }

        public override void render() {

        }
    }
}