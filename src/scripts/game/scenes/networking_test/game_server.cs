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

            if(e.Data.Split('§')[0] == "DISCONNECT") {
                game_server_handle.connected_players -= 1;
                Sessions.Broadcast("DISCONNECTPLAYER§" + e.Data.Split('§')[1]);
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
            game_manager.set_asset_pack("networking_test");
            font_handler.load_font("font", "FiraCode", 22);
        }

        public override void on_unload() {
            ws_server.stop();
        }

        public override void update() {

        }

        public override void render() {
            IntPtr tex;
            SDL.SDL_Rect rect;
            font_handler.get_text_and_rect(game_manager.renderer, "Connected Players: " + game_server_handle.connected_players.ToString(), "font", out tex, out rect, 0, 0);
            int a, w, h;
            uint f;
            SDL.SDL_QueryTexture(tex, out f, out a, out w, out h);
            draw.texture(game_manager.renderer, tex,  10 + w / 2, 20, 0, false);
        }
    }
}