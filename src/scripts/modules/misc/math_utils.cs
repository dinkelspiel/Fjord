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
    }
}