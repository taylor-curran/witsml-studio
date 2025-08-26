using System;

namespace WitsmlFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ImportingConstructorAttribute : Attribute
    {
    }
}

namespace WitsmlFramework.Interfaces
{
    public interface IPluginViewModel
    {
        string DisplayName { get; }
    }

    public interface ISoapMessageHandler
    {
        void LogRequest(string request, string response);
        void LogResponse(string request, string response);
        void HandleMessage(string message);
    }
}
