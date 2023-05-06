using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Fjord.Input;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_gfx;
using Fjord.Graphics;
using System.ComponentModel;
using static Fjord.Helpers;
using Fjord.Scenes;

namespace Fjord.Ui;

public static class FUI
{
    internal static string? selectedTextField = null;
    internal static string? selectedTextFieldValue = null;
    internal static Action<string>? selectedTextFieldOnChange = null;
    internal static Action<string>? selectedTextFieldOnSumbit = null;
    private static Vector2 UiRenderOffset = new();
    private static Vector2? OverMousePosition = null;
    private static Dictionary<string, List<Circle>> resizeRectCache = new();
    private static (string, int) pressedCorner = new("", -1);
    internal static Dictionary<string, bool> containerShown = new();
    public static void OverrideMousePosition(Vector2 MousePosition)
    {
        OverMousePosition = MousePosition;
    }

    public static void SetRenderOffset(Vector2 offset)
    {
        UiRenderOffset = offset;
    }

    public static void ResetRenderOffset()
    {
        UiRenderOffset = new();
    }

    public static void ResetMousePosition()
    {
        OverMousePosition = null;
    }

    public static void ButtonExt(Vector2 position, string text, Action callback, out Vector2 size)
    {
        Vector2 TextSize = Font.DrawSize(Font.GetDefaultFont(), text, 16, new(255, 0, 0, 255));

        SDL_FRect rect = new()
        {
            x = (int)position.X,
            y = (int)position.Y,
            w = TextSize.X + 40,
            h = TextSize.Y + 7
        };

        Vector4 col;

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, Helpers.FRectToRect(rect)))
        {
            col =  UiStyles.ContainerHoverColor;
            if (GlobalMouse.Down(MB.Left))
            {
                col = UiStyles.ContainerPressedColor;
            }
        }
        else
        {
            col = UiStyles.ContainerIdleColor;
        }

        new Rectangle(new(position.X, position.Y, TextSize.X + 40, TextSize.Y + 7))
            .Color(col)
            .Fill(true)
            .BorderRadius(UiStyles.GlobalBorderRadius)
            .Render();

        Draw.Text(position + new Vector2(5, 3), Font.GetDefaultFont(), text, 16, new(255, 255, 255, 255));

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, Helpers.FRectToRect(rect)))
        {
            if (GlobalMouse.Pressed(MB.Left))
            {
                callback();
            }
        }

        size = new Vector2(rect.w, rect.h);
    }

    public static void ButtonExt(Vector4 v4rect, string text, Action callback, bool runContinuously=false)
    {
        Vector2 TextSize = Font.DrawSize(Font.GetDefaultFont(), text, 16, new(255, 0, 0, 255));

        SDL_FRect rect = new()
        {
            x = v4rect.X,
            y = v4rect.Y,
            w = v4rect.Z,
            h = v4rect.W
        };

        Vector4 col;

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, Helpers.FRectToRect(rect)))
        {
            col =  UiStyles.ContainerHoverColor;
            if (GlobalMouse.Down(MB.Left))
            {
                col = UiStyles.ContainerPressedColor;
            }
        }
        else
        {
            col = UiStyles.ContainerIdleColor;
        }

        new Rectangle(v4rect)
            .Color(col)
            .Fill(true)
            .BorderRadius(UiStyles.GlobalBorderRadius)
            .Render();

        Draw.Text(Vector2.Add(new(v4rect.X, v4rect.Y), new Vector2(5, 3)), Font.GetDefaultFont(), text, 16, new(255, 255, 255, 255));

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, Helpers.FRectToRect(rect)))
        {
            if(!runContinuously) 
            {
                if (GlobalMouse.Pressed(MB.Left))
                {
                    callback();
                }
            } else {
                if (GlobalMouse.Down(MB.Left))
                {
                    callback();
                }
            }
        }
    }

    public static void Button(Vector2 position, string text, Action callback)
    {
        ButtonExt(position, text, callback, out Vector2 size);
    }

    public static void TextFieldExt(Vector2 position, string id, string value, Action<string> onChange, Action<string> onSubmit, string? placeholder, out Vector2 fieldsize)
    {
        // Font.Draw(new Vector2(indent * 10 + UiRenderOffset.X + 5, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.value, 14, UiStyles.TextColor);
        Vector2 size = Font.DrawSize(Font.GetDefaultFont(), "asdasd", 16, UiStyles.TextColor);
        Vector2 size2 = Font.DrawSize(Font.GetDefaultFont(), value, 16, UiStyles.TextColor);

        Vector4 rect = new()
        {
            X = (int)(position.X),
            Y = (int)(position.Y),
            Z = (int)Math.Max(Math.Max(size.X, size2.X) + 40, 200),
            W = (int)size.Y + 7
        };

        if(FUI.selectedTextField == id) {
            FUI.selectedTextFieldValue = value;
        }

        Vector4 color;

        if (!Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect))
        {
            if (GlobalMouse.Pressed(MB.Left))
            {
                if (FUI.selectedTextField == id)
                {
                    FUI.selectedTextField = null;
                }
            }
        }

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect))
        {
            if (GlobalMouse.Pressed(MB.Left))
            {
                if (FUI.selectedTextField != id)
                {
                    FUI.selectedTextField = id;
                    FUI.selectedTextFieldOnChange = (val) =>
                    {
                        onChange(val);
                    };
                    FUI.selectedTextFieldOnSumbit = (val) =>
                    {
                        onSubmit(val);
                    };
                    SDL_StartTextInput();
                }
                else
                {
                    FUI.selectedTextField = null;
                    SDL_StopTextInput();
                }
            }
            if (FUI.selectedTextField != id)
                color = UiStyles.ContainerHoverColor;
            else
                color = UiStyles.ContainerHoverPressedColor;
        }
        else if (FUI.selectedTextField == id)
        {
            color = UiStyles.ContainerPressedColor;
        }
        else
        {
            color = UiStyles.ContainerIdleColor;
        }

        fieldsize = new() {
            X = Math.Max(Math.Max(size.X, size2.X) + 40, 200),
            Y = size.Y + 7
        };

        new Rectangle(rect)
            .Color(color)
            .Fill(true)
            .BorderRadius(UiStyles.GlobalBorderRadius)
            .Render();

        //SDL_RenderFillFRect(Game.SDLRenderer, ref rect);
        Draw.Text(position + new Vector2(5, 3), Font.GetDefaultFont(), value, 16, UiStyles.TextColor);
    }

    public static void TextField(Vector2 position, string id, string value, Action<string> onChange, Action<string> onSubmit, string? placeholder=null) 
    {
        TextFieldExt(position, id, value, onChange, onSubmit, placeholder, out Vector2 fieldsize);
    }

    public static void SliderExt(Vector2 position, float min, float max, float value, Action<float> onChange, out Vector2 fieldsize)
    {
        Vector2 size = Font.DrawSize(Font.GetDefaultFont(), "10", 16, UiStyles.TextColor);

        SDL_Rect rect = new()
        {
            x = (int)(position.X),
            y = (int)(position.Y + (size.Y + 7) / 4),
            w = (int)210,
            h = (int)((size.Y + 7) / 2)
        };

        SDL_Color col = new();

        fieldsize = new(rect.w, rect.h);

        float normalizedValue = (value - min) / (max - min);

        SDL_Rect thumbrect = new()
        {
            x = (int)(position.X + (normalizedValue * 200) - (size.Y + 7) / 2),
            y = (int)(position.Y),
            w = (int)(size.Y + 7),
            h = (int)(size.Y + 7)
        };

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, thumbrect))
        {
            col = UiStyles.ContainerHoverColor.ToCol();
            if(GlobalMouse.Down(MB.Left)) {
                col = UiStyles.ContainerPressedColor.ToCol();
                // Debug.Log((position.X - (OverMousePosition.HasValue ? OverMousePosition.Value.X : Mouse.Position.X)).ToString());
            }
        }
        else
        {
            col = UiStyles.ContainerIdleColor.ToCol();
        }

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect))
        {
            if(GlobalMouse.Down(MB.Left)) {
                float offset = Math.Abs(position.X - (OverMousePosition.HasValue ? OverMousePosition.Value.X : GlobalMouse.Position.X));

                float lerp = offset / 200;

                float val = Math.Clamp(Lerp(min, max, lerp), min, max);

                onChange(val);
            }
        }

        new Rectangle(new(rect.x, rect.y, rect.w, rect.h))
            .Color(UiStyles.ContainerIdleColor)
            .Fill(true)
            .BorderRadius(UiStyles.GlobalBorderRadius)
            .Render();

        float radius = (size.Y + 7) / 2;
        new Circle(new(thumbrect.x + radius, thumbrect.y + radius), radius)
            .Fill(true)
            .Color(col.ToV4())
            .Render();

        // Font.Draw(position + new Vector2(5, 3), Font.GetDefaultFont(), value.ToString(), 16, UiStyles.TextColor);
    }

    public static void Slider(Vector2 position, float min, float max, float value, Action<float> onChange) {
        SliderExt(position, min, max, value, onChange, out Vector2 size);
    }

    public static void ResizeableRectangle(ref SDL_FRect rect, string id, Vector2? aspectRatio=null)
    {
        int i = 0;

        SDL_Rect localRect = new();
        while(i < 3) {
            i++;
            localRect = new SDL_Rect()
            {
                x = (int)(rect.x * Game.Window.Width - i),
                y = (int)(rect.y * Game.Window.Height - i),
                w = (int)((rect.w - rect.x) * Game.Window.Width + i * 2),
                h = (int)((rect.h - rect.y) * Game.Window.Height + i * 2)
            };

            SDL.SDL_SetRenderDrawColor(Game.SDLRenderer, 238, 170, 0,255);
            SDL_RenderDrawRect(Game.SDLRenderer, ref localRect);
            SDL.SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
        }
        localRect = new SDL_Rect()
        {
            x = (int)(rect.x * Game.Window.Width),
            y = (int)(rect.y * Game.Window.Height),
            w = (int)((rect.w - rect.x) * Game.Window.Width),
            h = (int)((rect.h - rect.y) * Game.Window.Height)
        };

        short radius = 12;

        new Circle(new(localRect.x, localRect.y), radius / 2)
            .Color(new(238, 170, 0, 255))
            .Fill(true)
            .HoverAnimation(SampleAnimations.CirclePulseAnimation(id + "0"))
            .Render();
        new Circle(new(localRect.x + localRect.w, localRect.y), radius / 2)
            .Color(new(238, 170, 0, 255))
            .Fill(true)
            .HoverAnimation(SampleAnimations.CirclePulseAnimation(id + "1"))
            .Render();
        new Circle(new(localRect.x, localRect.y + localRect.h), radius / 2)
            .Color(new(238, 170, 0, 255))
            .Fill(true)
            .HoverAnimation(SampleAnimations.CirclePulseAnimation(id + "2"))
            .Render();
        new Circle(new(localRect.x + localRect.w, localRect.y + localRect.h), radius / 2)
            .Color(new(238, 170, 0, 255))
            .Fill(true)
            .HoverAnimation(SampleAnimations.CirclePulseAnimation(id + "3"))
            .Render();
        

        if (GlobalMouse.Down(MB.Left)){
            if (Helpers.PointDistance(new Vector2(localRect.x, localRect.y), GlobalMouse.Position) < radius)
            {
                pressedCorner = (id, 0);
            } else if (Helpers.PointDistance(new Vector2(localRect.x + localRect.w, localRect.y), GlobalMouse.Position) < radius) {
                pressedCorner = (id, 1);
            } else if (Helpers.PointDistance(new Vector2(localRect.x, localRect.y + localRect.h), GlobalMouse.Position) < radius) {
                pressedCorner = (id, 2);
            } else if (Helpers.PointDistance(new Vector2(localRect.x + localRect.w, localRect.y + localRect.h), GlobalMouse.Position) < radius) {
                pressedCorner = (id, 3);
            }
        } else {
            pressedCorner = ("", -1);
        }

        if (pressedCorner.Item1 == id){
            switch (pressedCorner.Item2) {
                case 0: {
                    rect.x = GlobalMouse.Position.X / Game.Window.Width;
                    rect.y = GlobalMouse.Position.Y / Game.Window.Height;
                } break;
                case 1: {
                    rect.w = GlobalMouse.Position.X / Game.Window.Width;
                    rect.y = GlobalMouse.Position.Y / Game.Window.Height;
                } break;
                case 2: {
                    rect.x = GlobalMouse.Position.X / Game.Window.Width;
                    rect.h = GlobalMouse.Position.Y / Game.Window.Height;
                } break;
                case 3: {
                    rect.w = GlobalMouse.Position.X / Game.Window.Width;
                    rect.h = GlobalMouse.Position.Y / Game.Window.Height;
                } break;
            }
        }
    }

    public static float Render(List<object> components, ref float yOffset, int indent=0)
    {
        foreach (object componentObj in components)
        {
            if (componentObj.GetType() == typeof(UiButton))
            {
                UiButton component = (UiButton)componentObj;
                ButtonExt(new Vector2(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), component.text, component.callback, out Vector2 size);
                yOffset += size.Y + 5;
            }
            else if (componentObj.GetType() == typeof(UiTitle))
            {
                UiTitle component = (UiTitle)componentObj;
                Draw.Text(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), Font.GetDefaultFont(), component.text, 18, UiStyles.TextColor);
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.text, 18, new(0, 0, 0, 255));
                yOffset += size.Y + 5;
            }
            else if (componentObj.GetType() == typeof(UiText))
            {
                UiText component = (UiText)componentObj;
                Draw.Text(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), Font.GetDefaultFont(), component.text, 14, component.overrideColor.HasValue ? component.overrideColor.Value : UiStyles.TextColor);
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.text, 14, new(0, 0, 0, 255));
                yOffset += size.Y + 5;
            }
            else if (componentObj.GetType() == typeof(UiSpacer))
            {
                Vector4 spacerRect = new()
                {
                    X = (int)(indent * 10 + UiRenderOffset.X),
                    Y = (int)(yOffset + UiRenderOffset.Y),
                    Z = 200,
                    W = 1
                };

                new Rectangle(spacerRect)
                    .Color(new(50, 50, 50, 255))
                    .Fill(true)
                    .Render();

                yOffset += 5;
            }
            else if (componentObj.GetType() == typeof(UiCheckbox))
            {
                UiCheckbox component = (UiCheckbox)componentObj;

                Vector4 v4rect = new Vector4()
                {
                    X = (int)(indent * 10 + UiRenderOffset.X),
                    Y = (int)(yOffset + UiRenderOffset.Y),
                    Z = 20,
                    W = 20
                };

                Rectangle rect = new Rectangle(v4rect)
                    .Fill(true)
                    .BorderRadius(UiStyles.GlobalBorderRadius);

                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, v4rect))
                {
                    rect.Color(UiStyles.ContainerHoverColor);
                    if (GlobalMouse.Down(MB.Left))
                    {
                        rect.Color(UiStyles.ContainerPressedColor);
                    }
                }
                else if (component.value)
                {
                    rect.Color(UiStyles.ContainerPressedColor);
                }
                else
                {
                    rect.Color(UiStyles.ContainerIdleColor);
                }

                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, v4rect) && GlobalMouse.Pressed(MB.Left))
                {
                    component.callback();
                }

                rect.Render();

                Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + 25, yOffset + UiRenderOffset.Y), Font.DefaultFont, component.text, 16, new(255, 255, 255, 255));

                yOffset += 25;
            } else if (componentObj.GetType() == typeof(UiDebugLog))
            {
                UiDebugLog component = (UiDebugLog)componentObj;
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.time, 14, UiStyles.SuccessTextColor);

                SDL_Rect rect = new SDL_Rect()
                {
                    x = (int)(indent * 10 + UiRenderOffset.X), 
                    y = (int)(yOffset + UiRenderOffset.Y),
                    w = (int)(size.X + 10),
                    h = (int)(size.Y + 6)
                };

                SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.SuccessBackgroundColor.ToCol());
                SDL_RenderFillRect(Game.SDLRenderer, ref rect);

                Vector2 size2 = Font.DrawSize(Font.GetDefaultFont(), component.sender, 14, UiStyles.SuccessTextColor);

                Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + 5, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.time, 14, UiStyles.SuccessTextColor);
                Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + size.X + 20, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.sender, 14, UiStyles.SuccessTextColor);

                switch(component.level) {
                    case Scenes.LogLevel.Error: {
                        Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + size.X + 20 + size2.X + 10, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.message, 14, UiStyles.ErrorTextColor);
                    } break;
                    case Scenes.LogLevel.Warning: {
                        Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + size.X + 20 + size2.X + 10, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.message, 14, UiStyles.WarningTextColor);
                    } break;
                    default: {
                        Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + size.X + 20 + size2.X + 10, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), component.message, 14, UiStyles.TextColor);
                    } break;
                }

                if(component.repeat > 0) {
                    Vector2 size3 = Font.DrawSize(Font.GetDefaultFont(), component.message, 14, UiStyles.TextColor);
                    Draw.Text(new Vector2(indent * 10 + UiRenderOffset.X + size.X + 20 + size2.X + 10 + size3.X + 10, yOffset + UiRenderOffset.Y + 3), Font.GetDefaultFont(), "(" + component.repeat.ToString() + "x)", 14, UiStyles.ErrorTextColor);
                }

                yOffset += size.Y + 10;
            }
            else if (componentObj.GetType() == typeof(UiTextField)) 
            {
                UiTextField component = (UiTextField)componentObj;

                FUI.TextFieldExt(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), component.id, component.value, component.onChange, component.onSubmit, component.placeholder, out Vector2 size);
                
                yOffset += size.Y + 5;
            }
            else if (componentObj.GetType() == typeof(UiSlider)) 
            {
                UiSlider component = (UiSlider)componentObj;

                FUI.SliderExt(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), component.min, component.max, component.value, component.onChange, out Vector2 size);

                yOffset += size.Y + 15 + (size.Y + 5) / 2;
            }
            else if (componentObj.GetType() == typeof(UiImage))
            {
                UiImage component = (UiImage)componentObj;
                Texture tex = (Texture)component.texture.Clone();
                
                Vector2 mult = new(40 / tex.textureSize.Y, 40 / tex.textureSize.Y);
                tex.SizeMultiplier(mult).Position(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y)).Alpha(255).Angle(0).Depth(-5000).Render();

                yOffset += 45;
            }
            else if (componentObj.GetType() == typeof(List<object>))
            {
                List<object> component = (List<object>)componentObj;

                if(component[0].GetType() == typeof(UiTitle)) 
                {
                    UiTitle label = (UiTitle)component[0];

                    if(!FUI.containerShown.ContainsKey(label.text + component.Count))
                    {
                        FUI.containerShown.Add(label.text + component.Count, true);
                    }

                    Vector2 size = Font.DrawSize(Font.GetDefaultFont(), label.text + component.Count, 18, new(0, 0, 0, 255));
                    Vector4 rect = new Vector4(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), size.X + 15, size.Y);

                    float xpos;
                    float ypos;
                    (int, int) p2offset;
                    (int, int) p3offset;
                    xpos = indent * 10 + UiRenderOffset.X + size.X + 2;

                    if(FUI.containerShown[label.text + component.Count])
                    {
                        ypos = yOffset + UiRenderOffset.Y + 10 + 10;
                        p2offset = (5, -10);
                        p3offset = (-5, -10);
                    }
                    else {
                        ypos = yOffset + UiRenderOffset.Y + 10;
                        p2offset = (5, 10);
                        p3offset = (-5, 10);
                    }

                    new Geometry()
                        .AddVertex(new SDL_Vertex()
                        {
                            position = new SDL_FPoint()
                            {
                                x = xpos,
                                y = ypos
                            },
                            color = new SDL_Color()
                            {
                                r = 255, 
                                g = 255,
                                b = 255,
                                a = 255
                            },
                            tex_coord = new SDL_FPoint()
                            {
                                x = 0,
                                y = 0
                            }
                        })
                        .AddVertex(new SDL_Vertex()
                        {
                            position = new SDL_FPoint()
                            {
                                x = xpos + p2offset.Item1,
                                y = ypos + p2offset.Item2
                            },
                            color = new SDL_Color()
                            {
                                r = 255, 
                                g = 255,
                                b = 255,
                                a = 255
                            },
                            tex_coord = new SDL_FPoint()
                            {
                                x = 0,
                                y = 0
                            }
                        })
                        .AddVertex(new SDL_Vertex()
                        {
                            position = new SDL_FPoint()
                            {
                                x = xpos + p3offset.Item1,
                                y = ypos + p3offset.Item2
                            },
                            color = new SDL_Color()
                            {
                                r = 255, 
                                g = 255,
                                b = 255,
                                a = 255
                            },
                            tex_coord = new SDL_FPoint()
                            {
                                x = 0,
                                y = 0
                            }
                        })
                        .Render();

                    if(Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect))
                    {
                        if(GlobalMouse.Pressed(MB.Left))
                        {   
                            FUI.containerShown[label.text + component.Count] = !FUI.containerShown[label.text + component.Count];
                        }
                    } 

                    Draw.Text(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), Font.GetDefaultFont(), label.text, 18, UiStyles.TextColor);

                    yOffset += size.Y + 5;

                    component.RemoveAt(0);

                    if(FUI.containerShown[label.text + (component.Count + 1)]) 
                    {
                        Render(component, ref yOffset, indent + 1);
                    }
                } else 
                {
                    Render(component, ref yOffset, indent + 1);
                }
                //Debug.Log(FUI.containerShown.Count);

                // Debug.Log(FUI.containerShown.Count);
            }
            else if (componentObj.GetType() == typeof(HAlign<UiComponent>))
            {
                HAlign<UiComponent> component = (HAlign<UiComponent>)componentObj;
                RenderHorizontal(component, ref yOffset, indent);
            }
        }
        return yOffset;
    }

    public static float RenderHorizontal(HAlign<UiComponent> components, ref float yOffset, int indent = 0)
    {
        float xOffset = 0;
        float biggestHeight = 0;
        foreach (UiComponent componentObj in components)
        {
            if (componentObj.GetType() == typeof(UiButton))
            {
                UiButton component = (UiButton)componentObj;
                ButtonExt(new Vector2(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset), component.text, component.callback, out Vector2 size);
                xOffset += size.X + 5;
                if (size.Y > biggestHeight)
                    biggestHeight = size.Y;
            }
            else if (componentObj.GetType() == typeof(UiTitle))
            {
                UiTitle component = (UiTitle)componentObj;
                Draw.Text(new(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset), Font.GetDefaultFont(), component.text, 18, UiStyles.TextColor);
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.text, 18, new(0, 0, 0, 255));
                xOffset += size.X + 5;
                if (size.Y > biggestHeight)
                    biggestHeight = size.Y;
            }
            else if (componentObj.GetType() == typeof(UiText))
            {
                UiText component = (UiText)componentObj;
                Draw.Text(new(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset), Font.GetDefaultFont(), component.text, 14, component.overrideColor.HasValue ? component.overrideColor.Value : UiStyles.TextColor);
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.text, 14, new(0, 0, 0, 255));
                xOffset += size.X + 5;
                if (size.Y > biggestHeight)
                    biggestHeight = size.Y;
            }
            else if (componentObj.GetType() == typeof(UiCheckbox))
            {
                UiCheckbox component = (UiCheckbox)componentObj;

                SDL_Rect rect = new SDL_Rect()
                {
                    x = (int)(indent * 10 + UiRenderOffset.X + xOffset),
                    y = (int)(UiRenderOffset.Y + yOffset),
                    w = 20,
                    h = 20
                };

                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect))
                {
                    Draw.Text(Vector2.Add(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, new(20, 0)), Font.DefaultFont, component.text, 16, new(255, 255, 255, 255));

                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.ContainerHoverColor.ToCol());
                    if (GlobalMouse.Down(MB.Left))
                    {
                        SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.ContainerPressedColor.ToCol());
                    }
                    if (component.value)
                    {
                        SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.ContainerHoverPressedColor.ToCol());
                    }
                }
                else if (component.value)
                {
                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.ContainerPressedColor.ToCol());
                }
                else
                {
                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiStyles.ContainerIdleColor.ToCol());
                }

                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : GlobalMouse.Position, rect) && GlobalMouse.Pressed(MB.Left))
                {
                    component.callback();
                }

                SDL_RenderFillRect(Game.SDLRenderer, ref rect);

                Vector2 drawSize = Font.DrawSize(Font.DefaultFont, component.text, 16, new(255, 255, 255, 255));

                xOffset += 25;
                if (drawSize.Y > biggestHeight)
                    biggestHeight = drawSize.Y;
            }
            else if (componentObj.GetType() == typeof(UiTextField))
            {
                UiTextField component = (UiTextField)componentObj;

                FUI.TextFieldExt(new(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset), component.id, component.value, component.onChange, component.onSubmit, component.placeholder, out Vector2 size);

                xOffset += size.X + 5;
                if (size.Y > biggestHeight)
                    biggestHeight = size.Y;
            }
            else if (componentObj.GetType() == typeof(UiSlider))
            {
                UiSlider component = (UiSlider)componentObj;

                FUI.SliderExt(new(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset), component.min, component.max, component.value, component.onChange, out Vector2 size);

                xOffset += size.X + 5;
                if (size.Y > biggestHeight)
                    biggestHeight = size.Y;
            }
            else if (componentObj.GetType() == typeof(UiImage))
            {
                UiImage component = (UiImage)componentObj;
                Texture tex = (Texture)component.texture.Clone();
                
                Vector2 mult = new(biggestHeight / tex.textureSize.Y, biggestHeight / tex.textureSize.Y);
                tex.SizeMultiplier(mult).Center(Center.TopLeft).Position(new(indent * 10 + UiRenderOffset.X + xOffset, UiRenderOffset.Y + yOffset)).Alpha(255).Angle(0).Depth(-5000).GetRect(out Vector4 rect).Render();

                xOffset += rect.Z + 5;
                if (rect.W > biggestHeight)
                    biggestHeight = rect.W;
            }
            else
            {
                throw new Exception("Invalid UiComponent found in HAlign");
            }
        }
        yOffset += biggestHeight + 5;
        return biggestHeight + 5;
    }

    public static void Render(List<object> components, out float height)
    {
        float yOffset = 0;
        Render(components, ref yOffset);
        height = yOffset;
    }
}