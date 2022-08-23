using System.Numerics;
using System.Collections.Generic;
using System;

using static SDL2.SDL;
using static SDL2.SDL_gfx;

namespace Fjord.Modules.Graphics;

public static class Draw {
    private static List<dynamic> _drawBuffer = new List<dynamic>();

    public enum Fliptype {
        NONE = 0,
        HORIZONTAL = 1,
        VERTICAL = 2,
        BOTH = 3
    }

    public enum DrawOrigin {
        TOP_LEFT,
        TOP_MIDDLE,
        TOP_RIGHT,
        BOTTOM_RIGHT,
        BOTTOM_MIDDLE,
        BOTTOM_LEFT,
        MIDDLE_LEFT,
        MIDDLE_RIGHT,
        CENTER
    }

    internal static Vector2 GenerateCenter(DrawOrigin Origin, Vector2 Size) {
        SDL_Point point;
        switch(Origin) {
            case DrawOrigin.TOP_LEFT:
                point = new SDL_Point(0, 0);
                break;
            case DrawOrigin.TOP_MIDDLE:
                point = new SDL_Point((int)Size.X / 2, 0);
                break;
            case DrawOrigin.TOP_RIGHT:
                point = new SDL_Point((int)Size.X, 0);
                break;

            case DrawOrigin.MIDDLE_LEFT:
                point = new SDL_Point(0, (int)Size.Y / 2);
                break;
            case DrawOrigin.CENTER:
                point = new SDL_Point((int)Size.X / 2, (int)Size.Y / 2);
                break;
            case DrawOrigin.MIDDLE_RIGHT:
                point = new SDL_Point((int)Size.X, (int)Size.Y / 2);
                break;

            case DrawOrigin.BOTTOM_LEFT:
                point = new SDL_Point(0, (int)Size.Y);
                break;
            case DrawOrigin.BOTTOM_MIDDLE:
                point = new SDL_Point((int)Size.X / 2, (int)Size.Y);
                break;
            case DrawOrigin.BOTTOM_RIGHT:
                point = new SDL_Point((int)Size.X, (int)Size.Y);
                break;
            
            default:
                point = new SDL_Point((int)Size.X / 2, (int)Size.Y / 2);
                break; 
        }
        return new Vector2((float)point.x, (float)point.y);
    }

    internal struct DrawBufferRectangleInstruction {
        public int depth;
        public Vector4 rectangle;
        public Vector4 color;
        public bool fill;
        public int? border_radius;
        public double angle;
        public DrawOrigin origin;
    } 

    internal struct DrawBufferCircleInstruction {
        public Vector2 position;
        public int radius;
        public Vector4 color;
        public bool fill;
        public int depth;
    }

    internal struct DrawBufferTextureInstruction {
        public Texture texture;
        public int depth;
        public Vector2 position;
    }

    internal static List<dynamic> GetDrawBuffer() {
        return new List<dynamic>(_drawBuffer);
    }

    internal static void CleanDrawBuffer() {
        _drawBuffer = new List<dynamic>();
    }

    public static void Rectangle(Vector4 rectangle, Vector4 color, bool fill = true, int? border_radius = null, double angle = 0, DrawOrigin origin = DrawOrigin.CENTER, int depth = 0) => _drawBuffer.Add(new DrawBufferRectangleInstruction() {
        rectangle = rectangle,
        depth = depth,
        color = color,
        border_radius = border_radius,
        angle = angle,
        origin = origin,
        fill = fill
    });

    internal static void RectangleDirect(Vector4 rectangle, Vector4 color, bool fill, int? border_radius, double angle, DrawOrigin origin) {
        IntPtr _drawTexture = SDL_CreateTexture(Game.Renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)rectangle.Z + 1, (int)rectangle.W + 1);
        SDL_SetTextureBlendMode(_drawTexture, SDL_BlendMode.SDL_BLENDMODE_BLEND);
        SDL_SetRenderTarget(Game.Renderer, _drawTexture);

        // Debug.Debug.Send(fill);

        if(border_radius != null) { 
            if(!fill) {
                // Debug.Debug.Send("Rounded Rectangle RGBA");
                roundedRectangleRGBA(Game.Renderer, 0, 0, (short)rectangle.Z, (short)rectangle.W, (short)border_radius, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
            } else { 
                // Debug.Debug.Send("Rounded Box RGBA");
                roundedBoxRGBA(Game.Renderer, 0, 0, (short)rectangle.Z, (short)rectangle.W, (short)border_radius, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
            }
        } else {
            if(!fill) {
                // Debug.Debug.Send("Rectangle RGBA");
                rectangleRGBA(Game.Renderer, 0, 0, (short)rectangle.Z, (short)rectangle.W, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
            } else {
                // Debug.Debug.Send("Box RGBA");
                // SDL_SetRenderDrawColor(Game.Renderer, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
                boxRGBA(Game.Renderer, 0, 0, (short)rectangle.Z, (short)rectangle.W, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
                // SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);
            }
        }

        Vector2 _centerV2 = GenerateCenter(origin, new Vector2(rectangle.Z, rectangle.W));
        SDL_Point _center = new SDL_Point((int)_centerV2.X, (int)_centerV2.Y);

        SDL_Rect _destRect = new SDL_Rect((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Z, (int)rectangle.W);

        SDL_SetRenderTarget(Game.Renderer, IntPtr.Zero);
        SDL_RenderCopyEx(Game.Renderer, _drawTexture, IntPtr.Zero, ref _destRect, angle, IntPtr.Zero, SDL_RendererFlip.SDL_FLIP_NONE);
    }

    public static void Circle(Vector2 position, int radius, Vector4 color, bool fill = true, int depth = 0) => _drawBuffer.Add(new DrawBufferCircleInstruction() {
        position = position,
        radius = radius,
        color = color,
        fill = fill,
        depth = depth
    });

    internal static void CircleDirect(Vector2 position, int radius, Vector4 color, bool fill) {
        if(fill) {
            filledCircleRGBA(Game.Renderer, (short)position.X, (short)position.Y, (short)radius, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
        } else {
            circleRGBA(Game.Renderer, (short)position.X, (short)position.Y, (short)radius, (byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W);
        }
    }

    public static void Texture(Texture texture, Vector2 position, int depth = 0) => _drawBuffer.Add(new DrawBufferTextureInstruction() {
        texture = texture,
        position = position,
        depth = depth
    });

    internal static void TextureDirect(Vector2 position, Texture texture) {
        IntPtr _finalTexture = texture.GetSDL2Texture();

        SDL_Rect src, dest;

        src = new SDL_Rect(0, 0, (int)texture.GetSDL2TextureSize().X, (int)texture.GetSDL2TextureSize().Y);
        dest = new SDL_Rect((int)position.X, (int)position.Y, (int)texture.GetTextureSize().X, (int)texture.GetTextureSize().Y);

        // Origin Handling

        dest.x -= (int)texture.GetOrigin().X;
        dest.y -= (int)texture.GetOrigin().Y;

        // Scale Handling

        dest.w = (int)(dest.w * texture.GetScale().X);
        dest.h = (int)(dest.h * texture.GetScale().Y);

        // Flip Handling

        SDL_RendererFlip flip_sdl = SDL_RendererFlip.SDL_FLIP_NONE; 

        if(texture.GetFliptype() == Fliptype.BOTH) {
            flip_sdl = SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL_RendererFlip.SDL_FLIP_VERTICAL;
        } else if(texture.GetFliptype() == Fliptype.HORIZONTAL) {
            flip_sdl = SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
        } else if(texture.GetFliptype() == Fliptype.VERTICAL) {
            flip_sdl = SDL_RendererFlip.SDL_FLIP_VERTICAL;
        }

        // Alpha Handling

        SDL_SetTextureAlphaMod(_finalTexture, (byte)texture.GetAlpha());

        // Camera Relative Handling

        // if(texture.get_relative()) {
        //     dest.x -= (int)camera.get().x;
        //     dest.y -= (int)camera.get().y;
        // }

        // Draw

        SDL_Point center = new SDL_Point((int)texture.GetOrigin().X, (int)texture.GetOrigin().Y);
        SDL_RenderCopyEx(Game.Renderer, _finalTexture, ref src, ref dest, texture.GetAngle(), ref center, flip_sdl);      
    }
}