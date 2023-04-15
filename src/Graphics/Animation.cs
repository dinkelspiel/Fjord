using System.Numerics;

namespace Fjord.Graphics;

public static class SampleAnimations {
    public static Dictionary<string, CirlceAnimation> anims = new();

    public static CirlceAnimation circlePulseAnimation = new CirlceAnimation()
        .Radius((x) => {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return (1 + c3 * (float)Math.Pow(x - 1, 3) + c1 * (float)Math.Pow(x - 1, 2));
        })
        .Color((x) => {
            float c1 = 1.70158f;
            float c3 = c1 + 1;

            return (1 + c3 * (float)Math.Pow(x - 1, 3) + c1 * (float)Math.Pow(x - 1, 2));
        }, new(239, 17, 33, 255))
        .Speed(0.002f);

    public static CirlceAnimation CirclePulseAnimation(string id)
    {
        if(!anims.ContainsKey(id))
            anims.Add(id, (CirlceAnimation)circlePulseAnimation.Clone());

        return anims[id];
    }
}

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