using System.Numerics;

namespace Fjord.Scenes;

public abstract class Component 
{
    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;

    internal Entity ParentEntity;
    public Scene ParentScene;

    public float DeltaTime
    {
        get
        {
            return (float)Game.GetDeltaTime();
        }
        private set
        {
            this.DeltaTime = value;
        }
    }

    public void Remove<T>()
    {
        ParentEntity.Remove<T>();
    }

    public T Get<T>()
    {
        return ParentEntity.Get<T>();
    }

    public bool TryGet<T>(out T component)
    {
        return ParentEntity.TryGet<T>(out component);
    }

    public virtual void Awake() {}
    public virtual void Sleep() {}
    public virtual void Update() {}

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
        Update();
    }
}

public class Transform : Component
{
    public Vector2 Position;
    public float Angle;
}