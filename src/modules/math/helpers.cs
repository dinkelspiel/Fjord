using static SDL2.SDL;
using Fjord.Modules.Input;
using System;

namespace Fjord.Modules.Mathf {
    public static class helpers {
        public static bool mouse_inside(V4 rect, int margin=0) {
            if ((mouse.position.x > rect.x - margin && mouse.position.x < rect.x + rect.z + margin) && (mouse.position.y > rect.y - margin && mouse.position.y < rect.y + rect.w + margin))
                return true;
            else
                return false;
        }

        public static bool point_inside(V2 pos, V4 rect, int margin=0) {
            if ((pos.x > rect.x - margin && pos.x < rect.x + rect.z + margin) && (pos.y > rect.y - margin && pos.y < rect.y + rect.w + margin))
                return true;
            else
                return false;
        }

        public static bool circle_overlap_rect(V2 circle_pos, int radius, V4 rect) {
        
            // Find the nearest point on the
            // rectangle to the center of
            // the circle
            int Xn = Math.Max(rect.x,
                    Math.Min(circle_pos.x, rect.x + rect.z));
            int Yn = Math.Max(rect.y,
                    Math.Min(circle_pos.y, rect.y + rect.w));
            
            // Find the distance between the
            // nearest point and the center
            // of the circle
            // Distance between 2 points,
            // (x1, y1) & (x2, y2) in
            // 2D Euclidean space is
            // ((x1-x2)**2 + (y1-y2)**2)**0.5
            int Dx = Xn - circle_pos.x;
            int Dy = Yn - circle_pos.y;
            return (Dx * Dx + Dy * Dy) <= radius * radius;
        }

        public static bool rect_overlap_rect(V4 rect1, V4 rect2) {
            // If one rectangle is on left side of other
            if (rect1.x >= rect2.x + rect2.z || rect2.x >= rect1.x + rect1.z)
            {
                return false;
            }
    
            // If one rectangle is above other
            if (rect1.y + rect1.w >= rect2.y || rect2.y + rect2.z >= rect1.y)
            {
                return false;
            }
            return true;
        }

        public static SDL_Rect v4_to_sdl(V4 rect) {
            return new SDL_Rect(rect.x, rect.y, rect.z, rect.w);
        }

        public static V4 sdl_to_v4(SDL_Rect rect) {
            return new V4(rect.x, rect.y, rect.w, rect.h);
        }
    }
}