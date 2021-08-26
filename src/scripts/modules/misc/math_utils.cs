using System.Numerics;
using System;
using Proj.Modules.Input;

namespace Proj.Modules.Misc {
    public static class math_uti {
        public static double point_distance(Vector2 origin, Vector2 target) {
            return Math.Sqrt(Math.Pow(target.X - origin.X, 2) + Math.Pow(target.Y - origin.Y, 2));
        }

        public static double point_direction(Vector2 origin, Vector2 target) {
            return radtodeg(Math.Atan2(target.Y-origin.Y, target.X-origin.X));
        }

        public static double lengthdir_x(double length, double angle) {
            return length * Math.Cos(angle * Math.PI / -180);
        }

        public static double lengthdir_y(double length, double angle) {
            return length * Math.Sin(angle * Math.PI / -180);
        }

        public static double radtodeg(double radians) {
            return (180 / Math.PI) * radians;
        }

        public static double degtorad(double degrees) {
            return (System.Math.PI / 180) * degrees;
        }

        public static bool mouse_inside(int x, int y, int w, int h) {
            if((mouse.x > x) && (mouse.x < x + w) && (mouse.y > y) && (mouse.y < y + h)) {
                return true;
            }
            return false;
        }
    }
}