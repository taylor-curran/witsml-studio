namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public interface IWitsmlContext
    {
        string DataSchemaVersion { get; set; }
        object DataObject { get; set; }
    }
    
    public class WitsmlContext : IWitsmlContext
    {
        public string DataSchemaVersion { get; set; } = string.Empty;
        public object DataObject { get; set; } = new object();
    }
}
