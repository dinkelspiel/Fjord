using Fjord.Modules.Game;
using Fjord.Modules.Graphics;
using Fjord.Modules.Noise;
using Fjord.Modules.Debug;
using SDL2;
using System.Numerics;
using System.Collections.Generic;

namespace Fjord.Game
{
    public class game : scene
    {
        List<double> x_axis = new List<double> {0, 2.5, 5, 7.5, 10, 12.5, 15, 17.5, 20};
        List<double> y_axis = new List<double> {10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0};
        Vector2[] points = new Vector2[] { new Vector2(0, 0), new Vector2(5, 1), new Vector2(10, 4), new Vector2(15, 8.5f), new Vector2(20, 2) };

        public override void on_load()
        {

            // This is where you load all your scenes 
            // The if statement is so that it doesn't trigger multiple times

            if(!scene_handler.get_scene("game-template")) {

                // Add all scenes
                scene_handler.add_scene("game-template", new game());

                // Load the first scene this can later be called in any file as for example a win condition to switch scene.
                scene_handler.load_scene("game-template");
            }
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();

            var endy = 0;

            for(var i = 0; i < y_axis.Count; i++) {
                var text = y_axis[i].ToString().Split(',');

                draw.text(game_manager.renderer, 10, 40 * i + 80, "default", 48, text[0]);
                if(text.Length > 1)
                    draw.text(game_manager.renderer, 10, 40 * i + 80, "default", 22, "," + text[1]);
                
                endy = 40 * i + 80;
            }

            for(var i = 0; i < x_axis.Count; i++) {
                //draw.text(game_manager.renderer, 40 * i, endy, "default", 48, x_axis[i].ToString());

                var text = x_axis[i].ToString().Split(',');

                draw.text(game_manager.renderer, 70 * i + 70, endy + 70, "default", 35, text[0]);
                if(text.Length > 1)
                    draw.text(game_manager.renderer, 70 * i + 70 + (15 * text[0].Length), endy + 83, "default", 22, "," + text[1]);
            }

            var x2 = 0;
            var y2 = 0;

            for(var i = 0; i < points.Length; i++) {
                var x = (int)(70 * x_axis.IndexOf((int)points[i].X)) + 70;
                var y = (int)(40 * y_axis.IndexOf((int)points[i].Y)) + 110;

                draw.circle(game_manager.renderer, x, y, 5, 255, 255, 255, 255);
                if(i != 0) {
                    draw.line(game_manager.renderer, x, y, x2, y2, 255, 255, 255, 100);
                }

                x2 = x;
                y2 = y;
            }
        }
    }
}