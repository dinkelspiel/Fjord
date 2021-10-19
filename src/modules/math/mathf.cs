using System.Numerics;
using System;
using Fjord.Modules.Input;

namespace Fjord.Modules.Mathf {
    public static class Mathf {

        public static double Lerp(double firstFloat, double secondFloat, double by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        public static V2f Lerp(V2f firstVector, V2f secondVector, double by)
        {
            double retX = (double)Lerp((double)firstVector.x, (double)secondVector.x, by);
            double retY = (double)Lerp(firstVector.y, secondVector.y, by);
            return new V2f((float)retX, (float)retY);
        }

        public static V2 Lerp(V2 firstVector, V2 secondVector, double by)
        {
            double retX = (double)Lerp((double)firstVector.x, (double)secondVector.x, by);
            double retY = (double)Lerp(firstVector.y, secondVector.y, by);
            return new V2((int)retX, (int)retY);
        }

        public static double get_hypot(V2 origin, V2 target) {
            return Math.Sqrt(Math.Pow(target.x - origin.x, 2) + Math.Pow(target.y - origin.y, 2));
        }

        public static double get_dir(V2 origin, V2 target) {
            return radtodeg(Math.Atan2(target.y-origin.y, target.x-origin.x));
        }

        public static double lengthdir_x(double length, double angle) {
            angle = 180 - angle + 180;
            return length * Math.Cos(angle * Math.PI / -180);
        }

        public static double lengthdir_y(double length, double angle) {
            angle = 180 - angle + 180;
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

        public static int angle_difference(int angle1, int angle2) {
            var a = angle2 - angle1;
            a += (a>180) ? -360 : (a<-180) ? 360 : 0;
            return a;
        }
    }
}