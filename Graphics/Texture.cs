using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SDL2.SDL;
using static SDL2.SDL_image;

namespace Fjord.Graphics;
public class Texture
{
    public IntPtr SDLTexture;
    public Vector2 textureSize;
    public Texture(string path)
    {
        if(File.Exists(path)) {
            SDLTexture = IMG_LoadTexture(Game.SDLRenderer, path);
        } else {
            throw new FileNotFoundException($"No image exists to load at path '{path}'");
        }
        SDL_QueryTexture(SDLTexture, out uint format, out int access, out int w, out int h);
        textureSize = new Vector2(w, h);
    }
}