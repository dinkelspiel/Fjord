using Fjord.Modules.Graphics;

using System.Linq;
using System.Collections.Generic;
using System;

using Fjord;

using static SDL2.SDL;

namespace Fjord.Modules.Window;

public class Scene {
    public virtual void OnAwake() {}
    public virtual void OnSleep() {}

    public virtual void OnUpdate() {}
    public void UpdateCall() {
        OnUpdate();
    }

    public virtual void OnRender() {}
    public void RenderCall() {
        OnRender();

        List<dynamic> DrawBuffer = new List<dynamic>(Draw.GetDrawBuffer());
        List<dynamic> SortedDrawBuffer = Draw.GetDrawBuffer().OrderBy(e => e.depth).ToList();

        Dictionary<int, IntPtr> DepthTextureArray = new Dictionary<int, IntPtr>();

        int? DrawInstructionLastDepth = null;

        foreach(var DrawInstruction in SortedDrawBuffer) {
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
}