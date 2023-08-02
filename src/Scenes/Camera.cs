using System.Numerics;

namespace Fjord.Scenes;

public class SceneCamera
{
    public Entity? Target;
    private Vector2 CoreOffset = new();
    public Vector2 Offset = new();
    public float Lag = 1;
    public float CameraShakeIntensity = 1f;
    public float CameraShakeLife = 0f;
    public float CameraShakeLifeMax = 0f;
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

    public void SetCameraShake(float life, float intensity)
    {
        this.CameraShakeLife = life;
        this.CameraShakeLifeMax = life;
        this.CameraShakeIntensity = intensity;
    }

    public void Update(Vector2 WindowSize)
    {
        if(Target != null)
        {
            CoreOffset += ((((Target.Get<Transform>().Position - WindowSize / 2) - CoreOffset) / Lag) * 500) * parentScene.DeltaTime;
        }

        if(CameraShakeLife > 0f)
        {
            Random random = new();
            Offset = Vector2.Add(
                CoreOffset, 
                new(
                    ((float)(random.NextDouble() - 0.5) * CameraShakeIntensity) * (CameraShakeLife / CameraShakeLifeMax),
                    ((float)(random.NextDouble() - 0.5) * CameraShakeIntensity) * (CameraShakeLife / CameraShakeLifeMax)
                )
            );
            CameraShakeLife -= 100 * (float)Game.DeltaTime;
        } else
        {
            Offset = CoreOffset;
        }
    }
}