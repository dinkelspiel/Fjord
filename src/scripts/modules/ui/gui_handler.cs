using System.Collections.Generic;
using System;
using System.Numerics;
using Proj.Modules.Debug;
using Proj.Modules;

namespace Proj.Modules.Ui {
    public class gui_constraint {}

    public class percentage_constraint : gui_constraint {
        int value;
        public percentage_constraint(int value_set) {
            value = value_set;
        }
    }

    public class gui_element {
        private List<gui_element> children = new List<gui_element>();
        
        private gui_constraint x_constraint;
        private gui_constraint y_constraint;
        private int position_tween_value;

        private gui_constraint width_constraint;
        private gui_constraint height_constraint;
        private int size_tween_value;

        int x, y, width, height;
        
        public gui_element() {
            x = y = width = height = 0;
        }

        public void add_child(gui_element element) {
            children.Add(element);
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
                Debug.Debug.send(Debug.Debug.get(), "Pog");
                break;
            }

            foreach(gui_element element in children) {
                element.update();
            }
        }

        public void render() {
            foreach(gui_element element in children) {
                element.render();
            }
        }
    }
}