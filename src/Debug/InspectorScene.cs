using System.Numerics;
using System.Reflection;
using Fjord.Input;
using Fjord.Ui;
using Fjord.Graphics;
using static SDL2.SDL;

namespace Fjord.Scenes;

public class InspectorScene : Scene
{
    float yOffset = 0;
    public string SelectedScene = "";

    public InspectorScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiStyles.Background);
        // SetUpdateOnlyIfActive(true);
    }

    public override void Update()
    {
        if(SelectedScene == "")
        {
            SelectedScene = SceneHandler.Scenes.First(e => e.Key != "Inspector" && e.Key != "Console" && e.Key != "Performance").Key;
        }

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
            .Title($"Inspector for {SelectedScene}")
            .If(SceneHandler.Get(SelectedScene).Paused, new UiButton("Resume", () => SceneHandler.Get(SelectedScene).Paused = false))
            .If(!SceneHandler.Get(SelectedScene).Paused, new UiButton("Pause", () => SceneHandler.Get(SelectedScene).Paused = true))
            .ForEach(SceneHandler.Get(SelectedScene).Entities, (e) => {
                if(e.excludeFromInspector)
                {
                    return null;
                }
                
                return new UiBuilder()
                    .Title(e.name == null ? e.ToString()! : e.name)
                    .ForEach(e.Components, (c) => {
                        var list = new UiBuilder()
                            .Title(c.ToString()!)
                            .Build();

                        FieldInfo[] infos = c.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        List<object> exports = new() {
                            
                        };

                        foreach(var fi in infos) {
                            if (fi.IsDefined(typeof(Export), true))
                            {
                                var fival = fi.GetValue(c);
                                if(fival is not null) 
                                {
                                    exports.Add(new UiText(fi.Name));
                                    if (fival.GetType() == typeof(string))
                                    {
                                        exports.Add(new UiTextField(fi.Name, fival.ToString()!, (result) =>
                                        {
                                            fi.SetValue(c, result);
                                        }, (result) => { }));
                                    }
                                    else if (fival.GetType() == typeof(bool))
                                    {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiCheckbox(fi.Name, (bool)fival, () =>
                                        {
                                            fi.SetValue(c, !(bool)fival);
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
                                                fi.SetValue(c, result);
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
                                                fi.SetValue(c, (int)result);
                                            }));
                                        }
                                    } else if(fival.GetType() == typeof(Vector2))
                                    {
                                        exports.RemoveAt(exports.Count - 1);
                                        exports.Add(new UiText($"{fi.Name}"));
                                        var expor = fi.GetCustomAttribute(typeof(Export));
                                        if (expor is not null)
                                        {
                                            Export a = (Export)expor;

                                            exports.Add(new HAlign<UiComponent>() {
                                                new UiSlider(a.sliderMin, a.sliderMax, ((Vector2)fival).X, (result) => {
                                                    fi.SetValue(c, new Vector2(result, ((Vector2)fival).Y));
                                                }),
                                                new UiTitle("X")
                                            });

                                            exports.Add(new HAlign<UiComponent>() {
                                                new UiSlider(a.sliderMin, a.sliderMax, ((Vector2)fival).Y, (result) => {
                                                    fi.SetValue(c, new Vector2(((Vector2)fival).X, result));
                                                }),
                                                new UiTitle("Y")
                                            });
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
                            list.AddRange(exports);
                            return list;
                        }

                        return null;
                    })
                    .Build();
            })
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


