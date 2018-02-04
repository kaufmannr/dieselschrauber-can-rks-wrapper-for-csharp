using System;
using System.Runtime.InteropServices;

namespace rks_usb_csharp
{
    internal class NativeLoadLibraryMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll",SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);
    }
}