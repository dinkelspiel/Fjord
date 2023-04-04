using System.Numerics;
using Fjord.Graphics;
using Fjord.Scenes;
using Fjord.Ui;
using static SDL2.SDL;

namespace Fjord.Scenes;

public struct DebugLog
{
    public string time;
    public string sender;
    public string message;
    public LogLevel level;
}

public enum LogLevel
{
    Message,
    Warning,
    Error,
}

public static class Debug {

    public static List<DebugLog> Logs = new List<DebugLog>();

    public static void Initialize()
    {
        SceneHandler.Register(new InspectorScene((int)(Game.Window.Width * 0.2), 1080, "inspector")
            .SetAllowWindowResize(false)
            .SetRelativeWindowSize(0.8f, 0f, 1f, 1f));

        SceneHandler.Register(new ConsoleScene((int)(Game.Window.Width * 0.2), (int)(Game.Window.Height * 0.4), "console")
            .SetAllowWindowResize(true)
            .SetRelativeWindowSize(0.1f, 0.1f, 0.3f, 0.5f)
            .SetAlwaysRebuildTexture(true));

        Log(LogLevel.Warning, "08:09:39", "Fjord.Ui", "Test");
        Log(LogLevel.Warning, "08:09:39", "Fjord.Ui", "Test2");
        Log(LogLevel.Warning, "08:09:39", "Fjord.Ui", "Test3");
    }

    public static SDL_FRect DebugWindowOffset = new()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };

    public static void Log(LogLevel level, string time, string sender, string message)
    {
        Logs.Add(new DebugLog()
        {
            level = level,
            time = time,
            sender = sender,
            message = message
        });
    }
}

public class InspectorScene : Scene
{
    public InspectorScene(int width, int height, string id) : base(width, height, id)
    {

    }

    public override void Render()
    {
        new UiBuilder(new Vector4(0, 0, (int)(Game.Window.Width * 0.2), (int)Game.Window.Height), LocalMousePosition)
            .Title("Inspector")
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
                                new UiCheckbox("Allow window resize", val.Value.AllowWindowResize, () => val.Value.SetAllowWindowResize(!val.Value.AllowWindowResize)),
                                new UiCheckbox("Always at back", val.Value.AlwaysAtBack, () => val.Value.SetAlwaysAtBack(!val.Value.AlwaysAtBack)),
                                new UiCheckbox("Always rebuild texture", val.Value.AlwaysRebuildTexture, () => val.Value.SetAlwaysRebuildTexture(!val.Value.AlwaysRebuildTexture))
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


public class ConsoleScene : Scene
{
    public ConsoleScene(int width, int height, string id) : base(width, height, id)
    {

    }

    public override void Render()
    {
        SDL_Rect rect = new()
        {
            x = 0,
            y = 0,
            w = LocalWindowSize.w,
            h = LocalWindowSize.h
        };

        SDL_SetRenderDrawColor(Game.SDLRenderer, 21, 22, 23, 255);
        SDL_RenderFillRect(Game.SDLRenderer, ref rect);

        new UiBuilder(new Vector4(0, 0, 0, 0), LocalMousePosition)
            .Title("Console")
            .ForEach(Debug.Logs, (val, idx) =>
            {
                return new UiDebugLog(val.time, val.sender, val.message);
            })
            .Render();
    }
}
