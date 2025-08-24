using System;
using Caliburn.Micro;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public static class ScreenExtensions
    {
        public static void ActivateItem(this Screen screen, object item)
        {
        }
        
        public static void ActivateItem(this object conductor, object item)
        {
        }
    }
}

namespace PDS.WITSMLstudio.Desktop.Plugins.WitsmlBrowser.ViewModels.Request
{
    public static class GlobalActivateItemExtensions
    {
        public static void ActivateItem(this object conductor, object item)
        {
        }
    }
}
