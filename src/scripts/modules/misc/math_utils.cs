using System.Numerics;
using System;

namespace Proj.Modules.Misc {
    public static class math_uti {
        public static double point_distance(Vector2 origin, Vector2 target) {
            return Math.Sqrt(Math.Pow(target.X - origin.X, 2) + Math.Pow(target.Y - origin.Y, 2));
        }

        public static double point_direction(Vector2 origin, Vector2 target) {
            return Math.Atan2(target.Y-origin.Y, target.X-origin.X);
        }

        public static double lengthdir_x(double length, double angle) {
            return length * Math.Cos(angle * Math.PI / -180);
        }

        public static double lengthdir_y(double length, double angle) {
            return length * Math.Sin(angle * Math.PI / -180);
        }
    }
}