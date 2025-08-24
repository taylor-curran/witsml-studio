namespace PDS.WITSMLstudio.Desktop.Core.Providers
{
    public interface ISoapMessageHandler
    {
        void LogRequest(string action, string message);
        void LogResponse(string action, string message);
    }
}
