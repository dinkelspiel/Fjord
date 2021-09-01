using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using Proj.Modules.Graphics;
using Proj.Modules.Misc;
using System.Collections.Generic;
using SDL2;
using System;
using System.Numerics;

namespace Proj.Game {
    public class Point {
        public Vector2 position, prevPosition;
        public bool locked;
    }

    public class Stick {
        public Point pointA, pointB;
        public float length;
    }

    public class rope : scene {

        public bool bSimulate = false;

        List<Point> points = new List<Point>();
        List<Stick> sticks = new List<Stick>();
        public float gravity = 0.4f;

        public bool select = false;
        public Point point_1;
        public Point point_2;

        public void simulate() {
            foreach(Point p in points) {
                if(!p.locked) {
                    Vector2 positionBeforeUpdate = p.position;
                    p.position += p.position - p.prevPosition;
                    p.position += new Vector2(0, -1) * -gravity;
                    p.prevPosition = positionBeforeUpdate;
                }
            }

            for (int i = 0; i < 30; i++) {
                foreach(Stick stick in sticks) {
                    Vector2 stickCentre = (stick.pointA.position + stick.pointB.position) / 2;
                    Vector2 stickDir = Vector2.Normalize(stick.pointA.position - stick.pointB.position);
                    if(!stick.pointA.locked)
                        stick.pointA.position = stickCentre + stickDir * stick.length / 2;
                    if(!stick.pointB.locked)
                        stick.pointB.position = stickCentre - stickDir * stick.length / 2;
                }
            }
        }

        public override void on_load() {

        }

        public override void update() {
            if(bSimulate) 
                simulate();

            if(mouse.button_just_pressed(0) && !math_uti.mouse_inside(0, 0, 160, 110)) {
                if(!input.get_key_pressed(input.key_lshift)) {
                    var p = new Point{
                        position = new Vector2(mouse.x, mouse.y),
                        prevPosition = new Vector2(0, 0),
                        locked = false
                        
                    };
                    points.Add(p);
                } else {
                    foreach(Point point in points) {
                        if(math_uti.point_distance(new Vector2(mouse.x, mouse.y), new Vector2(point.position.X, point.position.Y)) < 10) {
                            point.locked = !point.locked;
                        }
                    }
                }
            }

            if(mouse.button_just_pressed(1)) {
                foreach(Point point in points) {
                    if(select) {
                        if(math_uti.point_distance(new Vector2(mouse.x, mouse.y), new Vector2(point.position.X, point.position.Y)) < 10) {
                            point_1 = point;
                            break;
                        }
                    } else {
                        if(math_uti.point_distance(new Vector2(mouse.x, mouse.y), new Vector2(point.position.X, point.position.Y)) < 10) {
                            point_2 = point;
                            break;
                        }
                    }
                }
            }

            if(input.get_key_just_pressed(input.key_space)) {
                if(point_1 != null && point_2 != null) {
                    var stick = new Stick{
                        pointA = point_1,
                        pointB = point_2,
                        length = 50
                    };
                    point_1 = null;
                    point_2 = null;
                    sticks.Add(stick);
                }
            }

            if(input.get_key_just_pressed(input.key_e)) {
                select = !select;
            }
        }

        public override void render() {
            zgui.button(10, 10, 150, 30, ref bSimulate, "default", "Simulate");
            zgui.button(10, 50, 150, 30, ref select, "default", "Select");

            foreach(Point p in points) {
                if(point_1 == p || point_2 == p) 
                    draw.circle(game_manager.renderer, (int)p.position.X, (int)p.position.Y, 14, 181, 198, 213, 255);
                if(!p.locked)
                    draw.circle(game_manager.renderer, (int)p.position.X, (int)p.position.Y, 10, 255, 255, 255, 255);
                else
                    draw.circle(game_manager.renderer, (int)p.position.X, (int)p.position.Y, 10, 255, 107, 107, 255);
            }

            foreach(Stick stick in sticks) {
                draw.line(game_manager.renderer, (int)stick.pointA.position.X, (int)stick.pointA.position.Y, (int)stick.pointB.position.X, (int)stick.pointB.position.Y, 255, 255, 255, 255);
            }
        }
    }
}