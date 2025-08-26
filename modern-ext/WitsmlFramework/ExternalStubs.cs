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
    }
}

namespace PDS.WITSMLstudio.Connections
{
    public enum ConnectionType
    {
        Witsml,
        Etp
    }
}

namespace PDS.WITSMLstudio.Query
{
    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string ChangeLog = "changeLog";
        public const string CapServer = "capServer";
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public static class ErrorCodes
    {
        public const int Success = 0;
        public const int GeneralError = -1;
    }

    public static class WitsmlParser
    {
        public static string GetVersion(string xml) => "1.4.1.1";
    }

    public static class TypeDecorationManager
    {
        public static void RegisterPropertyDecorator(Type type, object decorator) { }
    }
}

namespace System.Web.Security
{
    public static class Membership
    {
        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters) => "password";
    }
}

namespace Energistics.DataAccess
{
    public interface IEnergisticsCollection
    {
        string Uid { get; set; }
    }
}

namespace WitsmlFramework
{
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
    }

    public static class ConnectionTypes
    {
        public const string Witsml = "witsml";
    }

    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";

        public static string GetFamily(string objectType) => "witsml";
        public static string GetObjectGroupType(string objectType) => objectType;
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
        
        public Connection() { }
        public Connection(string name, string uri) { Name = name; Uri = uri; }
        public override string ToString() => Name;
    }
    
    public interface IWitsmlClient
    {
        Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities);
        Connection Connection { get; set; }
        bool IsConnected { get; }
    }

    public interface IContainer
    {
        T GetExportedValue<T>();
        object GetExportedValue(Type type);
        IEnumerable<T> ResolveAll<T>();
    }
    
    public class Container : IContainer
    {
        public T GetExportedValue<T>() => default(T)!;
        public object GetExportedValue(Type type) => Activator.CreateInstance(type)!;
        public IEnumerable<T> ResolveAll<T>() => new List<T>();
    }

    public class ResourceViewModel
    {
        public string Name { get; set; } = "";
        public string Uid { get; set; } = "";
    }

    public class ConnectionViewModel
    {
        public string Name { get; set; } = "";
        public Connection Connection { get; set; } = new Connection();
    }

    public class AboutViewModel
    {
        public string Version { get; set; } = "1.0.0";
    }
}
