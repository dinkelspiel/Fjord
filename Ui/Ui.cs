using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Fjord.Input;
using static Fjord.Extensions.Extensions;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_gfx;
using Fjord.Graphics;

namespace Fjord.Ui;

public static class FUI
{
    private static Vector2 UiRenderOffset = new();
    private static Vector2? OverMousePosition = null;
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
        Vector2 TextSize = Font.DrawSize(Font.GetDefaultFont(), text, 16, new SDL_Color() { r = 255, g = 0, b = 0, a = 255 });

        SDL_FRect rect = new()
        {
            x = (int)position.X,
            y = (int)position.Y,
            w = TextSize.X + 40,
            h = TextSize.Y + 7
        };

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : Mouse.Position, Helpers.FRectToRect(rect)))
        {
            SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerHoverColor);
            if (Mouse.Down)
            {
                SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerPressedColor);
            }
        }
        else
        {
            SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerIdleColor);
        }

        SDL_RenderFillRectF(Game.SDLRenderer, ref rect);

        Font.Draw(position + new Vector2(5, 3), Font.GetDefaultFont(), text, 16, new SDL_Color() { r = 255, g = 255, b = 255, a = 255 });

        if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : Mouse.Position, Helpers.FRectToRect(rect)))
        {
            if (Mouse.Pressed)
            {
                callback();
            }
        }

        size = new Vector2(rect.w, rect.h);
    }

    public static void Button(Vector2 position, string text, Action callback)
    {
        ButtonExt(position, text, callback, out Vector2 size);
    }
    
    public static void ResizeableRectangle(ref SDL_FRect rect, Vector2? aspectRatio=null)
    {
        SDL_Rect localRect = new SDL_Rect()
        {
            x = (int)(rect.x * Game.Window.Width),
            y = (int)(rect.y * Game.Window.Height),
            w = (int)((rect.w - rect.x) * Game.Window.Width),
            h = (int)((rect.h - rect.y) * Game.Window.Height)
        };
        
        short radius = 10;
        SDL.SDL_SetRenderDrawColor(Game.SDLRenderer, 255, 0, 0, 255);
        SDL_RenderDrawRect(Game.SDLRenderer, ref localRect);
        SDL.SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
        
        if (Helpers.PointDistance(new Vector2(localRect.x, localRect.y), Mouse.Position) < radius)
        {
            aacircleRGBA(Game.SDLRenderer, (short)localRect.x, (short)localRect.y, radius, 255, 255, 255, 255);
            if (Mouse.Down)
            {
                rect.x = Mouse.Position.X / Game.Window.Width;
                rect.y = Mouse.Position.Y / Game.Window.Height;
            }
        }
        
        if (Helpers.PointDistance(new Vector2(localRect.x + localRect.w, localRect.y), Mouse.Position) < radius)
        {
            aacircleRGBA(Game.SDLRenderer, (short)(localRect.x + localRect.w), (short)localRect.y, radius, 255, 255, 255, 255);
            if (Mouse.Down)
            {
                rect.w = Mouse.Position.X / Game.Window.Width;
                rect.y = Mouse.Position.Y / Game.Window.Height;
            }
        }

        if (Helpers.PointDistance(new Vector2(localRect.x, localRect.y + localRect.h), Mouse.Position) < radius)
        {
            aacircleRGBA(Game.SDLRenderer, (short)localRect.x, (short)(localRect.y + localRect.h), radius, 255, 255, 255, 255);
            if (Mouse.Down)
            {
                rect.x = Mouse.Position.X / Game.Window.Width;
                rect.h = Mouse.Position.Y / Game.Window.Height;
            }
        }

        if (Helpers.PointDistance(new Vector2(localRect.x + localRect.w, localRect.y + localRect.h), Mouse.Position) < radius)
        {
            aacircleRGBA(Game.SDLRenderer, (short)(localRect.x + localRect.w), (short)(localRect.y + localRect.h), radius, 255, 255, 255, 255);
            if (Mouse.Down)
            {
                rect.w = Mouse.Position.X / Game.Window.Width;
                rect.h = Mouse.Position.Y / Game.Window.Height;
            }
        }
    }

    public static float Render(List<object> components, ref float yOffset, int indent=0)
    {
        foreach(object componentObj in components)
        {
            if (componentObj.GetType() == typeof(UiButton))
            {
                UiButton component = (UiButton)componentObj;
                ButtonExt(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), component.text, component.callback, out Vector2 size);
                yOffset += size.Y + 5;
            } 
            else if(componentObj.GetType() == typeof(UiTitle))
            {
                UiTitle component = (UiTitle)componentObj;
                Font.Draw(new(indent * 10 + UiRenderOffset.X, yOffset + UiRenderOffset.Y), Font.GetDefaultFont(), component.text, 18, new() { r = 255, g = 255, b = 255, a = 255 });
                Vector2 size = Font.DrawSize(Font.GetDefaultFont(), component.text, 18, new() { r = 0, g = 0, b = 0, a = 255 });
                yOffset += size.Y + 5;
            }
            else if(componentObj.GetType() == typeof(UiSpacer))
            {
                SDL.SDL_SetRenderDrawColor(Game.SDLRenderer, 50, 50, 50, 255);

                SDL_Rect spacerRect = new()
                {
                    x = (int)(indent * 10 + UiRenderOffset.X), 
                    y = (int)(yOffset + UiRenderOffset.Y),
                    w = 200,
                    h = 1
                };

                SDL_RenderFillRect(Game.SDLRenderer, ref spacerRect);

                yOffset += 5;
            }
            else if (componentObj.GetType() == typeof(UiCheckbox))
            {
                UiCheckbox component = (UiCheckbox)componentObj;

                SDL_Rect rect = new SDL_Rect()
                {
                    x = (int)(indent * 10 + UiRenderOffset.X),
                    y = (int)(yOffset + UiRenderOffset.Y),
                    w = 20,
                    h = 20
                };
                
                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : Mouse.Position, rect))
                {
                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerHoverColor);
                    if (Mouse.Down)
                    {
                        SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerPressedColor);
                    }
                } 
                else if (component.value)
                {
                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerPressedColor);
                }
                else
                {
                    SDL_SetRenderDrawColor(Game.SDLRenderer, UiColors.ContainerIdleColor);
                }

                if (Helpers.PointInside(OverMousePosition.HasValue ? OverMousePosition.Value : Mouse.Position, rect) && Mouse.Pressed)
                {
                    component.callback();
                }

                SDL_RenderFillRect(Game.SDLRenderer, ref rect);

                Font.Draw(new Vector2(indent * 10 + UiRenderOffset.X + 25, yOffset + UiRenderOffset.Y), Font.DefaultFont, component.text, 16, new SDL_Color() {r = 255, g = 255, b = 255, a = 255});
                
                yOffset += 25;
            }
            else if (componentObj.GetType() == typeof(List<object>))
            {
                Render((List<object>)componentObj, ref yOffset, indent + 1);
            }
        }
        return yOffset;
    }

    public static void Render(List<object> components)
    {
        float yOffset = 0;
        Render(components, ref yOffset);
    }
}