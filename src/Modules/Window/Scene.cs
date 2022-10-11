using Fjord.Modules.Graphics;

using System.Linq;
using System.Collections.Generic;
using System;

using Fjord;
using Fjord.Modules.Entity;

using static SDL2.SDL;
using System.Numerics;

namespace Fjord.Modules.Window;

public class Scene {
    internal List<Entity.Entity> EntityList = new ();
    internal Vector2 Resolution = new Vector2();
    internal Vector4 BackgroundColor = new Vector4();

    public void SetResolution(Vector2 resolution)
    {
        Resolution = new Vector2(resolution.X, resolution.Y);
    }
    public void SetResolution(int x, int y)
    {
        Resolution = new Vector2(x, y);
    }

    public Vector2 GetResolution()
    {
        return Resolution;
    }

    public void SetBackground(int r, int g, int b, int a)
    {
        BackgroundColor = new Vector4(r, g, b, a);
    }

    public void SetBackground(Vector4 backgroundcolor)
    {
        BackgroundColor = backgroundcolor;
    }

    public Vector4 GetBackground()
    {
        return BackgroundColor;
    }

    public void AddEntity(Entity.Entity entity) {
        EntityList.Add(entity);
    }

    public virtual void OnAwake() {}
    public virtual void OnSleep() {}

    public virtual void OnUpdate() {}
    internal void Update() {
        OnUpdate();

        List<Entity.Entity> _entityList = new(EntityList);
        foreach (Entity.Entity e in _entityList)
        {
            e.Update();
        }
    }

    public virtual void OnRender() {}
    internal void Render() {
        OnRender();

        List<Entity.Entity> sorted_entities = new List<Entity.Entity>(EntityList);
        // sorted_entities = entities.OrderBy(e => e.depth).ToList();
        sorted_entities = sorted_entities.OrderBy(e => e.Depth).ToList();

        foreach (Entity.Entity e in sorted_entities)
        {
            e.Render();
        }

        List<dynamic> DrawBuffer = new List<dynamic>(Draw.GetDrawBuffer());
        List<dynamic> SortedDrawBuffer = Draw.GetDrawBuffer().OrderBy(e => e.depth).ToList();

        Dictionary<int, IntPtr> DepthTextureArray = new Dictionary<int, IntPtr>();

        int? DrawInstructionLastDepth = null;

        foreach (var DrawInstruction in SortedDrawBuffer) {
            if(DrawInstructionLastDepth != DrawInstruction.depth) {
                DepthTextureArray.Add(DrawInstruction.depth, SDL_CreateTexture(Fjord.Game.Renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)Fjord.Game.Size.X, (int)Fjord.Game.Size.Y));
                SDL_SetRenderTarget(Game.Renderer, DepthTextureArray[DrawInstruction.depth]);
                SDL_SetTextureBlendMode(DepthTextureArray[DrawInstruction.depth], SDL_BlendMode.SDL_BLENDMODE_BLEND);
                SDL_RenderClear(Game.Renderer);
            }
            // SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);
            
            if(DrawInstruction.GetType() == typeof(Draw.DrawBufferTextureInstruction)) {
                // SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);
                var _drawInstruction = (Draw.DrawBufferTextureInstruction) DrawInstruction;
                Draw.TextureDirect(_drawInstruction.position, _drawInstruction.texture);
            } else if(DrawInstruction.GetType() == typeof(Draw.DrawBufferRectangleInstruction)) {
                var _drawInstruction = (Draw.DrawBufferRectangleInstruction) DrawInstruction;
                Draw.RectangleDirect(_drawInstruction.rectangle, _drawInstruction.color, _drawInstruction.fill, _drawInstruction.border_radius, _drawInstruction.angle, _drawInstruction.origin);
            } else if(DrawInstruction.GetType() == typeof(Draw.DrawBufferCircleInstruction)) {
                var _drawInstruction = (Draw.DrawBufferCircleInstruction) DrawInstruction;
                Draw.CircleDirect(_drawInstruction.position, _drawInstruction.radius, _drawInstruction.color, _drawInstruction.fill);
            } else if(DrawInstruction.GetType() == typeof(Draw.DrawBufferTextInstruction)) {
                var _drawInstruction = (Draw.DrawBufferTextInstruction) DrawInstruction;
                Draw.TextDirect(_drawInstruction.position, _drawInstruction.fontID, _drawInstruction.fontSize, _drawInstruction.value, _drawInstruction.color);
            }

            DrawInstructionLastDepth = DrawInstruction.depth;
        }

        SDL_SetRenderTarget(Game.Renderer, IntPtr.Zero);

        foreach(KeyValuePair<int, IntPtr> DepthTexture in DepthTextureArray.Reverse()) {
            SDL_Rect SrcRect = new SDL_Rect(0, 0, (int)Game.Size.X, (int)Game.Size.Y);
            SDL_RenderCopy(Game.Renderer, DepthTexture.Value, ref SrcRect, ref SrcRect);
            SDL_DestroyTexture(DepthTextureArray[DepthTexture.Key]);
        }

        Draw.CleanDrawBuffer();
    }

    public virtual void OnRenderGUI() { }
    internal void RenderGUI()
    {
        OnRenderGUI();

        List<dynamic> DrawBuffer = new List<dynamic>(Draw.GetDrawGUIBuffer());
        List<dynamic> SortedDrawBuffer = Draw.GetDrawGUIBuffer().OrderBy(e => e.depth).ToList();

        Dictionary<int, IntPtr> DepthTextureArray = new Dictionary<int, IntPtr>();

        int? DrawInstructionLastDepth = null;

        foreach (var DrawInstruction in SortedDrawBuffer)
        {
            if (DrawInstructionLastDepth != DrawInstruction.depth)
            {
                DepthTextureArray.Add(DrawInstruction.depth, SDL_CreateTexture(Fjord.Game.Renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, (int)Fjord.Game.Size.X, (int)Fjord.Game.Size.Y));
                SDL_SetRenderTarget(Game.Renderer, DepthTextureArray[DrawInstruction.depth]);
                SDL_SetTextureBlendMode(DepthTextureArray[DrawInstruction.depth], SDL_BlendMode.SDL_BLENDMODE_BLEND);
                SDL_RenderClear(Game.Renderer);
            }
            // SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);

            if (DrawInstruction.GetType() == typeof(Draw.DrawBufferTextureInstruction))
            {
                // SDL_SetRenderDrawColor(Game.Renderer, 0, 0, 0, 0);
                var _drawInstruction = (Draw.DrawBufferTextureInstruction)DrawInstruction;
                Draw.TextureDirect(_drawInstruction.position, _drawInstruction.texture, true);
            }
            else if (DrawInstruction.GetType() == typeof(Draw.DrawBufferRectangleInstruction))
            {
                var _drawInstruction = (Draw.DrawBufferRectangleInstruction)DrawInstruction;
                Draw.RectangleDirect(_drawInstruction.rectangle, _drawInstruction.color, _drawInstruction.fill, _drawInstruction.border_radius, _drawInstruction.angle, _drawInstruction.origin, true);
            }
            else if (DrawInstruction.GetType() == typeof(Draw.DrawBufferCircleInstruction))
            {
                var _drawInstruction = (Draw.DrawBufferCircleInstruction)DrawInstruction;
                Draw.CircleDirect(_drawInstruction.position, _drawInstruction.radius, _drawInstruction.color, _drawInstruction.fill, true);
            }
            else if (DrawInstruction.GetType() == typeof(Draw.DrawBufferTextInstruction))
            {
                var _drawInstruction = (Draw.DrawBufferTextInstruction)DrawInstruction;
                Draw.TextDirect(_drawInstruction.position, _drawInstruction.fontID, _drawInstruction.fontSize, _drawInstruction.value, _drawInstruction.color, true);
            }

            DrawInstructionLastDepth = DrawInstruction.depth;
        }

        SDL_SetRenderTarget(Game.Renderer, IntPtr.Zero);

        foreach (KeyValuePair<int, IntPtr> DepthTexture in DepthTextureArray.Reverse())
        {
            SDL_Rect SrcRect = new SDL_Rect(0, 0, (int)Game.Size.X, (int)Game.Size.Y);
            SDL_RenderCopy(Game.Renderer, DepthTexture.Value, ref SrcRect, ref SrcRect);
            SDL_DestroyTexture(DepthTextureArray[DepthTexture.Key]);
        }

        Draw.CleanDrawBuffer();
    }
}