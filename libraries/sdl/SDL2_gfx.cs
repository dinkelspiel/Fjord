#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

namespace SDL2
{
    public static class SDL_gfx {
        private const string nativeLibName = "SDL2_gfx";

        internal static class Import
        {
            public const string lib = "SDL2_gfx.dll";
        }

        #region SDL2_gfxPrimitives.h 

		[DllImport(Import.lib, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int thickLineRGBA(
            IntPtr	renderer,
            int 	x1,
            int 	y1,
            int 	x2,
            int 	y2,
            uint 	width,
            uint 	r,
            uint 	g,
            uint 	b,
            uint 	a 
		);

        #endregion
    }
}