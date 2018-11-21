using System;
using System.Runtime.InteropServices;

namespace Konsole
{
    public static class PlatformCheck
    {
        public static bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
