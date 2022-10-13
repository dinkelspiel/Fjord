using System.Collections.Generic;
using System.Linq;
using Fjord.Modules.Components;
using Fjord.Modules.Graphics;
using Fjord.Modules.Window;

namespace Fjord.Modules.Entity;

class NoComponentException : Exception { }

public sealed class Entity
{
    internal List<dynamic> Components = new List<dynamic>();
    public dynamic Parent;
    public int Depth = 0;

    public Entity(Scene parent) {
        this.Parent = parent;
    }

    public static Entity New(Scene parent, Texture texture) {
        Entity entity = new Entity(parent);
        entity.Use(new Transform(entity))
              .Use(new SpriteRenderer(entity, texture)); 
        return entity;
    }

    public void Destroy()
    {
        this.Parent.EntityList.Remove(this);
    }

    public void SetParent(Scene parent) {
        this.Parent = parent;
    }

    public T Get<T>() {
        if (this.Components.OfType<T>().Count<T>() == 0)
            throw new NoComponentException();

        return this.Components.OfType<T>().First();
    }

    internal Entity Use(dynamic component, Entity parent) {
        if(component.GetType().BaseType != typeof(Component))
            Debug.Debug.Error("Class provided was not a Component!");

        if(Components.Contains(component.GetType()))
            Debug.Debug.Error("Component already exists in entity!");

        try {
            component.SetParent(parent);
            component.Awake();
            Components.Add(component);
        } catch(System.Exception e) {
            Debug.Debug.Error("-- Use Component Error --");
            Game.Stop(e);
        }
        return this;
    }

    public Entity Use(dynamic component) {
        if(component.GetType().BaseType != typeof(Component))
            Debug.Debug.Error("Class provided was not a Component!");
    
        if(Components.Contains(component.GetType()))
            Debug.Debug.Error("Component already exists in entity!");

        try {
            component.SetParent(this);
            component.Awake();
            Components.Add(component);
        } catch(System.Exception e) {
            Debug.Debug.Error(e.Message);
            Debug.Debug.Error("-- Use Component Error --");
            Game.Stop(e);
        }
        return this;
    }

    internal void Update() {
        foreach(dynamic i in Components) {
            i.Update();
        }
    }

    internal void Render() {
        foreach(dynamic i in Components) {
            i.Render();
        }
    }
}
