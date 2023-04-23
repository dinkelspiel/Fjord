using System.Numerics;

namespace Fjord.Scenes;

public abstract class Component 
{
    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;

    internal Entity ParentEntity;
    public Scene ParentScene;

    public Component()
    {
        //this.Keyboard = new("");
        //this.Mouse = new("");
        //this.ParentScene = default(Scene);
        //this.ParentEntity = new(ParentScene, Keyboard, Mouse);
    }

    public void Remove<T>()
    {
        ParentEntity.Remove<T>();
    }

    public T? Get<T>()
    {
        return ParentEntity.Get<T>();
    }

    public virtual void Awake() {}
    public virtual void Sleep() {}
    public virtual void Update(double dt) {}
    public virtual void Render() {}

    internal void AwakeCall()
    {
        Awake();
    }

    internal void SleepCall()
    {
        Sleep();
    }

    internal void UpdateCall()
    {
        Update(Game.GetDeltaTime());
    }

    internal void RenderCall()
    {
        Render();
    }
}

public class Transform : Component
{
    public Vector2 Position;
}