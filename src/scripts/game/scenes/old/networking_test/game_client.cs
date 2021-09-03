using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;
using Proj.Modules.Networking;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Proj.Game {
    public class player_packet {
        public int id_;
        public Vector2 position;
    }
    
    public class game_client : scene {
        
        Vector2 pos = new Vector2(0, 0);

        public static int id = -1;

        string url = "ws://127.0.0.1:5100/Echo";
        client ws_client;

        public static Dictionary<int, player_packet> players = new Dictionary<int, player_packet>();

        public static void ws_onmessage(object sender, MessageEventArgs e) {
            if(e.Data.Split('§')[0] == "ID") {
                id = Int32.Parse(e.Data.Split('§')[1]);
                Debug.send("Registered as ID: " + id.ToString());
            }

            if(e.Data.Split('§')[0] == "PACKET") {
                player_packet packet = JsonConvert.DeserializeObject<player_packet>(e.Data.Split('§')[1]);
                if(packet.id_ != id) {
                    players[id] = packet;
                }
            }

            if(e.Data.Split('§')[0] == "NEWPLAYER") {
                var asd = Int32.Parse(e.Data.Split('§')[1]);
                if(asd != id) {
                    var add = new player_packet{
                        id_ = asd,
                        position = new Vector2(0, 0)
                    };
                    players.Add(asd, add);
                }
            }

            if(e.Data.Split('§')[0] == "DISCONNECTPLAYER") {
                var asd = Int32.Parse(e.Data.Split('§')[1]);
                players.Remove(asd);
            }
        }

        public game_client() {
            ws_client = new client(url);

            ws_client.ws.OnMessage += ws_onmessage;
        }

        public override void on_load() {
            ws_client.connect();
            ws_client.ws.Send("Connected");
        }

        public override void on_unload() {
            ws_client.ws.Send("DISCONNECT§" + id.ToString());
        }

        public override void update() {
            int move_speed = 4;
            if(input.get_key_pressed(input.key_w)) {
                pos.Y -= move_speed;
            } else if(input.get_key_pressed(input.key_s)) {
                pos.Y += move_speed;
            }

            if(input.get_key_pressed(input.key_a)) {
                pos.X -= move_speed;
            } else if(input.get_key_pressed(input.key_d)) {
                pos.X += move_speed;
            }

            var _export_output = new player_packet {
                id_ = id,
                position = pos
            }; 

            var json_string = JsonConvert.SerializeObject(_export_output);

            ws_client.ws.Send("JSON§"+json_string);
        }

        public override void render() {
            SDL.SDL_Rect plr;
            plr.x = (int)pos.X;
            plr.y = (int)pos.Y;
            plr.w = 20;
            plr.h = 20;
            draw.rect(game_manager.renderer, plr, 0, 255, 255, 255, true);

            foreach(KeyValuePair<int, player_packet> player in players) {
                
                plr.x = (int)player.Value.position.X;
                plr.y = (int)player.Value.position.Y;
                plr.w = 20;
                plr.h = 20;
                draw.rect(game_manager.renderer, plr, 255, 255, 255, 255, true);
            }
        }
    }
}