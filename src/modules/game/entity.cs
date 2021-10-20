using Fjord.Modules.Mathf;
using Fjord.Modules.Graphics;
using Fjord.Modules.Debug;
using static SDL2.SDL;
using System;

namespace Fjord.Modules.Game
{
    public class entity
    {
        public Transform transform = new Transform();

        public List<component> components = new List<component>();

        public dynamic get_component<T>() {
            for(var i = 0; i < components.Count; i++) {
                if(components[i].GetType() == typeof(T)) {
                    return components[i];
                }
            }

            Debug.Debug.error("Component type: \"" + typeof(T).ToString() + "\" doesn't exist in components.");
            return components[0];
        }

        public void add_component(component Comp, dynamic parent) {
            Comp.parent = parent;
            Comp.on_load();
            components.Add(Comp);
        }

        public void remove_component(component Comp) {
            components.Remove(Comp);
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