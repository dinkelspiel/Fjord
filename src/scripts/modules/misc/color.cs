using System.Numerics;
using System;
using Proj.Modules.Debug;

namespace Proj.Modules.Misc {
    public class float_color {
        public float r, g, b, a;
        public float_color() {
            r = 0;
            g = 0;
            b = 0;
            a = 0;
        }

        public void print() {
            Debug.Debug.send(r.ToString() + " " + g.ToString() + " " + b.ToString() + " " + a.ToString());
        }
    }
}