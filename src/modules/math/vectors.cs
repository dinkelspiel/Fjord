using System;

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

        public static implicit operator V2(V2f input) {
            return new V2((int)input.x, (int)input.y);
        }


        public static V2 operator +(V2 v1, V2 v2) {
            return new V2(v1.x + v2.x, v1.y + v2.y); 
        }

        public static V2 operator -(V2 v1, V2 v2) {
            return new V2(v1.x - v2.x, v1.y - v2.y); 
        }

        public static V2 operator *(V2 v1, V2 v2) {
            return new V2(v1.x * v2.x, v1.y * v2.y); 
        }

        public static V2 operator /(V2 v1, V2 v2) {
            return new V2(v1.x / v2.x, v1.y / v2.y); 
        }


        public static V2 operator +(V2 v1, int v2) {
            return new V2(v1.x + v2, v1.y + v2); 
        }

        public static V2 operator -(V2 v1, int v2) {
            return new V2(v1.x - v2, v1.y - v2); 
        }

        public static V2 operator *(V2 v1, int v2) {
            return new V2(v1.x * v2, v1.y * v2); 
        }

        public static V2 operator /(V2 v1, int v2) {
            return new V2(v1.x / v2, v1.y / v2); 
        }

        public V2f normalized() {
            float distance = (float)Math.Sqrt(this.x * this.x + this.y * this.y);
            return new V2f(this.x / distance, this.y / distance);
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

        public static implicit operator V2f(V2 input) {
            return new V2f(input.x, input.y);
        }

        public static V2f operator +(V2f v1, V2f v2) {
            return new V2f(v1.x + v2.x, v1.y + v2.y); 
        }

        public static V2f operator -(V2f v1, V2f v2) {
            return new V2f(v1.x - v2.x, v1.y - v2.y); 
        }

        public static V2f operator *(V2f v1, V2f v2) {
            return new V2f(v1.x * v2.x, v1.y * v2.y); 
        }

        public static V2f operator /(V2f v1, V2f v2) {
            return new V2f(v1.x / v2.x, v1.y / v2.y); 
        }

    
        public static V2f operator +(V2f v1, float v2) {
            return new V2f(v1.x + v2, v1.y + v2); 
        }

        public static V2f operator -(V2f v1, float v2) {
            return new V2f(v1.x - v2, v1.y - v2); 
        }

        public static V2f operator *(V2f v1, float v2) {
            return new V2f(v1.x * v2, v1.y * v2); 
        }

        public static V2f operator /(V2f v1, float v2) {
            return new V2f(v1.x / v2, v1.y / v2); 
        }

        public V2f normalized() {
            float distance = (float)Math.Sqrt(this.x * this.x + this.y * this.y);
            return new V2f(this.x / distance, this.y / distance);
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

        public static implicit operator V3(V3f input) {
            return new V3((int)input.x, (int)input.y, (int)input.z);
        }

        public static V3 operator +(V3 v1, V3 v2) {
            return new V3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z); 
        }

        public static V3 operator -(V3 v1, V3 v2) {
            return new V3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z); 
        }

        public static V3 operator *(V3 v1, V3 v2) {
            return new V3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z); 
        }

        public static V3 operator /(V3 v1, V3 v2) {
            return new V3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z); 
        }


        public static V3 operator +(V3 v1, int v2) {
            return new V3(v1.x + v2, v1.y + v2, v1.z + v2); 
        }

        public static V3 operator -(V3 v1, int v2) {
            return new V3(v1.x - v2, v1.y - v2, v1.z - v2); 
        }

        public static V3 operator *(V3 v1, int v2) {
            return new V3(v1.x * v2, v1.y * v2, v1.z * v2); 
        }

        public static V3 operator /(V3 v1, int v2) {
            return new V3(v1.x / v2, v1.y / v2, v1.z / v2); 
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

        public static implicit operator V3f(V3 input) {
            return new V3f((int)input.x, (int)input.y, (int)input.z);
        }

        public static V3f operator +(V3f v1, V3f v2) {
            return new V3f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z); 
        }

        public static V3f operator -(V3f v1, V3f v2) {
            return new V3f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z); 
        }

        public static V3f operator *(V3f v1, V3f v2) {
            return new V3f(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z); 
        }

        public static V3f operator /(V3f v1, V3f v2) {
            return new V3f(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z); 
        }


        public static V3f operator +(V3f v1, float v2) {
            return new V3f(v1.x + v2, v1.y + v2, v1.z + v2); 
        }

        public static V3f operator -(V3f v1, float v2) {
            return new V3f(v1.x - v2, v1.y - v2, v1.z - v2); 
        }

        public static V3f operator *(V3f v1, float v2) {
            return new V3f(v1.x * v2, v1.y * v2, v1.z * v2); 
        }

        public static V3f operator /(V3f v1, float v2) {
            return new V3f(v1.x / v2, v1.y / v2, v1.z / v2); 
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

        public static implicit operator V4(V4f input) {
            return new V4((int)input.x, (int)input.y, (int)input.z, (int)input.w);
        }

        public static V4 operator +(V4 v1, V4 v2) {
            return new V4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w); 
        }

        public static V4 operator -(V4 v1, V4 v2) {
            return new V4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w); 
        }

        public static V4 operator *(V4 v1, V4 v2) {
            return new V4(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w); 
        }

        public static V4 operator /(V4 v1, V4 v2) {
            return new V4(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w); 
        }


        public static V4 operator +(V4 v1, int v2) {
            return new V4(v1.x + v2, v1.y + v2, v1.z + v2, v1.w + v2); 
        }

        public static V4 operator -(V4 v1, int v2) {
            return new V4(v1.x - v2, v1.y - v2, v1.z - v2, v1.w - v2); 
        }

        public static V4 operator *(V4 v1, int v2) {
            return new V4(v1.x * v2, v1.y * v2, v1.z * v2, v1.w * v2); 
        }

        public static V4 operator /(V4 v1, int v2) {
            return new V4(v1.x / v2, v1.y / v2, v1.z / v2, v1.w / v2); 
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

        public static implicit operator V4f(V4 input) {
            return new V4f((int)input.x, (int)input.y, (int)input.z, (int)input.w);
        }

        public static V4f operator +(V4f v1, V4f v2) {
            return new V4f(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w); 
        }

        public static V4f operator -(V4f v1, V4f v2) {
            return new V4f(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w); 
        }

        public static V4f operator *(V4f v1, V4f v2) {
            return new V4f(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w); 
        }

        public static V4f operator /(V4f v1, V4f v2) {
            return new V4f(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w); 
        }


        public static V4f operator +(V4f v1, float v2) {
            return new V4f(v1.x + v2, v1.y + v2, v1.z + v2, v1.w + v2); 
        }

        public static V4f operator -(V4f v1, float v2) {
            return new V4f(v1.x - v2, v1.y - v2, v1.z - v2, v1.w - v2); 
        }

        public static V4f operator *(V4f v1, float v2) {
            return new V4f(v1.x * v2, v1.y * v2, v1.z * v2, v1.w * v2); 
        }

        public static V4f operator /(V4f v1, float v2) {
            return new V4f(v1.x / v2, v1.y / v2, v1.z / v2, v1.w / v2); 
        }
    }
    
}