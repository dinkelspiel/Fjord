#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

namespace SDL2
{
    public static class SDL_gfx {
        private const string nativeLibName = "SDL2_gfx";

        #region SDL2_gfxPrimitives.h 

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern int thickLineColor(
            IntPtr	renderer,
            int 	x1,
            int 	y1,
            int 	x2,
            int 	y2,
            uint 	width,
            uint 	color 
		);

        #endregion
    }
}