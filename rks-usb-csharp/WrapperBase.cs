using System;
using System.IO;
using System.Runtime.InteropServices;

namespace rks_usb_csharp
{
    public class WrapperBase
    {
        private readonly string _libraryPath;
        private readonly string _dllName;
        private IntPtr _nativeLibraryPointer;

        public WrapperBase(string libraryPath, string dllName)
        {
            this._libraryPath = libraryPath;
            _dllName = dllName;
        }

        private string Library => Path.Combine(_libraryPath, _dllName);

        public static CanWrapper Create(string libraryPath)
        {
            return new CanWrapper(libraryPath);
        }

        public void Dispose()
        {
            NativeLoadLibraryMethods.FreeLibrary(_nativeLibraryPointer);
        }

        public void SetUp()
        {
            LoadLibrary();
            Setup();
        }

        private void Setup()
        {
        }

        private void LoadLibrary()
        {
            var dllToLoad = Library;
            _nativeLibraryPointer = NativeLoadLibraryMethods.LoadLibrary(dllToLoad);

            if (_nativeLibraryPointer == IntPtr.Zero)
            {
                throw new Exception($"Error Code ({Marshal.GetLastWin32Error()}) while trying to load {dllToLoad}");
            }
        }

        protected TDelegate Procedure<TDelegate>(string procedureName)
        {
            var entryPoint = ResolveNativeMethode(procedureName);
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(entryPoint);
        }


        private IntPtr ResolveNativeMethode(string procedureName)
        {
            return NativeLoadLibraryMethods.GetProcAddress(_nativeLibraryPointer, procedureName);
        }
    }
}