using System.Drawing;
using System.Numerics;
using Fjord.Input;
using Fjord.Scenes;
using static SDL2.SDL;
using static SDL2.SDL_gfx;
using static SDL2.SDL_ttf;

namespace Fjord.Graphics;

public enum Flip
{
    None,
    Horizontal,
    Vertical,
    Both
}

public enum Center
{
    TopLeft,
    TopMiddle,
    TopRight,
    MiddleLeft,
    Middle,
    MiddleRight,
    BottomLeft,
    BottomMiddle,
    BottomRight
}

public static class Draw
{
    internal static string? CurrentSceneID = null;

    internal static List<IDrawInstruction> drawBuffer = new();
    
    internal static Dictionary<string, IntPtr> textureCache = new();

    public static void Box(Vector4 rect, Vector4 color, int depth=0) {
        new Rectangle(rect)
        {
            color = color,
            depth = depth,
            fill = true
        }.Render();
    }
    
    public static void Rectangle(Vector4 rect, Vector4 color, int depth=0) {
        new Rectangle(rect) {
            color = color,
            depth = depth,
            fill = false
        }.Render();
    }

    public static void RoundedBox(Vector4 rect, Vector4 color, float radius, int depth=0) {
        new Rectangle(rect)
        {
            color = color,
            depth = depth,
            fill = true,
            borderRadius = radius
        }.Render();
    }
    
    public static void RoundedRectangle(Vector4 rect, Vector4 color, float radius, int depth=0) {
        new Rectangle(rect) {
            color = color,
            depth = depth,
            fill = false,
            borderRadius = radius
        }.Render();
    }

    public static void FillCircle(Vector2 position, float radius, Vector4 color, int depth=0) {
        new Circle(position, radius) {
            color = color,
            depth = depth,
            fill = true
        }.Render();
    }

    public static void Circle(Vector2 position, float radius, Vector4 color, int depth=0) {
        new Circle(position, radius) {
            color = color,
            depth = depth,
            fill = false
        }.Render();
    }

    public static void TextureExt(Vector2 position, IntPtr texture, float angle, int depth=0)
    {
        new Texture(texture)
        {
            position = position,
            depth = depth,
            angle = angle
        }.Render();
    }

    public static void Texture(Vector2 position, IntPtr texture, int depth=0)
    {
        new Texture(texture)
        {
            position = position,
            depth = depth,
            angle = 0
        }.Render();
    }

    public static void Geometry(List<SDL_Vertex> verts, int depth=0)
    {
        new Geometry()
        {
            verticies = verts,
            depth = depth
        }.Render();
    }

    public static void Text(Vector2 position, string font, string text, int size, Vector4 color, int depth = 0)
    {
        new Text(font, text)
        {
            position = position,
            size = size,
            color = color,
            depth = depth
        }.Render();
    }

    public static void Line(Vector2 point1, Vector2 point2, Vector4 color, int depth = 0)
    {
        new Line(point1, point2)
        {
            color = color,
            depth = depth
        }.Render();
    }

    internal static void RectangleDirect(Rectangle rect) {
        SDL_Color col = new SDL_Color() {
            r = (byte)rect.color.X,
            g = (byte)rect.color.Y,
            b = (byte)rect.color.Z,
            a = (byte)rect.color.W
        };
        SDL_Rect SDLRect = new () {
            x = (int)rect.rect.X,
            y = (int)rect.rect.Y,
            w = (int)rect.rect.Z,
            h = (int)rect.rect.W
        };

        Helpers.SDL_SetRenderDrawColor(Game.SDLRenderer, col);
        if(rect.borderRadius != null) 
        {
            if(rect.fill) {
                if(rect.borderRadius > Math.Min(rect.rect.Z, rect.rect.W) / 2) {
                    rect.borderRadius = Math.Min(rect.rect.Z, rect.rect.W) / 2;
                }

                SDL_Rect rect1 = new () {
                    x = (int)rect.rect.X,
                    y = (int)(rect.rect.Y + rect.borderRadius),
                    w = (int)rect.rect.Z,
                    h = (int)(rect.rect.W - rect.borderRadius * 2)
                };
                SDL_RenderFillRect(Game.SDLRenderer, ref rect1);

                SDL_Rect rect2 = new () {
                    x = (int)(rect.rect.X + rect.borderRadius),
                    y = (int)rect.rect.Y,
                    w = (int)(rect.rect.Z - rect.borderRadius * 2),
                    h = (int)rect.rect.W
                };
                SDL_RenderFillRect(Game.SDLRenderer, ref rect2);

                //Top Left
                CircleDirect(
                    new Circle(new(rect.rect.X + (float)rect.borderRadius, rect.rect.Y + (float)rect.borderRadius), (float)rect.borderRadius)
                        .Fill(true)
                        .Color(rect.color)
                );

                //Top Right
                CircleDirect(
                    new Circle(new(rect.rect.X + rect.rect.Z - (float)rect.borderRadius - 1, rect.rect.Y + (float)rect.borderRadius), (float)rect.borderRadius)
                        .Fill(true)
                        .Color(rect.color)
                );

                //Bottom Left
                CircleDirect(
                    new Circle(new(rect.rect.X + (float)rect.borderRadius + 1, rect.rect.Y + rect.rect.W - (float)rect.borderRadius), (float)rect.borderRadius)
                        .Fill(true)
                        .Color(rect.color)
                );

                // Bottom Right
                CircleDirect(
                    new Circle(new(rect.rect.X + rect.rect.Z - (float)rect.borderRadius - 1, rect.rect.Y + rect.rect.W - (float)rect.borderRadius - 1), (float)rect.borderRadius)
                        .Fill(true)
                        .Color(rect.color)
                );
            } else
                roundedRectangleRGBA(Game.SDLRenderer, (short)rect.rect.X, (short)rect.rect.Y, (short)(rect.rect.X + rect.rect.Z), (short)(rect.rect.Y + rect.rect.W), (short)rect.borderRadius, col.r, col.g, col.b, col.a);
        } else {
            if(rect.fill)
                SDL_RenderFillRect(Game.SDLRenderer, ref SDLRect);
            else 
                SDL_RenderDrawRect(Game.SDLRenderer, ref SDLRect);
        }
    }

    internal static void CircleDirect(Circle circle) {
        if(circle.fill) {
            Helpers.SDL_SetRenderDrawColor(Game.SDLRenderer, Helpers.V4ToColor(circle.color));
            int x = (int)circle.radius;
            int y = 0;
            int err = 0;

            int x0 = (int)circle.position.X;
            int y0 = (int)circle.position.Y;
        
            while (x >= y)
            {
                SDL_RenderDrawLine(Game.SDLRenderer, x0 + x, y0 + y, x0 - x, y0 + y);
                SDL_RenderDrawLine(Game.SDLRenderer, x0 - y, y0 + x, x0 - y, y0 - x);

                SDL_RenderDrawLine(Game.SDLRenderer, x0 + x, y0 - y, x0 - x, y0 - y);
                SDL_RenderDrawLine(Game.SDLRenderer, x0 + y, y0 - x, x0 + y, y0 + x);

                if (err <= 0)
                {
                    y += 1;
                    err += 2*y + 1;
                }
                
                if (err > 0)
                {
                    x -= 1;
                    err -= 2*x + 1;
                }
            }
        } else
            if(aacircleRGBA(Game.SDLRenderer, (short)circle.position.X, (short)circle.position.Y, (short)circle.radius, (byte)circle.color.X, (byte)circle.color.Y, (byte)circle.color.Z, (byte)circle.color.W) == -1) {
                Debug.Log(LogLevel.Error, "Failed to draw aacircleRGBA");
            }
        SDL_SetRenderDrawColor(Game.SDLRenderer, 0, 0, 0, 255);
    }

    internal static void TextureDirect(Texture texture)
    {
        SDL_Rect rect = new()
        {
            x = (int)texture.position.X,
            y = (int)texture.position.Y,
            w = (int)(texture.textureSize.X * texture.sizeMultiplier.X),
            h = (int)(texture.textureSize.Y * texture.sizeMultiplier.Y)
        };

        SDL_RendererFlip tmpFlip = texture.flip == Flip.Horizontal ? SDL_RendererFlip.SDL_FLIP_HORIZONTAL : texture.flip == Flip.Vertical ? SDL_RendererFlip.SDL_FLIP_VERTICAL : texture.flip == Flip.Both ? SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL_RendererFlip.SDL_FLIP_VERTICAL : SDL_RendererFlip.SDL_FLIP_NONE;

        Vector2 center = new();
        Vector2 textureSize = texture.textureSize;

        if(!texture.isCustomCenter)
        {
            switch(texture.center)
            {
                case Graphics.Center.TopLeft: {
                    center = new(0, 0);
                } break;
                case Graphics.Center.TopMiddle: {
                    center = new(rect.w / 2, 0);
                } break;
                case Graphics.Center.TopRight: {
                    center = new(rect.h, 0);
                } break;

                case Graphics.Center.MiddleLeft: {
                    center = new(0, rect.h / 2);
                } break;
                case Graphics.Center.Middle: {
                    center = new(rect.w / 2, rect.h / 2);
                } break;
                case Graphics.Center.MiddleRight: {
                    center = new(rect.w, rect.h / 2);
                } break;
                
                case Graphics.Center.BottomLeft: {
                    center = new(0, rect.h);
                } break;
                case Graphics.Center.BottomMiddle: {
                    center = new(rect.w / 2, rect.h);
                } break;
                case Graphics.Center.BottomRight: {
                    center = new(rect.w, rect.h);
                } break;
            }
        } else
        {
            center = new(texture.customCenter.X, texture.customCenter.Y);
        }

        SDL_Point sdlcenter = new()
        {
            x = (int)center.X,
            y = (int)center.Y
        };
        rect.x -= sdlcenter.x;
        rect.y -= sdlcenter.y;

        SDL_SetTextureAlphaMod(texture.SDLTexture, (byte)texture.alpha);

        if(texture.srcTextureOffset is not null)
        {
            SDL_Rect srcRect = new()
            {
                x = (int)texture.srcTextureOffset.Value.X,
                y = (int)texture.srcTextureOffset.Value.Y,
                w = (int)texture.srcTextureOffset.Value.Z,
                h = (int)texture.srcTextureOffset.Value.W
            };
            SDL_RenderCopyEx(Game.SDLRenderer, texture.SDLTexture, ref srcRect, ref rect, texture.angle, ref sdlcenter, tmpFlip);
        } else {
            SDL_RenderCopyEx(Game.SDLRenderer, texture.SDLTexture, IntPtr.Zero, ref rect, texture.angle, ref sdlcenter, tmpFlip);
        }

        SDL_SetTextureAlphaMod(texture.SDLTexture, 255);

        if(texture.destroy)
        {
            SDL_DestroyTexture(texture.SDLTexture);
            SDL_FreeSurface(texture.SDLSurface);
            texture = null;
        }
    }

    internal static void GeometryDirect(Geometry geometry)
    {
        SDL_RenderGeometry(Game.SDLRenderer, IntPtr.Zero, geometry.verticies.ToArray(), geometry.verticies.Count, IntPtr.Zero, 0);
    }

    internal static void TextDirect(Text text)
    {
        if(!Font.Fonts.ContainsKey(text.font + text.size.ToString()))
        {
            Font.Fonts.Add(text.font + text.size.ToString(), TTF_OpenFont(text.font, text.size));
        }

        SDL_Color col = Helpers.V4ToColor(text.color);
        string CacheKey = text.font + text.value + text.size + col.r + col.g + col.b + col.a;
        if (!Font.FontCache.ContainsKey(CacheKey))
        {
            IntPtr surface = TTF_RenderText_Blended(Font.Fonts[text.font + text.size.ToString()], text.value, col);
            IntPtr texture = SDL_CreateTextureFromSurface(Game.SDLRenderer, surface);
            Font.FontCache.Add(CacheKey, texture);
            SDL_FreeSurface(surface);
        }

        SDL_QueryTexture(Font.FontCache[CacheKey], out uint format, out int access, out int textureW, out int textureH);
        
        SDL_Rect rect = new()
        {
            x = (int)text.position.X,
            y = (int)text.position.Y,
            w = (int)(textureW),
            h = (int)(textureH)
            
        };
        
        SDL_RenderCopy(Game.SDLRenderer, Font.FontCache[CacheKey], IntPtr.Zero, ref rect);
    }

    internal static void LineDirect(Line line)
    {
        SDL_SetRenderDrawColor(Game.SDLRenderer, (byte)line.color.X, (byte)line.color.Y, (byte)line.color.Z, (byte)line.color.W);
        SDL_RenderDrawLine(Game.SDLRenderer, (int)line.point1.X, (int)line.point1.Y, (int)line.point2.X, (int)line.point2.Y);
    }

    internal static void DrawDrawBuffer(List<IDrawInstruction> drawBufferLocal, string? sceneId) {
        
        drawBufferLocal.Sort((a, b) => a.depth.CompareTo(b.depth));
        foreach(IDrawInstruction drawInsObj in drawBufferLocal) {
            if(drawInsObj is Rectangle rect) {
                if(sceneId != null)
                {
                    rect.rect.X -= SceneHandler.Get(sceneId).Camera.Offset.X;
                    rect.rect.Y -= SceneHandler.Get(sceneId).Camera.Offset.Y;
                }
                Draw.RectangleDirect(rect);
            } else if(drawInsObj is Circle circle) {
                if(circle.hoverAnimation != null)
                {
                    if(circle.hoverAnimation.xDriver != null)
                        circle.position.X *= (circle.hoverAnimation.xDriver(circle.hoverAnimation.progress) + 1);
                    if(circle.hoverAnimation.yDriver != null)
                        circle.position.Y *= (circle.hoverAnimation.yDriver(circle.hoverAnimation.progress) + 1);
                    if(circle.hoverAnimation.radiusDriver != null)
                        circle.radius *= (circle.hoverAnimation.radiusDriver(circle.hoverAnimation.progress) + 1);
                    if(circle.hoverAnimation.colorDriver != null)
                        circle.color = Helpers.Lerp(circle.color, circle.hoverAnimation.colorGoal, circle.hoverAnimation.progress);
                        // drawInsClone.color *= (drawIns.hoverAnimation.colorDriver(drawIns.hoverAnimation.progress) + 1);
                    if(sceneId == null ? Helpers.PointDistance(GlobalMouse.Position, circle.position) < circle.radius : Helpers.PointDistance(SceneHandler.Scenes[sceneId].Mouse.Position, circle.position) < circle.radius) {
                        if(circle.hoverAnimation.progress < 1)
                            circle.hoverAnimation.progress += circle.hoverAnimation.speed * (float)Game.DeltaTime;
                    } else if(circle.hoverAnimation.progress > 0) {
                        circle.hoverAnimation.progress -= circle.hoverAnimation.speed * (float)Game.DeltaTime;
                        if(circle.hoverAnimation.progress < 0)
                            circle.hoverAnimation.progress = 0;
                    }

                }
                if(sceneId != null)
                    circle.position -= SceneHandler.Get(sceneId).Camera.Offset;
                Draw.CircleDirect(circle);
            } else if(drawInsObj is Texture texture)
            {
                if(sceneId != null)
                    texture.position -= SceneHandler.Get(sceneId).Camera.Offset;
                Draw.TextureDirect(texture);
            } else if(drawInsObj is Geometry geo)
            {
                if(sceneId != null)
                {
                    for(var i = 0; i < geo.verticies.Count; i++)
                    {
                        geo.verticies[i] = new SDL_Vertex() {
                            position = new SDL_FPoint() {
                                x = geo.verticies[i].position.x - SceneHandler.Get(sceneId).Camera.Offset.X,
                                y = geo.verticies[i].position.y - SceneHandler.Get(sceneId).Camera.Offset.Y,
                            },
                            color = geo.verticies[i].color,
                            tex_coord = geo.verticies[i].tex_coord
                        }; 
                    }
                }

                Draw.GeometryDirect(geo);
            } else if(drawInsObj is Text text)
            {
                if(sceneId != null)
                    text.position -= SceneHandler.Get(sceneId).Camera.Offset;
                Draw.TextDirect(text);
            } else if(drawInsObj is Line line)
            {
                if(sceneId != null)
                {   
                    line.point1 -= SceneHandler.Get(sceneId).Camera.Offset;
                    line.point2 -= SceneHandler.Get(sceneId).Camera.Offset;
                }

                Draw.LineDirect(line);
            }
        }
    }
}