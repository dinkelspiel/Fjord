using System.Numerics;

namespace Fjord.Graphics;

public class CirlceAnimation : ICloneable {
    public Func<float, float>? xDriver = null;
    public Func<float, float>? yDriver = null;
    public Func<float, float>? radiusDriver = null;
    public Func<float, float>? colorDriver = null;
    public Vector4 colorGoal = new();

    public float progress = 0f;
    public float speed = 0f;

    public CirlceAnimation X(Func<float, float> callback)
    {
        this.xDriver = callback;
        return this;
    }

    public CirlceAnimation Y(Func<float, float> callback)
    {
        this.yDriver = callback;
        return this;
    }

    public CirlceAnimation Radius(Func<float,  float> callback)
    {
        this.radiusDriver = callback;
        return this;
    }

    public CirlceAnimation Color(Func<float, float> callback, Vector4 goal)
    {
        this.colorDriver = callback;
        this.colorGoal = goal;
        return this;
    }

    public CirlceAnimation Speed(float speed) 
    {
        this.speed = speed;
        return this;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}