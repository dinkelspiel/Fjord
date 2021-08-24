using System.Collections.Generic;
using Proj.Modules.Debug;
using System.Numerics;
using System;
using Proj.Modules.Misc;
using Proj.Modules.Input;
using Proj.Modules.Graphics;
using SDL2;

namespace Proj.Modules.Ui {
     
    public class screen_rect : gui_element {
        int w, h;
        public screen_rect() {
            x = y = 0;
            SDL.SDL_GetWindowSize(game_manager.window, out w, out h); 
        }

        public void screen_update() {
            SDL.SDL_GetWindowSize(game_manager.window, out w, out h);
            width = (float)w;
            height = (float)h;
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

    public class center_constraint : gui_constraint {

    }

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

        public float_color color = new float_color();
        public float_color color_to = new float_color();
        public float color_tween_value = 1;

        public int border_radius = 0, border_radius_to = 0, border_radius_tween = 1;

        public float x, y, width, height;
        #endregion
        
        #region Constructrs
        public gui_element() {
            x = y = width = height = 0;

            color.r = 255;
            color.b = 255;
            color.a = 255;
            color.g = 0;
        }

        public gui_element(gui_constraint x_constraint_set, gui_constraint y_constraint_set, gui_constraint width_constraint_set, gui_constraint height_constraint_set) {
            color.r = 255;
            color.b = 255;
            color.a = 255;
            color.g = 0;

            width_constraint = width_constraint_set;
            switch(width_constraint_set) {
                case percentage_constraint:
                    width = (parent.width * width_constraint_set.value);
                    break;
                case pixel_constraint:
                    width = width_constraint_set.value;
                    break;
                case aspect_constraint:
                    height = 0;
                    break;
            }

            height_constraint = height_constraint_set;
            switch(height_constraint_set) {
                case percentage_constraint:
                    height = (parent.height * height_constraint_set.value);
                    break;
                case pixel_constraint:
                    height = height_constraint_set.value;
                    break;
                case aspect_constraint:
                    height = 0;
                    break;
            }

            x_constraint = x_constraint_set;
            switch(x_constraint_set) {
                case percentage_constraint: 
                    x = (parent.width * x_constraint_set.value) - width / 2;
                    break;
                case pixel_constraint:
                    x = x_constraint_set.value - width / 2;
                    break;
                case center_constraint:
                    x = (parent.width / 2) - (width / 2);
                    break;
            }

            y_constraint = y_constraint_set;
            switch(y_constraint_set) {
                case percentage_constraint: 
                    y = (parent.height * y_constraint_set.value) - height / 2;
                    break;
                case pixel_constraint:
                    y = y_constraint_set.value - height / 2;
                    break;
                case center_constraint:
                    y = (parent.height / 2) - (height / 2);
                    break;
                
            }
        }
        #endregion

        public void add_child(ref gui_element element) {
            element.set_parent(this);
            children.Add(element);
        }

        public void set_parent(gui_element element) {
            parent = element;
        }

        public bool mouse_hovered() {
            if(mouse.x > x - width / 2 && mouse.x < x + width / 2) { 
                if(mouse.y > y - width / 2 && mouse.y < y + height / 2) {
                    return true;
                }
            }
            return false;
        }

        public void set_color(float r, float g, float b, float a=255, float tween_value_set=1) {
            color_to.r = r;
            color_to.g = g;
            color_to.b = b;
            color_to.a = a;
            color_tween_value = tween_value_set;
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

        public void set_border_radius(int border_radius_set, int tween_value_set) {
            border_radius_to = border_radius_set;
            border_radius_tween = tween_value_set;
        }

        public void update() {
            #region Constraints
            if(parent.x == 0 && parent.y == 0) {
                switch(x_constraint) {
                    case percentage_constraint:
                        x -= (x - (parent.x + parent.width * x_constraint.value)) / position_tween_value;
                        break;
                    case pixel_constraint:
                        x -= (x - (parent.x + x_constraint.value)) / position_tween_value;
                        break;
                    case center_constraint:
                        x -= (x - (parent.x + parent.width / 2)) / position_tween_value;
                        break;  
                    case aspect_constraint:
                        x -= (x - y) / position_tween_value;
                        break;                  
                }

                switch(y_constraint) {
                    case percentage_constraint:
                        y -= (y - (parent.y + parent.height * y_constraint.value)) / position_tween_value;
                        break;
                    case pixel_constraint:
                        y -= (y - (parent.y + y_constraint.value)) / position_tween_value;
                        break;
                    case center_constraint:
                        y -= (y - (parent.y + parent.height / 2)) / position_tween_value;
                        break;    
                    case aspect_constraint:
                        y -= (y - x) / position_tween_value;
                        break;                
                }
            } else {
                switch(x_constraint) {
                    case percentage_constraint:
                        x -= (x - (parent.x + parent.width * x_constraint.value) + parent.width / 2) / position_tween_value;
                        break;
                    case pixel_constraint:
                        x -= (x - (parent.x + x_constraint.value) + parent.width / 2) / position_tween_value;
                        break;
                    case center_constraint:
                        x -= (x - (parent.x + parent.width / 2) + parent.width / 2) / position_tween_value;
                        break;                    
                }

                switch(y_constraint) {
                    case percentage_constraint:
                        y -= (y - (parent.y + parent.height * y_constraint.value) + parent.height / 2) / position_tween_value;
                        break;
                    case pixel_constraint:
                        y -= (y - (parent.y + y_constraint.value) + parent.height / 2) / position_tween_value;
                        break;
                    case center_constraint:
                        y -= (y - (parent.y + parent.height / 2) + parent.height / 2) / position_tween_value;
                        break;                    
                }
            }

            switch(width_constraint) {
                case percentage_constraint:
                    width -= (width - (parent.width * width_constraint.value)) / size_tween_value;
                    break;
                case pixel_constraint:
                    width -= (width - (width_constraint.value)) / size_tween_value;
                    break;
                case aspect_constraint:
                    width -= (width - height * width_constraint.value) / size_tween_value;
                    break;
            }

            switch(height_constraint) {
                case percentage_constraint:
                    height -= (height - (parent.height * height_constraint.value)) / size_tween_value;
                    break;
                case pixel_constraint:
                    height -= (height - (height_constraint.value)) / size_tween_value;
                    break;
                case aspect_constraint:
                    height -= (height - width * height_constraint.value) / size_tween_value;
                    break;
            }
            #endregion
            color.r -= ((color.r - color_to.r) / color_tween_value);
            color.g -= ((color.g - color_to.g) / color_tween_value);
            color.b -= ((color.b - color_to.b) / color_tween_value);
            color.a -= ((color.a - color_to.a) / color_tween_value);

            border_radius = border_radius_to;

            foreach(gui_element element in children) {
                element.update();
            }
        }

        public void render() {
            SDL.SDL_Rect rect;
            rect.x = (int)x - (int)width / 2;
            rect.y = (int)y - (int)height / 2;
            rect.w = (int)width;
            rect.h = (int)height;
            draw.round_rect(game_manager.renderer, rect, (byte)color.r, (byte)color.g, (byte)color.b, (byte)color.a, border_radius, true);

            foreach(gui_element element in children) {
                element.render();
            }
        }
    }
}