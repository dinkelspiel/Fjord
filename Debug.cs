using ShooterThingy;
using System.Numerics;
using Fjord.Graphics;
using Fjord.Scenes;
using static SDL2.SDL;

namespace Fjord.Scenes;

public static class Debug {

    public static void Initialize()
    {
        SceneHandler.Register(new DebugScene((int)(Game.Window.Width * 0.2), 1080, "debug")
            .SetAllowWindowResize(false)
            .SetRelativeWindowSize(0.8f, 0f, 1f, 1f));
    }

    public static SDL_FRect DebugWindowOffset = new()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };
}

public class DebugScene : Scene
{
    public DebugScene(int width, int height, string id) : base(width, height, id)
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
        new UiBuilder(new Vector4(0, 0, (int)(Game.Window.Width * 0.2), (int)Game.Window.Height), LocalMousePosition)
            .Title("Debug")
            .Container(
                new UiBuilder()
                    .Title("Scenes")
                    .ForEach(SceneHandler.Scenes.ToList(), (val, idx) =>
                    {
                        var list = new List<object>() {
                                new UiTitle(val.Key),
                                new UiButton("Load", () => SceneHandler.Load(val.Key)),
                                new UiButton("Unload", () => SceneHandler.Unload(val.Key)),
                                new UiButton("Remake", () => SceneHandler.Remake(val.Key)),
                        };

                        if (idx != SceneHandler.Scenes.ToList().Count - 1)
                        {
                            list.Add(new UiSpacer());
                        }
                        
                        return list;
                    })
                    .Title("Loaded Scenes")
                    .ForEach(SceneHandler.LoadedScenes, (scene, idx) =>
                    {
                        var list = new List<object>()
                        {
                            new UiTitle(scene),
                            new UiButton("Unload", () => SceneHandler.Unload(scene))
                        };

                        if (idx != SceneHandler.LoadedScenes.Count - 1)
                        {
                            list.Add(new UiSpacer());
                        }

                        return list;
                    })
                    .Build()
            )
            .Render();
    }
}
