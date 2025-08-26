using System.Collections.Generic;
using Caliburn.Micro;

namespace PDS.WITSMLstudio.Desktop.Core.Extensions
{
    public static class WindowManagerExtensions
    {
        public static bool? ShowDialog(this IWindowManager windowManager, object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            return true;
        }
    }
}
