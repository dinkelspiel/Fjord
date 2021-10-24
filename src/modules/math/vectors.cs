namespace Fjord.Modules.Mathf {
    public class V2 : IFormattable {
        public int x;
        public int y;

        public V2() {
            x = 0;
            y = 0;
        }

        public V2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V2 p = (V2) obj;
                return (x == p.x) && (y == p.y);
            }
        }

        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString(); 
        }
    
        public static bool operator ==(V2 c1, V2 c2) {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(V2 c1, V2 c2) {
            return !(c1.x == c2.x && c1.y == c2.y);
        }
    }
    
    public class V2f : IFormattable {
        public float x;
        public float y;

        public V2f() {
            x = 0;
            y = 0;
        }

        public V2f(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V2 p = (V2) obj;
                return (x == p.x) && (y == p.y);
            }
        }

        public override int GetHashCode()
        {
            return ((int)x << 2) ^ (int)y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString(); 
        }
    
        public static bool operator ==(V2f c1, V2f c2) {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(V2f c1, V2f c2) {
            return !(c1.x == c2.x && c1.y == c2.y);
        }
    } 

    public class V3 : IFormattable {
        public int x;
        public int y;
        public int z;

        public V3() {
            x = 0;
            y = 0;
            z = 0;
        }

        public V3(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V3 p = (V3) obj;
                return (x == p.x) && (y == p.y) && (z == p.z);
            }
        }

        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString() + ", " + z.ToString(); 
        }
    
        public static bool operator ==(V3 c1, V3 c2) {
            return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z;
        }

        public static bool operator !=(V3 c1, V3 c2) {
            return !(c1.x == c2.x && c1.y == c2.y && c1.z == c2.z);
        }
    }
    
    public class V3f : IFormattable {
        public float x;
        public float y;
        public float z;

        public V3f() {
            x = 0;
            y = 0;
            z = 0;
        }

        public V3f(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V3f p = (V3f) obj;
                return (x == p.x) && (y == p.y) && (z == p.z);
            }
        }

        public override int GetHashCode()
        {
            return ((int)x << 2) ^ (int)y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString() + ", " + z.ToString(); 
        }
    
        public static bool operator ==(V3f c1, V3f c2) {
            return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z;
        }

        public static bool operator !=(V3f c1, V3f c2) {
            return !(c1.x == c2.x && c1.y == c2.y && c1.z == c2.z);
        }
    }
    
    public class V4 : IFormattable {
        public int x;
        public int y;
        public int z;
        public int w;

        public V4() {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public V4(int x, int y, int z, int w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V4 p = (V4) obj;
                return (x == p.x) && (y == p.y) && (z == p.z) && (w == p.w);
            }
        }

        public override int GetHashCode()
        {
            return (x << 2) ^ y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString() + ", " + z.ToString(); 
        }
    
        public static bool operator ==(V4 c1, V4 c2) {
            return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z && c1.w == c2.w;
        }

        public static bool operator !=(V4 c1, V4 c2) {
            return !(c1.x == c2.x && c1.y == c2.y && c1.z == c2.z && c1.w == c2.w);
        }
    }
    
    public class V4f : IFormattable {
        public float x;
        public float y;
        public float z;
        public float w;

        public V4f() {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public V4f(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else {
                V4f p = (V4f) obj;
                return (x == p.x) && (y == p.y) && (z == p.z) && (w == p.w);
            }
        }

        public override int GetHashCode()
        {
            return ((int)x << 2) ^ (int)y;
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider) {
            return x.ToString() + ", " + y.ToString() + ", " + z.ToString(); 
        }
    
        public static bool operator ==(V4f c1, V4f c2) {
            return c1.x == c2.x && c1.y == c2.y && c1.z == c2.z && c1.w == c2.w;
        }

        public static bool operator !=(V4f c1, V4f c2) {
            return !(c1.x == c2.x && c1.y == c2.y && c1.z == c2.z && c1.w == c2.w);
        }
    }
    
}