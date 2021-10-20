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

        public Dictionary<Type, component> components = new Dictionary<Type, component>();

        public component get_component(Type Comp) {
            return components[Comp];
        }

        public void add_component(component Comp) {
            components.Add(Comp.GetType(), Comp);
        }

        public void remove_component(component Comp) {
            components.Remove(Comp.GetType());
        }

        public virtual void update() {

        }

        public virtual void render() {

        }
    }
}