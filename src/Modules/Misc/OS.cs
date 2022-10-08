using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Fjord.Modules.Misc
{
    public static class OS {
 
        public enum Platform
        {
            Windows,
            Linux,
            OSX,
            Unknown
        }

        public static Platform GetOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Platform.Windows;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Platform.Linux;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return Platform.OSX;

            return Platform.Unknown;
        }

    }
}
