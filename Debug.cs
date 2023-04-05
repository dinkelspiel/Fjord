using System.Diagnostics;
using System.Numerics;
using Fjord.Graphics;
using Fjord.Input;
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
    public int repeat;
    public bool hideInfo;

    public override bool Equals(object? obj)
    {
        return obj is DebugLog log &&
               sender == log.sender &&
               message == log.message &&
               level == log.level &&
               hideInfo == log.hideInfo;
    }
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
    internal static DebugLog lastTopMessage;

    public static Dictionary<string, Action<object[]>> commands = new Dictionary<string, Action<object[]>>();

    public static void RegisterCommand(string id, Action<object[]> callback) {
        commands.Add(id, callback);
    } 

    public static void Initialize()
    {
        SceneHandler.Register(new InspectorScene((int)(Game.Window.Width * 0.2), 1080, "inspector")
            .SetAllowWindowResize(false)
            .SetAlwaysRebuildTexture(true)
            .SetRelativeWindowSize(0.8f, 0f, 1f, 1f));

        SceneHandler.Register(new ConsoleScene((int)(Game.Window.Width * 0.2), (int)(Game.Window.Height * 0.4), "console")
            .SetAllowWindowResize(true)
            .SetRelativeWindowSize(0.1f, 0.1f, 0.3f, 0.5f)
            .SetAlwaysRebuildTexture(true));

        RegisterCommand("closewindow", (args) => {
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

        RegisterCommand("clear", (args) =>
        {
            Logs = new();
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
        var words = message.Split();
        // List<string> messageSplit = message.ToString().SplitInParts(60).ToList();

        var lines = new List<string> { words[0] };
        var lineNum = 0;
        for(int i = 1; i < words.Length; i++)
        {
            if(lines[lineNum].Length + words[i].Length + 1 <= 120)
                lines[lineNum] += " " + words[i];
            else
            {
                lines.Add(words[i]);
                lineNum++;
            }
        }

        StackTrace stackTrace = new StackTrace(); 
        StackFrame? stackFrame = stackTrace.GetFrame(1);

        int idx = -1;
        foreach(string i in lines) {
            idx++;
            if(stackFrame is not null) {
                System.Reflection.MethodBase? methodBase = stackFrame.GetMethod();
                if(methodBase is not null) {
                    var names = methodBase.DeclaringType;
                    if (names is not null) {
                        var logtmp = new DebugLog() {
                            level = level,
                            time = DateTime.Now.ToString("hh:mm:ss"),
                            sender = names.Namespace + "." + names.Name,
                            message = i,
                            hideInfo = idx != 0
                        };

                        Logs.Add(logtmp);
                        //lastTopMessage = logtmp;

                        //if (level != LogLevel.User)
                        //{
                        //    if (idx == 0)
                        //    {
                        //        Console.WriteLine(Logs.Count.ToString());
                        //        if (Logs.Count > 0)
                        //        {
                        //            Console.WriteLine("Help");
                        //            if (!lastTopMessage.Equals(logtmp))
                        //            {
                        //                Console.WriteLine("Help2");
                        //                Logs.Add(logtmp);
                        //                lastTopMessage = logtmp;
                        //            }
                        //            else
                        //            {
                        //                logtmp.repeat = Logs[Logs.Count - 1].repeat + 1;
                        //                Logs[Logs.Count - 1] = logtmp;
                        //                return;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            Logs.Add(logtmp);
                        //            lastTopMessage = logtmp;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Logs.Add(logtmp);
                        //    }
                        //} else
                        //{
                        //    Logs.Add(logtmp);
                        //}
                    }
                }
            }
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
    float yOffset = 0;

    public ConsoleScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiColors.Background);
    }

    public override void Render()
    {
        if(Mouse.ScrollDown) {
            yOffset -= 10;
        }
        if(Mouse.ScrollUp) {
            yOffset += 10;
        }

        new UiBuilder(new Vector4(0, yOffset, 0, 0), LocalMousePosition)
            .Title("Console")
            .ForEach(Debug.Logs, (val, idx) =>
            {
                switch(val.level) {
                    case LogLevel.User: {
                        return new UiText(val.message);
                    }
                    default: {
                        return new UiDebugLog(val.level, val.time, val.sender, val.message, val.hideInfo, val.repeat);
                    } 
                }
            })
            .Render();

        var submitCommand = () => {
            Debug.Log(consoleInput);
            string command = consoleInput.Split(" ")[0];
            List<object> args = new List<object>();

            string currentWord = "";
            bool isString = false;

            void HandleCurrentWord()
            {
                if (currentWord != String.Empty)
                {
                    float value = 0f;
                    if (float.TryParse(currentWord, out value))
                    {
                        args.Add(value);
                    }
                    else
                    {
                        args.Add(currentWord);
                    }
                }
                currentWord = "";
            }

            foreach (char c in String.Join(" ", consoleInput.Split(" ").ToList().Skip(1)))
            {
                if (c == '"')
                {
                    isString = !isString;
                    if (!isString)
                    {
                        HandleCurrentWord();
                    }
                    continue;
                }
                if (isString)
                {
                    currentWord += c;
                }
                else if (c != ' ')
                {
                    currentWord += c;
                }
                else
                {
                    HandleCurrentWord();
                }
            }
            if (currentWord != String.Empty)
            {
                HandleCurrentWord();
            }

            Debug.PerformCommand(command, args.ToArray());
            consoleInput = "";
        };

        FUI.OverrideMousePosition(LocalMousePosition);
        FUI.TextFieldExt(new(10, LocalWindowSize.h - 40), "consolein", consoleInput, (val) => {consoleInput = val;}, (val) => submitCommand(), null, out Vector2 size);
        FUI.Button(new(Math.Min(size.X + 20, LocalWindowSize.w - 88), LocalWindowSize.h - 40), "Send", submitCommand);
        FUI.ResetMousePosition();
    }
}
