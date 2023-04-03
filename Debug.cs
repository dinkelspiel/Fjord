using ShooterThingy;
using System.Numerics;
using static SDL2.SDL;

namespace Fjord.Debug;

public static class Debug {
    private static bool DebugMode = false;

    public static void Initialize()
    {
        SceneHandler.Register("debug", new DebugScene((int)(Game.Window.Width * 0.8), 0));
    }

    public static SDL_FRect DebugWindowOffset = new ()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };
    public static void SetDebugMode(bool debugMode) {
        DebugMode = debugMode;
    }

    public static bool GetDebugMode()
    {
        return DebugMode;
    }
}

public class DebugScene : Scene
{
    public DebugScene(int width, int height) : base(width, height)
    {

    }

    public override void Awake()
    {
        
    }

    public override void Sleep()
    {

    }
    public override void Update()
    {

    }

    public override void Render()
    {
        new UiBuilder(new Vector4((int)(Game.Window.Width * 0.8), 0, (int)(Game.Window.Width * 0.2), (int)Game.Window.Height))
            .Title("Debug")
            .Container(
                new UiBuilder()
                    .Title("Scenes")
                    .ForEach(SceneHandler.Scenes.ToList(), (val) =>
                    {
                        return new List<object>() {
                                new UiTitle(val.Key),
                                new UiButton("Load", () => SceneHandler.Load(val.Key)),
                                new UiButton("Unload", () => SceneHandler.Unload(val.Key)),
                                new UiButton("Remake", () => SceneHandler.Remake(val.Key))
                        };
                    })
                    .Title("Loaded Scenes")
                    .ForEach(SceneHandler.LoadedScenes, (scene) =>
                    {
                        return new List<object>()
                        {
                                new UiTitle(scene),
                                new UiSpacer()
                        };
                    })
                    .Build()
            )
            .Render();
    }
}
