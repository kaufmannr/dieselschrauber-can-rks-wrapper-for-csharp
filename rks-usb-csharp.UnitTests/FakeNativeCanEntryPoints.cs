using System.Runtime.InteropServices;

namespace rks_usb_csharp.UnitTests
{
    class FakeNativeCanEntryPoints
    {
        public const string _SetUpModel = "SetUpModel";
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int UniversalCallback(string name, string value);

      
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int SetUpModel(
            [MarshalAs(UnmanagedType.FunctionPtr)] UniversalCallback universalCallbackPtr);
    }
}