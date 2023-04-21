namespace Fjord.Scenes;

public abstract class Component 
{
    public SceneKeyboard Keyboard;
    public SceneMouse Mouse;

    public Component()
    {
        this.Keyboard = new("");
        this.Mouse = new("");
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