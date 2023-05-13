using System.Numerics;

namespace Fjord.Scenes;

public abstract class Component 
{
    public SceneKeyboard Keyboard = new("");
    public SceneMouse Mouse = new("");

    internal Entity ParentEntity = new(default(Scene)!);
    public Scene ParentScene = default(Scene)!;

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
    [Export]
    public Vector2 Position;

    [Export(0, 360)]
    public float Angle;

    public override void Update()
    {
        if(Angle > 360)
        {
            Angle = 0;
        }

        if(Angle < 0)
        {
            Angle = 360;
        }
    }
}