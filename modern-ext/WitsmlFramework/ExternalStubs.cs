using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace System.ComponentModel.Composition
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class InheritedExportAttribute : Attribute
    {
        public InheritedExportAttribute() { }
        public InheritedExportAttribute(Type contractType) { }
        public Type? ContractType { get; set; }
    }
    
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ImportingConstructorAttribute : Attribute { }
}

namespace Energistics.DataAccess
{
    public interface IDataObject
    {
        string Uid { get; set; }
        string Name { get; set; }
    }
    
    public interface IEnergisticsCollection
    {
        string Uid { get; set; }
        System.Collections.IList Items { get; set; }
    }
}

namespace PDS.WITSMLstudio.Connections
{
    public enum ConnectionType
    {
        Witsml,
        Etp
    }
    
    public static class ConnectionTypes
    {
        public const string Witsml = "witsml";
        public const string Etp = "etp";
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public enum ErrorCodes : short
    {
        Success = 0,
        GeneralError = -1,
        ParialSuccess = 1
    }

    public static class WitsmlParser
    {
        public static string GetVersion(string xml) => "1.4.1.1";
        public static string GetVersion(System.Xml.Linq.XElement element) => "1.4.1.1";
        public static System.Xml.Linq.XDocument Parse(string xml) => System.Xml.Linq.XDocument.Parse("<root/>");
        public static System.Xml.Linq.XDocument Parse(string xml, string version) => System.Xml.Linq.XDocument.Parse("<root/>");
    }

    public static class TypeDecorationManager
    {
        public static void RegisterPropertyDecorator(Type type, object decorator) { }
        public static void Register(Type type) { }
    }
}

namespace System.Web.Security
{
    public static class Membership
    {
        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters) => "password";
    }
}

namespace WitsmlFramework
{
    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string ChangeLog = "changeLog";
        public const string CapServer = "capServer";
        public const string Trajectory = "trajectory";

        public static string GetFamily(string objectType) => "witsml";
        public static string GetObjectGroupType(string objectType) => objectType;
        public static string GetObjectTypeFromGroup(string objectGroup) => objectGroup;
        public static string GetObjectTypeFromGroup(System.Xml.Linq.XElement element) => element.Name.LocalName;
    }

    public class WitsmlException : Exception
    {
        public WitsmlException(string message) : base(message) { }
        public WitsmlException(string message, Exception innerException) : base(message, innerException) { }
        public int ErrorCode { get; set; }
    }

    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static bool EqualsIgnoreCase(this string str, string other)
        {
            return string.Equals(str, other, StringComparison.OrdinalIgnoreCase);
        }

        public static string Encrypt(this string str) => str;
        public static string Decrypt(this string str) => str;
        public static System.Security.SecureString ToSecureString(this string str)
        {
            var secureString = new System.Security.SecureString();
            foreach (char c in str)
                secureString.AppendChar(c);
            return secureString;
        }

        public static string GetDescription(this int value) => value.ToString();
        public static string GetDescription(this short value) => value.ToString();
        public static string GetDescription(this PDS.WITSMLstudio.Framework.ErrorCodes errorCode) => errorCode.ToString();
        public static string GetDescription(this Enum enumValue) => enumValue.ToString();
        public static object ParseEnum(this Type type, string value) => Enum.Parse(type, value);
        public static string GetName(this Enum enumValue) => enumValue.ToString();
        public static T NotNull<T>(this T value) where T : class => value ?? throw new ArgumentNullException(nameof(value));
        public static T NotNull<T>(this T value, string paramName) where T : class => value ?? throw new ArgumentNullException(paramName);
    }

    public class Connection
    {
        public string Name { get; set; } = "";
        public string Uri { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public System.Security.SecureString SecurePassword { get; set; } = new System.Security.SecureString();
        public string ProxyPassword { get; set; } = "";
        public System.Security.SecureString SecureProxyPassword { get; set; } = new System.Security.SecureString();
        public string SoapRequestCompressionMethod { get; set; } = "";
        
        public Connection() { }
        public Connection(string name, string uri) { Name = name; Uri = uri; }
        public override string ToString() => Name;
        
        public void UpdateProxy() { }
        public void UpdateProxy(object proxy) { }
        public WITSMLWebServiceConnection CreateProxy() => new WITSMLWebServiceConnection();
        public WITSMLWebServiceConnection CreateProxy(object proxy) => new WITSMLWebServiceConnection();
    }
    
    public interface IWitsmlClient
    {
        Connection Connection { get; set; }
        bool IsConnected { get; }
        short WMLS_GetCap(string optionsIn, out string xmlOut, out string suppMsgOut);
        short WMLS_AddToStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
        short WMLS_UpdateInStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
        short WMLS_DeleteFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
        short WMLS_GetFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string xmlOut, out string suppMsgOut);
    }

    public interface IContainer
    {
        T GetExportedValue<T>();
        object GetExportedValue(Type type);
        IEnumerable<T> ResolveAll<T>();
        T Resolve<T>();
    }
    
    public class Container : IContainer
    {
        public T GetExportedValue<T>() => default(T)!;
        public object GetExportedValue(Type type) => Activator.CreateInstance(type)!;
        public IEnumerable<T> ResolveAll<T>() => new List<T>();
        public T Resolve<T>() => default(T)!;
    }

    public class ResourceViewModel
    {
        public string Name { get; set; } = "";
        public string Uid { get; set; } = "";
        public object Resource { get; set; } = new object();
        public string MessageId { get; set; } = "";
        public bool IsSelected { get; set; } = false;
        public bool IsChecked { get; set; } = false;
        public List<ResourceViewModel> Children { get; set; } = new List<ResourceViewModel>();
    }

    public class ConnectionViewModel
    {
        public string Name { get; set; } = "";
        public Connection Connection { get; set; } = new Connection();
        public Connection DataItem { get; set; } = new Connection();
        public List<string> ConnectionNames { get; set; } = new List<string>();
        
        public ConnectionViewModel() { }
        public ConnectionViewModel(object runtime, Connection connection) 
        { 
            Connection = connection;
            DataItem = connection;
        }
        
        public void SaveConnectionFile() { }
        public void SaveConnectionFile(string filename) { }
        public Connection OpenConnectionFile() => new Connection();
    }

    public class AboutViewModel
    {
        public string Version { get; set; } = "1.0.0";
        
        public AboutViewModel() { }
        public AboutViewModel(object runtime) { }
    }
    
    public static class EnergisticsConverter
    {
        public static string XmlToJson(string xml) => "{}";
        public static string JsonToXml(string json) => "<root/>";
        public static string ObjectToXml(object obj) => "<root/>";
        public static T XmlToObject<T>(string xml) => default(T)!;
    }

}

namespace Caliburn.Micro
{
    public static class ScreenExtensions
    {
        public static void ActivateItem(this object conductor, object item) { }
        public static void TryClose(this object screen) { }
        public static void ActivateEmbeddedItem(this object screen, object item) { }
    }
}
