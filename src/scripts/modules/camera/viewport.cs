using System.Numerics;

namespace Proj.Modules.Camera {
    public static class camera {
        public static Vector2 camera_position = new Vector2(0, 0);

        public static void set_viewport(float x, float y) {
            camera_position.X = x;
            camera_position.Y = y;
        }
    }
}