using System.Numerics;
using System.Reflection;
using Fjord.Input;
using Fjord.Ui;
using Fjord.Graphics;

namespace Fjord.Scenes;

public class InspectorScene : Scene
{
    float yOffset = 0;

    public InspectorScene(int width, int height, string id) : base(width, height, id)
    {
        SetClearColor(UiStyles.Background);
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
                                        exports.Add(new UiTextField(fi.Name, fival.ToString()!, (result) =>
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


