using System;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class Win32WindowHandle
    {
        public IntPtr Handle { get; set; }
        
        public Win32WindowHandle(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
