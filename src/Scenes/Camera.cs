using System.Numerics;

namespace Fjord.Scenes;

public class SceneCamera
{
    public Entity? Target;
    public Vector2 Offset = new();
    public float Lag = 1;
    private Scene parentScene;

    public SceneCamera(Scene parent)
    {
        this.parentScene = parent;
    }

    public void SetTarget(Entity target)
    {
        this.Target = target;
    }

    public void SetLag(float lag)
    {
        this.Lag = lag;
    } 

    public void Update(Vector2 WindowSize)
    {
        if(Target != null)
        {
            Offset += ((((Target.Get<Transform>().Position - WindowSize / 2) - Offset) / Lag) * 500) * parentScene.DeltaTime;
        }
    }
}