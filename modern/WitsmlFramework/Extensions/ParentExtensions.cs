using System;

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public static class ParentExtensions
    {
        public static void SetApplicationTitle(this object parent, string title)
        {
        }
        
        public static void SetStatusBarText(this object parent, string text)
        {
        }
        
        public static System.Action<string> LogQuery { get; set; } = (query) => { };
        
        public static System.Action<string> LogResponse { get; set; } = (response) => { };
        
        public static bool RetrievePartialResults = false;
        
        public static object ResolveAll(this object parent, Type type)
        {
            return new object[0];
        }
    }
}
