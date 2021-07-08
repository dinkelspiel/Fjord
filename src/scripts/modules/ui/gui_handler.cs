using System.Collections.Generic;
using System;
using System.Numerics;
using Proj.Modules.Debug;
using Proj.Modules;
using Proj.Modules.Input;
using SDL2;

namespace Proj.Modules.Ui {

    public class screen_rect : gui_element {
        public screen_rect() {
            x = y = 0;
            SDL.SDL_GetWindowSize(game_manager.window, out width, out height); 
        }

        public void screen_update() {
            SDL.SDL_GetWindowSize(game_manager.window, out width, out height);
        }
    }

    public class gui_constraint {
        public float value;
    }

    public class percentage_constraint : gui_constraint {
        public percentage_constraint(float value_set) {
            value = value_set;
        }
    }

    public class pixel_constraint : gui_constraint {
        public pixel_constraint(float value_set) {
            value = value_set;
        }
    }

    public class aspect_constraint : gui_constraint {
        public aspect_constraint(float value_set) {
            value = value_set;
        }
    }

    public class center_constraint : gui_constraint {}

    public class gui_element {
        #region Variables
        private List<gui_element> children = new List<gui_element>();
        
        public gui_constraint x_constraint;
        public gui_constraint y_constraint;
        public int position_tween_value = 1;

        public gui_constraint width_constraint;
        public gui_constraint height_constraint;
        public int size_tween_value = 1;

        public gui_element parent = game_manager.screen;

        public SDL.SDL_Color color;

        public int x, y, width, height;
        #endregion
        
        #region Constructrs
        public gui_element() {
            x = y = width = height = 0;

            color.r = color.b = color.a = 255;
            color.g = 0;
        }

        public gui_element(gui_constraint x_constraint_set, gui_constraint y_constraint_set, gui_constraint width_constraint_set, gui_constraint height_constraint_set) {
            color.r = color.b = color.a = 255;
            color.g = 0;

            width_constraint = width_constraint_set;
            switch(width_constraint_set) {
                case percentage_constraint:
                    width = (int)(parent.width * width_constraint_set.value);
                    break;
                case pixel_constraint:
                    width = (int)width_constraint_set.value;
                    break;
                case aspect_constraint:
                    height = 0;
                    break;
            }

            height_constraint = height_constraint_set;
            switch(height_constraint_set) {
                case percentage_constraint:
                    height = (int)(parent.height * height_constraint_set.value);
                    break;
                case pixel_constraint:
                    height = (int)height_constraint_set.value;
                    break;
                case aspect_constraint:
                    height = 0;
                    break;
            }

            x_constraint = x_constraint_set;
            switch(x_constraint_set) {
                case percentage_constraint: 
                    x = (int)(parent.width * x_constraint_set.value) - width / 2;
                    break;
                case pixel_constraint:
                    x = (int)x_constraint_set.value - width / 2;
                    break;
                case center_constraint:
                    x = (parent.width / 2) - (width / 2);
                    break;
            }

            y_constraint = y_constraint_set;
            switch(y_constraint_set) {
                case percentage_constraint: 
                    y = (int)(parent.height * y_constraint_set.value) - height / 2;
                    break;
                case pixel_constraint:
                    y = (int)y_constraint_set.value - height / 2;
                    break;
                case center_constraint:
                    y = (parent.height / 2) - (height / 2);
                    break;
                
            }
        }
        #endregion

        public void add_child(ref gui_element element) {
            element.parent = this;
            children.Add(element);
        }

        public bool mouse_hovered() {
            if(mouse.x > x && mouse.x < x + width) {
                if(mouse.y > y && mouse.y < y + height) {
                    return true;
                }
            }
            return false;
        }

        public void set_color(byte r, byte g, byte b, byte a=255) {
            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
        }

        public void set_position_constraint(gui_constraint x_constraint_set, gui_constraint y_constraint_set, int tween_value_set) {
            x_constraint = x_constraint_set;
            y_constraint = y_constraint_set;
            position_tween_value = tween_value_set;
        }

        public void set_size_constraint(gui_constraint width_constraint_set, gui_constraint height_constraint_set, int tween_value_set) {
            width_constraint = width_constraint_set;
            height_constraint = height_constraint_set;
            size_tween_value = tween_value_set;
        }

        public void update() {
            switch(x_constraint) {
                case percentage_constraint:
                    x -= (x - (parent.width * (int)x_constraint.value - width / 2)) / position_tween_value;
                    break;
                case pixel_constraint:
                    x -= (int)(x + parent.x - x_constraint.value) / position_tween_value;
                    break;
                case center_constraint:
                    x -= (int)(x - game_manager.screen.width / 2 + width / 2) / position_tween_value;
                    break;
            }

            switch(y_constraint) {
                case percentage_constraint:
                    y -= (y - (parent.height * (int)y_constraint.value - height / 2)) / position_tween_value;
                    break;
                case pixel_constraint:
                    y -= (int)(y + parent.y - y_constraint.value) / position_tween_value;
                    break;
                case center_constraint:
                    y -= (int)(y - game_manager.screen.height / 2 + height / 2) / position_tween_value;
                    break;
            }

            switch(width_constraint) {
                case percentage_constraint:
                    width -= (int)(width - parent.width * width_constraint.value) / size_tween_value;
                    break;
                case pixel_constraint:
                    width -= (int)(width - x_constraint.value) / size_tween_value;
                    break;
                case aspect_constraint:
                    width -= (int)(width - height * width_constraint.value) / size_tween_value;
                    break;
            }

            switch(height_constraint) {
                case percentage_constraint:
                    height -= (int)(height - parent.height * height_constraint.value) / size_tween_value;
                    break;
                case pixel_constraint:
                    height -= (int)(height - y_constraint.value) / size_tween_value;
                    break;
                case aspect_constraint:
                    height -= (int)(height - width * height_constraint.value) / size_tween_value;
                    break;
            }

            foreach(gui_element element in children) {
                element.update();
            }
        }

        public void render() {
            SDL.SDL_Rect rect;
            rect.x = x;
            rect.y = y;
            rect.w = width;
            rect.h = height;
            draw.rect(game_manager.renderer, rect, color.r, color.g, color.b, color.a, true);

            foreach(gui_element element in children) {
                element.render();
            }
        }
    }
}