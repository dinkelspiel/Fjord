namespace Fjord.Scenes;

public abstract class Component 
{
    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;

    public virtual void Awake() {}
    public virtual void Sleep() {}
    public virtual void Update() {}
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
        Update();
    }

    internal void RenderCall()
    {
        Render();
    }
}