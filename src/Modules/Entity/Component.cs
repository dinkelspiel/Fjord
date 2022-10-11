using System.Collections.Generic;

namespace Fjord.Modules.Entity;

public abstract class Component
{
    private Entity Parent;

    public Component(Entity parent) {
        this.Parent = parent;
    }

    public virtual void OnAwake() {}
    public virtual void OnSleep() {}
    public virtual void OnUpdate() {}
    public virtual void OnRender() {}

    internal void Awake() {
        OnAwake();
    }

    internal void Sleep() {
        OnSleep();
    }

    internal void Update() {
        OnUpdate();
    }

    internal void Render() {
        OnRender();
    }

    internal void SetParent(Entity entity) {
        this.Parent = entity;
    }

    public T Get<T>() {
        return Parent.Get<T>();
    }

    public Entity GetParent() {
        return Parent;
    }
}
