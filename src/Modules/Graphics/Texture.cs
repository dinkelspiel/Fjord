using System.Numerics;
using System;

using static SDL2.SDL;
using static SDL2.SDL_image;

using Fjord.Modules.Misc;
using HalloweenGame.Fjord.src.Modules.Misc;

namespace Fjord.Modules.Graphics;

public class Texture : ICloneable {
    private IntPtr _sdl2texture;
    private Vector2 _scale = new Vector2(1, 1);
    private int _alpha = 255;
    private int _angle = 0;
    private Vector2 _origin = new Vector2(0, 0);
    private Draw.Fliptype _fliptype = Draw.Fliptype.NONE;

    public IntPtr GetSDL2Texture() {
        return _sdl2texture;
    }

    public Texture(string Path) {
        SetTexture(Path);
        SetOrigin(Draw.DrawOrigin.CENTER);
    }

    public Texture SetTexture(string Path) {
        try {
            this._sdl2texture = IMG_LoadTexture(Game.Renderer, $"{Game.ExecutablePath}\\Assets\\Images\\{Path}".OSPath());
        } catch(Exception e) {
            Game.Stop(e);
        }
        return this;
    }

    public Vector2 GetSDL2TextureSize() {
        int w, h;
        uint f; int a;
        SDL_QueryTexture(_sdl2texture, out f, out a, out w, out h);
        return new Vector2((float)w, (float)h);
    }

    public Texture SetScale(float x) {
        _scale = new Vector2(x, x);
        return this;
    }

    public Texture SetScale(float x, float y) {
        _scale = new Vector2(x, y);
        return this;
    }

    public Texture SetScale(Vector2 scale) {
        _scale = scale;
        return this;
    }

    public Vector2 GetScale() {
        return _scale;
    }

    public Vector2 GetTextureSize() {
        return new Vector2(GetSDL2TextureSize().X * _scale.X, GetSDL2TextureSize().Y * _scale.Y);
    }

    public Texture SetAlpha(int alpha) {
        _alpha = alpha;
        return this;
    }

    public int GetAlpha() {
        return _alpha;
    }

    public Texture SetOrigin(Draw.DrawOrigin _setOrigin) {
        Vector2 _textureSize = GetTextureSize();

        switch(_setOrigin) {
        case Draw.DrawOrigin.TOP_LEFT:
            this._origin = new Vector2(0, 0);
            break;
        case Draw.DrawOrigin.TOP_MIDDLE:
            this._origin = new Vector2(_textureSize.X / 2, 0);
            break;
        case Draw.DrawOrigin.TOP_RIGHT:
            this._origin = new Vector2(_textureSize.X, 0);
            break;
        case Draw.DrawOrigin.BOTTOM_RIGHT:
            this._origin = new Vector2(_textureSize.X, _textureSize.Y);
            break;
        case Draw.DrawOrigin.BOTTOM_MIDDLE:
            this._origin = new Vector2(_textureSize.X / 2, _textureSize.Y);
            break;     
        case Draw.DrawOrigin.BOTTOM_LEFT:
            this._origin = new Vector2(0, _textureSize.Y);
            break;      
        case Draw.DrawOrigin.MIDDLE_LEFT:
            this._origin = new Vector2(0, _textureSize.Y / 2);
            break;        
        case Draw.DrawOrigin.MIDDLE_RIGHT:
            this._origin = new Vector2(_textureSize.X, _textureSize.Y / 2);
            break;       
        case Draw.DrawOrigin.CENTER:
            this._origin = new Vector2(_textureSize.X / 2, _textureSize.Y / 2);
            break;  
        }

        return this;
    } 

    public Texture SetOrigin(int x, int y)
    {
        this._origin = new Vector2(x, y);
        return this;
    }

    public Texture SetOrigin(Vector2 origin)
    {
        this._origin = origin;
        return this;
    }

    public Vector2 GetOrigin() {
        return _origin;
    }

    public Texture SetFliptype(Draw.Fliptype fliptype) {
        _fliptype = fliptype;
        return this;
    }

    public Draw.Fliptype GetFliptype() {
        return _fliptype;
    }

    public Texture SetAngle(int angle) {
        this._angle = angle;
        return this;
    }

    public int GetAngle() {
        return _angle;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

class Animation : ICloneable
{
    private Texture[] _textures;
    private int _frame = 0;

    public Animation(Texture[] textures)
    {
        _textures = textures;
    }

    public static Animation LoadAnimation(string animation)
    {
        Texture[] textures = new Texture[File.ReadAllLines($"{Game.ExecutablePath}\\Assets\\Images\\{animation}.anim".OSPath()).Length];
        int i = 0;
        foreach (string line in File.ReadAllLines($"{Game.ExecutablePath}\\Assets\\Images\\{animation}.anim".OSPath()))
        {
            textures[i] = new Texture(line);
            i++;
        }
        Animation anim = new(textures);
        return anim;
    }

    public void Next()
    {
        _frame++;
        if (_frame > _textures.Length - 1)
        {
            _frame = 0;
        }
    }

    public Texture Get()
    {
        return _textures[_frame];
    }
    
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}