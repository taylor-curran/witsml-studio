using System.ComponentModel;

namespace WitsmlFramework
{
    public class Connection
    {
        public string Name { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public interface IWitsmlClient
    {
        Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities);
        Task<string> AddToStoreAsync(string objectType, string xml, string options, string capabilities);
        Task<string> UpdateInStoreAsync(string objectType, string xml, string options, string capabilities);
        Task<string> DeleteFromStoreAsync(string objectType, string query, string options, string capabilities);
    }

    public static class ErrorCodes
    {
        public const int Success = 0;
        public const int GeneralError = -1;
        public const int InvalidRequest = -401;
    }

    public enum Functions
    {
        GetFromStore,
        AddToStore,
        UpdateInStore,
        DeleteFromStore,
        GetCap,
        GetVersion
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
        
        public static string GetName(this Enum value)
        {
            return value.ToString();
        }
    }
}

namespace PDS.WITSMLstudio.Connections
{
    public class Connection
    {
        public string Name { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

namespace PDS.WITSMLstudio.Adapters
{
    public interface IWitsmlDataAdapter
    {
        string GetFromStore(string objectType, string query, string options, string capabilities);
    }
}

namespace PDS.WITSMLstudio.Data
{
    public static class Extensions
    {
        public static string ToXml(this object obj)
        {
            return "<stub>ToXml not implemented</stub>";
        }
    }
}

namespace PDS.WITSMLstudio.Linq
{
    public static class Extensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this object obj)
        {
            return Enumerable.Empty<T>();
        }
    }
}

namespace PDS.WITSMLstudio.Query
{
    public static class WitsmlQuery
    {
        public static string GetQuery(string objectType)
        {
            return "<stub>GetQuery not implemented</stub>";
        }
    }
}

namespace PDS.WITSMLstudio.Data.Channels
{
    public interface IChannelMetadataRecord
    {
        string Mnemonic { get; set; }
        string Unit { get; set; }
        string Description { get; set; }
    }
    
    public class ChannelMetadataRecord : IChannelMetadataRecord
    {
        public string Mnemonic { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

namespace Energistics.DataAccess
{
    public interface IDataObject
    {
        string Uid { get; set; }
        string Name { get; set; }
    }
    
    public class DataObject : IDataObject
    {
        public string Uid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}

namespace Energistics.DataAccess.WITSML131
{
    public class Well : DataObject
    {
    }
    
    public class Wellbore : DataObject
    {
    }
    
    public class Log : DataObject
    {
    }
}

namespace Energistics.DataAccess.WITSML141
{
    public class Well : DataObject
    {
    }
    
    public class Wellbore : DataObject
    {
    }
    
    public class Log : DataObject
    {
    }
}

namespace Energistics.DataAccess.Validation
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this object obj)
        {
            return true;
        }
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public interface IContainer
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        void RegisterInstance<T>(T instance);
    }
    
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
    
    public class Container : IContainer
    {
        public T GetInstance<T>()
        {
            return default(T);
        }
        
        public object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        
        public void RegisterInstance<T>(T instance)
        {
        }
    }
    
    public enum ConnectionTypes
    {
        Witsml,
        Etp,
        Http
    }
}

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public class ResourceViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Uid { get; set; } = string.Empty;
        public object DataObject { get; set; } = new object();
    }
    
    public class DataGridViewModel
    {
        public string Name { get; set; } = string.Empty;
    }
}

namespace PDS.WITSMLstudio
{
    public class WitsmlException : Exception
    {
        public WitsmlException() : base() { }
        public WitsmlException(string message) : base(message) { }
        public WitsmlException(string message, Exception innerException) : base(message, innerException) { }
    }
}
