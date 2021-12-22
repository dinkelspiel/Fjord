using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using Fjord.Modules.Debug;
using static SDL2.SDL;
using System;
using System.Collections.Generic;

namespace Fjord.Modules.Game
{
    public class entity
    {
        public List<component> components = new List<component>();

        public int depth = 0;

        public entity() {
            this.use(new Transform());
            this.use(new Sprite_Renderer());
        }

        public dynamic get<T>() {
            for(var i = 0; i < components.Count; i++) {
                if(components[i].GetType() == typeof(T)) {
                    return components[i];
                }
            }

            Debug.Debug.error("Component type: \"" + typeof(T).ToString() + "\" doesn't exist in components.");
            return components[0];
        }

        public entity use(component Comp, dynamic parent) {
            Comp.parent = parent;
            Comp.on_load();
            components.Add(Comp);
            return this;
        }

        public entity use(component Comp) {
            Comp.parent = this;
            Comp.on_load();
            components.Add(Comp);
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