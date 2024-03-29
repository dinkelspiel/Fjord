using System.Numerics;
using Fjord.Input;
using Fjord.Ui;
using Fjord.Graphics;

namespace Fjord.Scenes;

public class ConsoleScene : Scene
{
    string consoleInput = "";
    float yOffset = 0;
    int logsLength = 0;
    float scrollY = 0;
    float scrollYOffset = 0;
    bool pressedScroll = false;

    public ConsoleScene(int width, int height) : base(width, height)
    {
        ClearColor = UiStyles.Background;
        UpdateOnlyIfActive = false;
    }

    public override void Update()
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
            .Title($"Console")
            .ForEach(Debug.Logs, (val, idx) =>
            {
                switch(val.level) {
                    case LogLevel.User: {
                        return new UiText(val.message, new(170, 170, 170, 255));
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

            Debug.Log(LogLevel.User, $"> {consoleInput}");
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
                        args.Add((float)value);
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

        FUI.OverMousePosition = Mouse.Position;

        float height = 0;
        if(consoleInput != "" && Debug.commands.Keys.ToList().Where((command) => command == consoleInput).ToList().Count != 1 ) 
        {
            foreach(var i in Debug.commands.Keys.ToList().Where((command) => command.Contains(consoleInput)))
            {
                if(GlobalKeyboard.Pressed(Key.TAB))
                {
                    consoleInput = i;
                }

                FUI.ButtonExt(new Vector2(10, WindowSize.Y - 40 - height - 55), i, () => {consoleInput = i + " ";}, out Vector2 bSize);
                height += bSize.Y;
            }
        }

        float h = -uiHeight + WindowSize.Y - 70;

        if(pressedScroll && !GlobalMouse.Down(MB.Left))
        {
            pressedScroll = false;
        }

        float hh =  WindowSize.Y / (Math.Abs(h) + WindowSize.Y);
        if(pressedScroll)
        {
            scrollY = Mouse.Position.Y + scrollYOffset;
            scrollY = Math.Clamp(scrollY, 0, WindowSize.Y - (hh * WindowSize.Y));
            yOffset = (scrollY / (WindowSize.Y - (hh * WindowSize.Y))) * (-uiHeight + WindowSize.Y - 70);
        } else {
            scrollY = (yOffset / (-uiHeight + WindowSize.Y - 70)) * (WindowSize.Y - (hh * WindowSize.Y));
        }

        FUI.TextFieldExt(new(10, WindowSize.Y - 50), WindowSize.X - 20, "consolein", consoleInput, (val) => {consoleInput = val;}, (val) => submitCommand(), null, out Vector2 size);
        // FUI.Button(new(Math.Min(size.X + 20, WindowSize.X - 88), WindowSize.Y - 50), "Send", submitCommand);

        if(h < 0)
        {
            FUI.ButtonExt(new Vector4(WindowSize.X - 20, scrollY, 15, hh * WindowSize.Y), "", () => {
                pressedScroll = true;
                scrollYOffset = scrollY - Mouse.Position.Y;
            });
        }
        FUI.OverMousePosition = null;
    }
}