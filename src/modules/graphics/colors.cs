using static SDL2.SDL;
using static SDL2.SDL_image;
using Fjord.Modules.Mathf;
using System;
using System.IO;

namespace Fjord.Modules.Graphics {
    public static class color {
        public static V4 black = new V4(0, 0, 0, 255);
        public static V4 white = new V4(255, 255, 255, 255);
        public static V4 red = new V4(255, 0, 0, 255);
        public static V4 green = new V4(0, 255, 0, 255);
        public static V4 blue = new V4(0, 0, 255, 255);
    }
}