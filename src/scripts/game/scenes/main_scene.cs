using Proj.Modules.Input;
using Proj.Modules.Ui;
using Proj.Modules.Debug;
using SDL2;

namespace Proj.Game {
    public class main_scene : scene {
        gui_element bg, sidebar, button, button_2, button_2_;
        bool pressed, press;

        public main_scene() {
            bg = new gui_element(new center_constraint(), new center_constraint(), new percentage_constraint(0.9f), new percentage_constraint(0.9f));
            bg.set_color(230, 221, 198, 255, 1);

            sidebar = new gui_element();
            bg.add_child(ref sidebar);
            sidebar.set_size_constraint(new percentage_constraint(0.3f), new percentage_constraint(0.95f), 6);
            sidebar.set_position_constraint(new percentage_constraint(0.17f),new center_constraint(), 6);
            sidebar.set_color(255, 255, 255, 125, 1);

            button = new gui_element();
            button.set_size_constraint(new aspect_constraint(1), new percentage_constraint(0.1f), 6);
            button.set_position_constraint(new percentage_constraint(0.95f), new percentage_constraint(0.1f), 6);
            button.set_color(100, 100, 100, 125, 1);
            button.set_border_radius(20, 24);
            bg.add_child(ref button);
            
            pressed = false;
            press = false;

            button_2 = new gui_element();
            button_2.set_position_constraint(new percentage_constraint(0.8f), new pixel_constraint(40f), 1);
            button_2.set_size_constraint(new percentage_constraint(0.13f), new percentage_constraint(0.1f), 1);
            button_2.set_color(100, 100, 100, 255, 1);
            button_2.set_border_radius(30, 1);

            bg.add_child(ref button_2);

            button_2_ = new gui_element();
            button_2_.set_position_constraint(new percentage_constraint(0.17f), new center_constraint(), 1);
            button_2_.set_size_constraint(new aspect_constraint(1f), new percentage_constraint(0.85f), 1);
            button_2_.set_color(0, 255, 0, 255, 1);
            button_2_.set_border_radius(25, 1);
            button_2.add_child(ref button_2_);
        }

        public override void update() {
            if(button.mouse_hovered()) {
                button.set_color(200, 200, 200, 125, 3);
                if(mouse.button_just_pressed("lmb")) {
                    pressed = !pressed;
                }
            } else {
                button.set_color(100, 100, 100, 125, 12);
            }

            if(pressed) {
                sidebar.set_color(255, 255, 255, 125, 6);
                sidebar.set_position_constraint(new percentage_constraint(-0.25f), new center_constraint(), 12);
            } else {
                sidebar.set_color(255, 255, 255, 125, 12);
                sidebar.set_position_constraint(new percentage_constraint(0.17f), new center_constraint(), 12);
            }

            bg.update();

            if(button_2.mouse_hovered()) {
                if(mouse.button_just_pressed("lmb")) {
                    press = !press;
                }
            }

            if(press) {
                button_2_.set_position_constraint(new percentage_constraint(0.78f), new center_constraint(), 12);
            } else {
                button_2_.set_position_constraint(new percentage_constraint(0.22f), new center_constraint(), 12);
            }
        }



        public override void render() {
            bg.render();
        }
    }
}