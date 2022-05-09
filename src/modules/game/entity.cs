using System.Collections.Generic;
using System;

namespace Fjord.Modules.Game
{
    public class entity 
    {
        public List<dynamic> components = new List<dynamic>();

        public int depth = 0;

        public entity() {
            this.use(new Transform());
            this.use(new Sprite_Renderer());
        }

        public T get<T>() {
            for(var i = 0; i < components.Count; i++) {
                if(components[i].GetType() == typeof(T)) {
                    return components[i];
                }
            }

            Debug.Debug.error("Component type: \"" + typeof(T).ToString() + "\" doesn't exist in components.");
            return components[0];
        }

        public entity use(component Comp, entity parent) {
            try {
                Comp.parent = parent;
                Comp.on_load();
                components.Add(Comp);
            } catch(Exception e) {
                Debug.Debug.send("-- Use Component Error --");
                game.stop(e);
            }
            return this;
        }

        public entity use(component Comp) {
            try {
                Comp.parent = this;
                Comp.on_load();
                components.Add(Comp);
            } catch(Exception e) {
                Debug.Debug.send("-- Use Component Error --");
                game.stop(e);
            }
            return this;
        }

        public entity pop(component Comp) {
            components.Remove(Comp);
            return this;
        }

        public virtual void update() {
            foreach(dynamic i in components) {
                i.update();
            }
        }

        public virtual void render() {
            foreach(dynamic i in components) {
                i.render();
            }
        }
    }
}