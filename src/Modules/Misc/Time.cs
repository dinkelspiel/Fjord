using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fjord.Modules.Misc;
public static class Time
{
    public static double DeltaTime = 0;
    internal static double Now = 0;
    internal static double Last = 0;

    internal static double LastRender = 0;
}
