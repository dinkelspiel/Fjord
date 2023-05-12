using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using Fjord.Graphics;
using Fjord.Input;
using Fjord.Scenes;
using Fjord.Ui;
using static Fjord.Helpers;
using static SDL2.SDL;

namespace Fjord.Scenes;

public class Export : Attribute
{
    public float sliderMin = 0;
    public float sliderMax = 200;

    public Export(float min, float max) {
        this.sliderMin = min;
        this.sliderMax = max;
    }

    public Export() {

    }
}

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

    public override int GetHashCode()
    {
        return HashCode.Combine(time, sender, message, level, repeat, hideInfo);
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


    public static Dictionary<string, Action<object[]>> commands = new Dictionary<string, Action<object[]>>();

    public static void RegisterCommand(string id, Action<object[]> callback) {
        if (commands.ContainsKey(id))
        {
            commands.Remove(id);
        }
        commands.Add(id, callback);
    }
    public static void RegisterCommand(string[] ids, Action<object[]> callback)
    {
        foreach (string id in ids)
            RegisterCommand(id, callback);
    }

    public static void Initialize()
    {
        SceneHandler.Register(new InspectorScene((int)(Game.Window.Width * 0.201), 1080, "Inspector")
            .SetAllowWindowResize(false)
            .SetAlwaysRebuildTexture(true)
            .SetRelativeWindowSize(0.8f, 0f, 1.0001f, 1f));

        SceneHandler.Register(new ConsoleScene((int)(Game.Window.Width * 0.2), (int)(Game.Window.Height * 0.4), "Console")
            .SetAllowWindowResize(true)
            .SetRelativeWindowSize(0.1f, 0.1f, 0.4f, 0.6f)
            .SetAlwaysRebuildTexture(true));

        SceneHandler.Register(new PerformanceScene((int)(Game.Window.Width * 0.2), (int)(Game.Window.Height * 0.4), "Performance")
            .SetRelativeWindowSize(0f, 0.89f, 0.10f, 1.001f)
            .SetAlwaysRebuildTexture(true));

        //SceneHandler.Load("Performance");
        // SceneHandler.Load("Console");
        // SceneHandler.Load("Inspector");

        RegisterCommand("clear", (args) =>
        {
            Logs = new();
        });

        RegisterCommand("showfps", (args) =>
        {
            if (args.Length < 1)
            {
                Debug.Log(SceneHandler.IsLoaded("Performance"));
                return;
            }

            if (args[0].GetType() != typeof(bool))
            {
                Debug.Log("Argument 1 is not of type bool");
                return;
            }

            if ((bool)args[0])
            {
                SceneHandler.Load("Performance");
            }
            else
            {
                SceneHandler.Unload("Performance");
            }
        });

        RegisterCommand("commands", (args) => {
            foreach(string command in commands.Keys.ToList())
            {
                Debug.Log(command);
            }
        });

        RegisterCommand(new string[3] { "q", "quit", "exit" }, (args) =>
        {
            Game.Stop();
        });
    }

    public static SDL_FRect DebugWindowOffset = new()
    {
        x = 0f,
        y = 0f,
        w = 0.2f,
        h = 0f
    };

    public static void Log(object message)
    {
        Log(LogLevel.Message, message);
    }

    public static void Log(LogLevel level, object message)
    {
        var words = message.ToString()?.Split();
        // List<string> messageSplit = message.ToString().SplitInParts(60).ToList();

        var lines = new List<string> { words![0] };
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

        // Console.WriteLine(message);

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
                            sender = names.Name,
                            message = i,
                            hideInfo = idx != 0
                        };

                        Logs.Add(logtmp);
                    }
                }
            }
        }
    }

    public static void PerformCommand(string command, object[] args) {
        if(commands.TryGetValue(command, out var command1)) {
            try {
                command1(args);
            } catch(Exception e) {
                Debug.Log(LogLevel.Error, e.ToString());
            }
        } else {
            Debug.Log(LogLevel.Error, $"Unknown Command: {command}");
        }
    }
}
