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

        RegisterCommand("cl_showfps", (args) =>
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
            Debug.Log(LogLevel.Error, "Invalid Command");
        }
    }
}

public class InspectorScene : Scene
{
    float yOffset = 0;

    public InspectorScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiColors.Background);
    }

    public override void Render()
    {
        if(MouseInsideScene) 
        {
            if(Mouse.Pressed(MB.ScrollDown)) {
                yOffset -= 10;
            }
            if(Mouse.Pressed(MB.ScrollUp)) {
                yOffset += 10;
            }
        }

        new UiBuilder(new Vector4(0, yOffset, (int)(Game.Window.Width * 0.2), (int)Game.Window.Height), Mouse.Position)
            .Title("Inspector")
            .Container(
                "Scenes",
                new UiBuilder()
                    .ForEach(SceneHandler.Scenes.ToList(), (val, idx) =>
                    {
                        var list = new UiBuilder()
                            .Title(val.Key)
                            .HAlign(
                                new UiBuilder()
                                    .Button("Load", () => SceneHandler.Load(val.Key))
                                    .Button("Unload", () => SceneHandler.Unload(val.Key))
                                    .Button("Remake", () => SceneHandler.Remake(val.Key))
                                    .BuildHAlign()
                            )
                            .Button("Apply Aspect Ratio", () => val.Value.ApplyOriginalAspectRatio())
                            .Checkbox("Allow window resize", val.Value.AllowWindowResize, () => val.Value.SetAllowWindowResize(!val.Value.AllowWindowResize))
                            .Checkbox("Always at back", val.Value.AlwaysAtBack, () => val.Value.SetAlwaysAtBack(!val.Value.AlwaysAtBack))
                            .Checkbox("Always rebuild texture", val.Value.AlwaysRebuildTexture, () => val.Value.SetAlwaysRebuildTexture(!val.Value.AlwaysRebuildTexture))
                            .Build();

                        FieldInfo[] infos = val.Value.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        List<object> exports = new() {
                            
                        };

                        foreach(var fi in infos) {
                            if (fi.IsDefined(typeof(Export), true))
                            {
                                var fival = fi.GetValue(val.Value);
                                if(fival is not null) 
                                {
                                    exports.Add(new UiText(fi.Name));
                                    if (fival.GetType() == typeof(string))
                                    {
                                        exports.Add(new UiTextField(fi.Name, fival.ToString(), (result) =>
                                        {
                                            fi.SetValue(val.Value, result);
                                        }, (result) => { }));
                                    }
                                    else if (fival.GetType() == typeof(bool))
                                    {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiCheckbox(fi.Name, (bool)fival, () =>
                                        {
                                            fi.SetValue(val.Value, !(bool)fival);
                                        }));
                                    }
                                    else if (fival.GetType() == typeof(float))
                                    {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiText($"{fi.Name} ({(float)fival})"));
                                        var expor = fi.GetCustomAttribute(typeof(Export));
                                        if (expor is not null)
                                        {
                                            Export a = (Export)expor;

                                            exports.Add(new UiSlider(a.sliderMin, a.sliderMax, (float)fival, (result) =>
                                            {
                                                fi.SetValue(val.Value, result);
                                            }));
                                        }
                                    } else if(fival.GetType() == typeof(int)) {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiText($"{fi.Name} ({(int)fival})"));
                                        var expor = fi.GetCustomAttribute(typeof(Export));
                                        if (expor is not null)
                                        {
                                            Export a = (Export)expor;

                                            exports.Add(new UiSlider(a.sliderMin, a.sliderMax, (int)fival, (result) => {
                                                fi.SetValue(val.Value, (int)result);
                                            }));
                                        }
                                    } else {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiText($"{fi.Name} has an unsupported type: {fival.GetType()}!"));
                                    }
                                }
                            }
                        }

                        if (exports.Count > 0)
                        {
                            list.Add(new UiTitle($"Exports"));
                            list.Add(exports);
                        }

                        if (idx != SceneHandler.Scenes.ToList().Count - 1)
                        {
                            list.Add(new UiSpacer());
                        }
                        
                        return list;
                    })
                    .Build()
            )
            .Container(
                "Loaded Scenes",
                new UiBuilder()
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
            .Render(out int uiHeight);

        if(MouseInsideScene) 
        {
            if(uiHeight > WindowSize.Y) {
                if(-yOffset < 0) {
                    yOffset = 0;
                }
                if(-yOffset > uiHeight - WindowSize.Y + 50) {
                    yOffset = -uiHeight + WindowSize.Y - 50;
                }
            } else {
                yOffset = 0;
            }
        }
    }
}


public class ConsoleScene : Scene
{
    string consoleInput = "";
    float yOffset = 0;
    int logsLength = 0;

    public ConsoleScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiColors.Background);
    }

    public override void Render()
    {
        if(MouseInsideScene)
        {
            if(Mouse.Pressed(MB.ScrollDown)) {
                yOffset -= 10;
            }
            if(Mouse.Pressed(MB.ScrollUp)) {
                yOffset += 10;
            }
        }

        new UiBuilder(new Vector4(0, yOffset, 0, 0), Mouse.Position)
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
            .Render(out int uiHeight);

        // Math.Clamp(yOffset, 0, uiHeight);
        if(uiHeight > WindowSize.Y) {
            if(-yOffset < 0) {
                yOffset = 0;
            }
            if(-yOffset > uiHeight - WindowSize.Y + 50) {
                yOffset = -uiHeight + WindowSize.Y - 50;
            }
        } else {
            yOffset = 0;
        }

        if(Debug.Logs.Count != logsLength)
        {
            yOffset = -uiHeight + WindowSize.Y - 200;
        }

        var submitCommand = () => {
            if(consoleInput == "")
            {
                return;
            }

            Debug.Log(LogLevel.User, consoleInput);
            string command = consoleInput.Split(" ")[0];
            List<object> args = new List<object>();

            string currentWord = "";
            bool isString = false;
            string[] boolValues = {"true", "false"}; 

            void HandleCurrentWord()
            {
                if (currentWord != String.Empty)
                {
                    float value = 0f;
                    if (float.TryParse(currentWord, out value))
                    {
                        args.Add(value);
                    }
                    if(boolValues.Contains(currentWord.ToLower())) {
                        args.Add(currentWord.ToLower() == "true");
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

        logsLength = Debug.Logs.Count;

        FUI.OverrideMousePosition(Mouse.Position);

        float height = 0;
        if(consoleInput != "" && Debug.commands.Keys.ToList().Where((command) => command == consoleInput).ToList().Count != 1 ) 
        {
            foreach(var i in Debug.commands.Keys.ToList().Where((command) => command.Contains(consoleInput)))
            {
                FUI.ButtonExt(new(10, WindowSize.Y - 40 - height - 40), i, () => {consoleInput = i + " ";}, out Vector2 bSize);
                height += bSize.Y;
            }
        }

        FUI.TextFieldExt(new(10, WindowSize.Y - 40), "consolein", consoleInput, (val) => {consoleInput = val;}, (val) => submitCommand(), null, out Vector2 size);
        FUI.Button(new(Math.Min(size.X + 20, WindowSize.X - 88), WindowSize.Y - 40), "Send", submitCommand);
        FUI.ResetMousePosition();
    }
}

public class PerformanceScene : Scene
{
    float InputFPS = 0f;
    float UpdateFPS = 0f;
    float RenderFPS = 0f;
    float ProgramFPS = 0f;
    ulong setFps = 0;

    List<float> recentInputFPS = new();
    List<float> recentUpdateFPS = new();
    List<float> recentRenderFPS = new();
    List<float> recentProgramFPS = new();

    [Export(0, 3)]
    public int Position = 0;

    int LastPosition = 0;

    [Export]
    public bool Size = false;

    bool LastSize = false;

    public PerformanceScene(int width, int height, string id) : base(width, height, id)
    {
    }

    public override void Awake()
    {
        SetClearColor(UiColors.Background);
    }

    public override void Update(double dt)
    {
        if (SDL_GetTicks64() - setFps > 1000)
        {
            setFps = SDL_GetTicks64();

            InputFPS = Game.inputFPS;
            UpdateFPS = Game.updateFPS;
            RenderFPS = Game.renderFPS;
            ProgramFPS = Game.programFPS;

            recentInputFPS.Add(InputFPS);
            recentProgramFPS.Add(ProgramFPS);

            recentRenderFPS.Add(RenderFPS);
            recentUpdateFPS.Add(UpdateFPS);
        }

        if (LastPosition != Position)
        {
            if (Position == 0)
            {
                SetRelativeWindowSize(0f, 0.89f, 0.10f, 1.001f);
            }
            else if (Position == 1)
            {
                SetRelativeWindowSize(0f, 0f, 0.1f, 0.11f);
            }
            else if (Position == 2)
            {
                SetRelativeWindowSize(0.9f, 0f, 1.001f, 0.11f);
            }
            else if (Position == 3)
            {
                SetRelativeWindowSize(0.9f, 0.89f, 1.001f, 1.001f);
            }
            Size = false;
            LastSize = false;
        }

        if(LastSize != Size)
        {
            PerformanceScene scene = SceneHandler.Get<PerformanceScene>()!;
            if (Size)
            {
                if (scene.Position < 2)
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w + 0.14f, scene.RelativeWindowSize.h);
                else
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x - 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
            } else
            {
                if (scene.Position < 2)
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w - 0.14f, scene.RelativeWindowSize.h);
                else
                    scene.SetRelativeWindowSize(scene.RelativeWindowSize.x + 0.14f, scene.RelativeWindowSize.y, scene.RelativeWindowSize.w, scene.RelativeWindowSize.h);
            }
        }
    }

    public override void Render()
    {
        //new Rectangle(new(0, 0, WindowSize.X, WindowSize.Y))
        //    .Color(new(0, 0, 0, 120))
        //    .Fill(true)
        //    .Render();

        new UiBuilder(new Vector2(0, 0), Mouse.Position)
            .Title("FPS")
            .Text(((int)ProgramFPS).ToString() + " FPS")
            .Title("Input")
            .Text(((int)InputFPS).ToString() + " FPS")
            .Render();

        new UiBuilder(new Vector2(WindowSize.X / 2, 0), Mouse.Position)
            .Title("Update")
            .Text(((int)UpdateFPS).ToString() + " FPS")
            .Title("Render")
            .Text(((int)RenderFPS).ToString() + " FPS")
            .Render();

        if(recentInputFPS.Count > 25)
        {
            recentInputFPS.RemoveAt(0);
            recentProgramFPS.RemoveAt(0);

            recentRenderFPS.RemoveAt(0);
            recentUpdateFPS.RemoveAt(0);
        }

        if(WindowSize.X > 450)
        {
            for(var i = 0; i < recentInputFPS.Count; i++)
            {
                new Rectangle(new(95 + i * 5, 10, 4, (recentProgramFPS[i] / recentProgramFPS.Max()) * 30))
                    .Color(UiColors.ContainerIdleColor)
                    .Fill(true)
                    .Render();

                new Rectangle(new(95 + i * 5, 65, 4, (recentInputFPS[i] / recentInputFPS.Max()) * 30))
                    .Color(UiColors.ContainerIdleColor)
                    .Fill(true)
                    .Render();


                new Rectangle(new(95 + i * 5 + WindowSize.X / 2, 10, 4, (recentUpdateFPS[i] / recentUpdateFPS.Max()) * 30))
                    .Color(UiColors.ContainerIdleColor)
                    .Fill(true)
                    .Render();

                new Rectangle(new(95 + i * 5 + WindowSize.X / 2, 65, 4, (recentRenderFPS[i] / recentRenderFPS.Max()) * 30))
                    .Color(UiColors.ContainerIdleColor)
                    .Fill(true)
                    .Render();
            }
        }

        LastPosition = Position;
        LastSize = Size;

        //Draw.Text(new(10, 10), Font.DefaultFont, Game.inputFPS.ToString(), 32, new(255, 255, 255, 255));
        //Draw.Text(new(10, 10), Font.DefaultFont, Game.updateFPS.ToString(), 32, new(255, 255, 255, 255));
        //Draw.Text(new(10, 10), Font.DefaultFont, Game.renderFPS.ToString(), 32, new(255, 255, 255, 255));
    }
}