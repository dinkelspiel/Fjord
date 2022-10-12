using System.Runtime.CompilerServices;
using Fjord;
using Fjord.Modules.Misc;

namespace HalloweenGame.Fjord.src.Modules.Misc;

public static class Path
{
    public static string OSPath(this string path, [CallerFilePath] string caller = "")
    {
        String newPath = path;
        if (OS.GetPlatform() == OS.Platform.Windows)
            newPath = path.Replace("/", "\\");
        else
            newPath = path.Replace("\\", "/");
        return newPath;
    }
}