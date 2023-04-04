using System.Diagnostics;
using System.Numerics;
using Fjord.Graphics;
using Fjord.Scenes;
using Fjord.Ui;
using static Fjord.Helpers;
using static SDL2.SDL;

namespace Fjord.Scenes;

public struct DebugLog
{
    public string time;
    public string sender;
    public string message;
    public LogLevel level;
    public bool hideInfo;
}

public enum LogLevel
{
    User,
    Message,
    Warning,
    Error,
}

public static class Debug {

    public static List<DebugLog> Logs = new List<DebugLog>();

    public static Dictionary<string, Action<object[]>> commands = new Dictionary<string, Action<object[]>>();

    public static void Initialize()
    {
        SceneHandler.Register(new InspectorScene((int)(Game.Window.Width * 0.2), 1080, "inspector")
            .SetAllowWindowResize(false)
            .SetRelativeWindowSize(0.8f, 0f, 1f, 1f));

        SceneHandler.Register(new ConsoleScene((int)(Game.Window.Width * 0.2), (int)(Game.Window.Height * 0.4), "console")
            .SetAllowWindowResize(true)
            .SetRelativeWindowSize(0.1f, 0.1f, 0.3f, 0.5f)
            .SetAlwaysRebuildTexture(true));

        commands.Add("closewindow", (args) => {
            if(args.Length > 0) {
                if(SceneHandler.Scenes.ContainsKey((string)args[0])) {
                    if(SceneHandler.IsLoaded((string)args[0])) {
                        SceneHandler.Unload((string)args[0]);
                        Debug.Log(LogLevel.Message, $"Unloaded {(string)args[0]}");
                    } else {
                        Debug.Log(LogLevel.Warning, $"{(string)args[0]} is not loaded");
                    }
                } else {
                    Debug.Log(LogLevel.Warning, $"No scene named {(string)args[0]}");
                }
            } else {
                Debug.Log(LogLevel.Error, $"No argument provided");
            }
        });
    }

    public static SDL_FRect DebugWindowOffset = new()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };

    public static void Log(LogLevel level, string message)
    {
        List<string> messageSplit = message.ToString().SplitInParts(60).ToList();
        
        StackTrace stackTrace = new StackTrace(); 
        int idx = -1;
        foreach(string i in messageSplit) {
            idx++;
            Logs.Add(new DebugLog() {
                level = level,
                time = DateTime.Now.ToString("hh:mm:ss"),
                sender = stackTrace.GetFrame(1).GetMethod().Name,
                message = i,
                hideInfo = idx != 0 ? true : false
            });
        }
    }

    public static void Log(string message)
    {
        Logs.Add(new DebugLog()
        {
            level = LogLevel.User,
            time = DateTime.Now.ToString("hh:mm:ss"),
            sender = "User",
            message = message
        });
    }

    public static void PerformCommand(string command, object[] args) {
        if(commands.ContainsKey(command)) {
            try {
                commands[command](args);
            } catch(Exception e) {
                Debug.Log(LogLevel.Error, e.ToString());
            }
        } else {
            Debug.Log(LogLevel.Error, "Invalid Command");
        }
    }
}

public class InspectorScene : Scene
{
    public InspectorScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiColors.Background);
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
    string consoleInput = "";

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

        SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.Background);
        SDL_RenderFillRect(Game.SDLRenderer, ref rect);

        new UiBuilder(new Vector4(0, 0, 0, 0), LocalMousePosition)
            .Title("Console")
            .ForEach(Debug.Logs, (val, idx) =>
            {
                switch(val.level) {
                    case LogLevel.User: {
                        return new UiText(val.message);
                    }
                    default: {
                        return new UiDebugLog(val.level, val.time, val.sender, val.message, val.hideInfo);
                    } 
                }
            })
            .Render();

        FUI.OverrideMousePosition(LocalMousePosition);
        FUI.TextFieldExt(new(10, LocalWindowSize.h - 40), "consolein", consoleInput, (val) => {consoleInput = val;}, null, out Vector2 size);
        FUI.Button(new(Math.Min(size.X + 20, LocalWindowSize.w - 88), LocalWindowSize.h - 40), "Send", () => {
            Debug.Log(consoleInput);
            string command = consoleInput.Split(" ")[0];
            List<object> args = new List<object>();

            string currentWord = "";
            bool isString = false;

            void HandleCurrentWord() {
                if(currentWord != String.Empty) {
                    float value = 0f;
                    if(float.TryParse(currentWord, out value)) {
                        args.Add(value);
                    } else {
                        args.Add(currentWord);
                    }
                }
                currentWord = "";
            }

            foreach(char c in String.Join(" ", consoleInput.Split(" ").ToList().Skip(1))) {
                if(c == '"') {
                    isString = !isString;
                    if(!isString) {
                        HandleCurrentWord();
                    }
                    continue;
                }
                if(isString) {
                    currentWord += c;
                } else if(c != ' ') {
                    currentWord += c;
                } else {
                    HandleCurrentWord();
                }
            }
            if(currentWord != String.Empty) {
                HandleCurrentWord();
            }

            Debug.PerformCommand(command, args.ToArray());
            consoleInput = "";
        });
        FUI.ResetMousePosition();
    }
}