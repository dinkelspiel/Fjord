using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Fjord.Input;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_gfx;

namespace ShooterThingy;

public static class Ui
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
            SDL_SetRenderDrawColor(Game.SDLRenderer, 52, 97, 152, 255);
            if (Mouse.Down)
            {
                SDL_SetRenderDrawColor(Game.SDLRenderer, 65, 121, 190, 255);
            }
        }
        else
        {
            SDL_SetRenderDrawColor(Game.SDLRenderer, 39, 73, 114, 255);
        }

        SDL_RenderFillRectF(Game.SDLRenderer, ref rect);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);

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
        SDL_SetRenderDrawColor(Game.SDLRenderer, 255, 0, 0, 255);
        SDL_RenderDrawRect(Game.SDLRenderer, ref localRect);
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
        
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
                SDL_SetRenderDrawColor(Game.SDLRenderer, 50, 50, 50, 255);

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

public abstract class UiComponent { }

public class UiButton : UiComponent
{
    public string text;
    public Action callback;

    public UiButton(string text, Action callback)
    {
        this.text = text;
        this.callback = callback;
    }
}

public class UiTitle : UiComponent
{
    public string text;

    public UiTitle(string text)
    {
        this.text = text;
    }
}

public class UiSpacer : UiComponent
{

}


public class UiBuilder
{
    List<object> UiComponents = new();
    Vector2 Position = new();
    Vector2? Size = null;

    public UiBuilder(Vector2 position=new(), Vector2? MouseOverride=null)
    {
        this.Position = position;
        if(MouseOverride != null)
        {
            Ui.OverrideMousePosition(MouseOverride.Value);
        }
    }
    public UiBuilder(Vector4 rect, Vector2? MouseOverride = null)
    {
        this.Position = new(rect.X, rect.Y);
        this.Size = new(rect.Z, rect.W);

        if (MouseOverride != null)
        {
            Ui.OverrideMousePosition(MouseOverride.Value);
        }
    }

    public UiBuilder Button(string text, Action callback)
    {
        UiComponents.Add(new UiButton(text, callback));
        return this;
    }

    public UiBuilder Button(string text)
    {
        UiComponents.Add(new UiButton(text, () => Console.WriteLine($"{text} Pressed")));
        return this;
    }

    public UiBuilder Title(string text)
    {
        UiComponents.Add(new UiTitle(text));
        return this;
    }

    public UiBuilder Container(List<object> components)
    {
        UiComponents.Add(components);
        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, UiComponent> callback)
    {
        foreach(T obj in objects)
        {
            UiComponents.Add(callback(obj));
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, List<object>> callback)
    {
        foreach (T obj in objects)
        {
            UiComponents.Add(callback(obj));
        }

        return this;
    }
    
    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, UiComponent> callback)
    {
        int idx = -1;
        foreach(T obj in objects)
        {
            idx++;
            UiComponents.Add(callback(obj, idx));
        }

        return this;
    }

    public UiBuilder ForEach<T>(List<T> objects, Func<T, int, List<object>> callback)
    {
        int idx = -1;
        foreach (T obj in objects)
        {
            idx++;
            UiComponents.Add(callback(obj, idx));
        }

        return this;
    }

    public UiBuilder Spacer()
    {
        UiComponents.Add(new UiSpacer());
        return this;
    }

    public List<object> Build()
    {
        return UiComponents;
    }

    public void Render()
    {
        Ui.SetRenderOffset(Position + new Vector2(10, 5));

        //float yOffset = 0;
        //float y = Ui.Render(Build(), ref yOffset);

        SDL_Rect rect = new()
        {
            x = (int)(Position.X),
            y = (int)(Position.Y),
            w = Size.HasValue ? (int)Size.Value.X : 200,
            h = Size.HasValue ? (int)Size.Value.Y : 400
        };
        SDL_SetRenderDrawColor(Game.SDLRenderer, 21, 22, 23, 255);
        SDL_RenderFillRect(Game.SDLRenderer, ref rect);

        Ui.Render(Build());
        Ui.ResetMousePosition();
        Ui.ResetRenderOffset();
    }
}