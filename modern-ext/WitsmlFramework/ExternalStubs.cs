using System;
using System.ComponentModel;

namespace PDS.WITSMLstudio.Connections
{
    public static class ConnectionTypes
    {
        public static string Witsml = "WITSML";
        public static string Etp = "ETP";
    }
}

namespace WitsmlFramework
{
    public static class ConnectionTypes
    {
        public static string Witsml = "WITSML";
        public static string Etp = "ETP";
    }
    
    public enum ConnectionType
    {
        Witsml,
        Etp
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public static class FrameworkExtensions
    {
    }
}

namespace PDS.WITSMLstudio.Data
{
    public static class DataExtensions
    {
    }
}

namespace PDS.WITSMLstudio.Adapters
{
    public static class AdapterExtensions
    {
    }
}

namespace PDS.WITSMLstudio.Linq
{
    public static class LinqExtensions
    {
    }
}

namespace PDS.WITSMLstudio.Query
{
    public class QueryTemplates
    {
        public static string GetTemplate(string objectType, string family)
        {
            return "<stub>Template not implemented</stub>";
        }
        
        public static string GetTemplate(string objectType, string family, string version, string options)
        {
            return "<stub>Template not implemented</stub>";
        }
    }
    
    public static class ObjectFamilies
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string Witsml = "witsml";
    }
    
    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string Trajectory = "trajectory";
        public const string MudLog = "mudLog";
        public const string CementJob = "cementJob";
        public const string ConvCore = "convCore";
        public const string SidewallCore = "sidewallCore";
        public const string FluidsReport = "fluidsReport";
        public const string BhaRun = "bhaRun";
        public const string Tubular = "tubular";
        public const string Target = "target";
        public const string Realtime = "realtime";
        public const string SurveyProgram = "surveyProgram";
        public const string Attachment = "attachment";
        public const string ChangeLog = "changeLog";
        public const string ObjectGroup = "objectGroup";
        public const string StimJob = "stimJob";
        public const string DrillReport = "drillReport";
        public const string WellCMLedger = "wellCMLedger";
        public const string WellCompletion = "wellCompletion";
        public const string OpsReport = "opsReport";
        public const string ToolErrorModel = "toolErrorModel";
        public const string ToolErrorTermSet = "toolErrorTermSet";
        public const string DtsInstalledSystem = "dtsInstalledSystem";
        public const string DtsMeasurement = "dtsMeasurement";
        public const string CapServer = "capServer";
        
        public static string Root(string objectType)
        {
            return objectType;
        }
        
        public static string GetObjectTypeFromGroup(string group, string objectType)
        {
            return objectType;
        }
        
        public static string GetObjectTypeFromGroup(object documentRoot)
        {
            return "well";
        }
        
        public static string GetObjectTypeFromGroup(System.Xml.Linq.XElement documentRoot)
        {
            if (documentRoot?.Name?.LocalName != null)
            {
                return documentRoot.Name.LocalName.ToLower();
            }
            return "well";
        }
    }
}

namespace WitsmlFramework
{
    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string Trajectory = "trajectory";
        public const string MudLog = "mudLog";
        public const string CementJob = "cementJob";
        public const string ConvCore = "convCore";
        public const string SidewallCore = "sidewallCore";
        public const string FluidsReport = "fluidsReport";
        public const string BhaRun = "bhaRun";
        public const string Tubular = "tubular";
        public const string Target = "target";
        public const string Realtime = "realtime";
        public const string SurveyProgram = "surveyProgram";
        public const string Attachment = "attachment";
        public const string ChangeLog = "changeLog";
        public const string ObjectGroup = "objectGroup";
        public const string StimJob = "stimJob";
        public const string DrillReport = "drillReport";
        public const string WellCMLedger = "wellCMLedger";
        public const string WellCompletion = "wellCompletion";
        public const string OpsReport = "opsReport";
        public const string ToolErrorModel = "toolErrorModel";
        public const string ToolErrorTermSet = "toolErrorTermSet";
        public const string DtsInstalledSystem = "dtsInstalledSystem";
        public const string DtsMeasurement = "dtsMeasurement";
        public const string CapServer = "capServer";
        
        public static string Root(string objectType)
        {
            return objectType;
        }
        
        public static string GetObjectTypeFromGroup(string group, string objectType)
        {
            return objectType;
        }
        
        public static string GetObjectTypeFromGroup(System.Xml.Linq.XElement documentRoot)
        {
            if (documentRoot?.Name?.LocalName != null)
            {
                return documentRoot.Name.LocalName.ToLower();
            }
            return "well";
        }
        
        public static string GetFamily(System.Xml.Linq.XElement element)
        {
            return "witsml";
        }
        
        public static Type GetObjectGroupType(string objectType, string family, string version)
        {
            return typeof(object);
        }
    }
}

namespace Energistics.DataAccess
{
    public class WitsmlVersion
    {
        public static string Version131 = "1.3.1";
        public static string Version141 = "1.4.1";
    }
    
    public interface IDataObject
    {
        string Uid { get; set; }
        string Name { get; set; }
    }
    
    public static class WITSML131
    {
        public static string Version = "1.3.1";
    }
    
    public static class WITSML141
    {
        public static string Version = "1.4.1";
    }
    
    namespace Validation
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; } = "";
        }
    }
}

namespace System.ComponentModel.Composition
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ImportingConstructorAttribute : Attribute
    {
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public class ExportAttribute : Attribute
    {
        public string? ContractName { get; set; }
        public Type? ContractType { get; set; }
        
        public ExportAttribute() { }
        public ExportAttribute(Type contractType) { ContractType = contractType; }
        public ExportAttribute(string contractName) { ContractName = contractName; }
        public ExportAttribute(string contractName, Type contractType) 
        { 
            ContractName = contractName; 
            ContractType = contractType; 
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class InheritedExportAttribute : ExportAttribute
    {
        public InheritedExportAttribute() : base() { }
        public InheritedExportAttribute(Type contractType) : base(contractType) { }
        public InheritedExportAttribute(string contractName) : base(contractName) { }
        public InheritedExportAttribute(string contractName, Type contractType) : base(contractName, contractType) { }
    }
}




namespace System.Windows.Interactivity
{
    public class Behavior<T> where T : class
    {
    }
}

namespace System.Windows.Forms
{
    public class FolderBrowserDialog
    {
        public string SelectedPath { get; set; } = "";
        public bool ShowNewFolderButton { get; set; } = true;
        public string Description { get; set; } = "";
        
        public DialogResult ShowDialog()
        {
            return DialogResult.OK;
        }
        
        public DialogResult ShowDialog(object owner)
        {
            return DialogResult.OK;
        }
    }
    
    public enum DialogResult
    {
        OK,
        Cancel
    }
    
    public interface IWin32Window
    {
        IntPtr Handle { get; }
    }
}

public class Win32WindowHandle
{
    public IntPtr Handle { get; set; }
    
    public Win32WindowHandle(IntPtr handle)
    {
        Handle = handle;
    }
}

namespace PDS.WITSMLstudio.Desktop.Core.ViewModels
{
    public interface IPluginViewModel
    {
        string DisplayName { get; }
    }
    
    public class ShellViewModel
    {
        public string StatusBarText { get; set; } = "";
        public bool RetrievePartialResults { get; set; } = false;
        public object LogQuery { get; set; } = new object();
        public object LogResponse { get; set; } = new object();
    }
}

namespace WitsmlFramework
{
    public static class WitsmlParser
    {
        public static System.Xml.Linq.XDocument Parse(string xml)
        {
            try
            {
                return System.Xml.Linq.XDocument.Parse(xml);
            }
            catch
            {
                return new System.Xml.Linq.XDocument(new System.Xml.Linq.XElement("root"));
            }
        }
        
        public static object Parse(Type dataType, System.Xml.Linq.XElement element)
        {
            return Activator.CreateInstance(dataType);
        }
    }
    
    public interface IEnergisticsCollection
    {
        System.Collections.IList Items { get; }
    }
    
    public static class TypeDecorationManager
    {
        public static void Register(Type type)
        {
        }
    }
    
    public static class CompressionMethods
    {
        public const string None = "none";
        public const string Gzip = "gzip";
    }
    
    public static class ClientCompression
    {
        public static string True = "true";
        public static string False = "false";
        
        public static string SafeDecompress(string input)
        {
            return input;
        }
        
        public static string GZipCompressAndBase64Encode(string input)
        {
            return input;
        }
    }
    
    public class WitsmlException : Exception
    {
        public int ErrorCode { get; set; }
        
        public WitsmlException(string message) : base(message) { }
        public WitsmlException(string message, Exception innerException) : base(message, innerException) { }
        public WitsmlException(string message, int errorCode) : base(message) 
        { 
            ErrorCode = errorCode; 
        }
    }
}

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
        
        public static string GetDescription(this int value)
        {
            return value.ToString();
        }
    }
}

namespace System
{
    public static class StringExtensions
    {
        public static string True => "true";
        public static string False => "false";
        public static string Gzip => "gzip";
        public static string None => "none";
    }
}

namespace WitsmlFramework
{
    public static class StringConstants
    {
        public static string True = "true";
        public static string False = "false";
        public static string Gzip = "gzip";
        public static string None = "none";
    }

    public interface IEtpClient
    {
        bool IsConnected { get; }
        void Connect();
        void Disconnect();
    }

    public class EtpUri
    {
        public string Uri { get; set; } = "";
        public static EtpUri RootUri => new EtpUri { Uri = "eml:///" };
        public override string ToString() => Uri;
    }

    public interface IWitsmlContext
    {
        Connection Connection { get; set; }
        string DataSchemaVersion { get; set; }
        object LogQuery { get; set; }
        object LogResponse { get; set; }
    }

    public interface IMessageHeader
    {
        string MessageId { get; set; }
        DateTime TimeStamp { get; set; }
    }

    public interface ISpecificRecord
    {
        string Uri { get; set; }
    }

    public interface ISupportedProtocol
    {
        string Protocol { get; set; }
        string ProtocolVersion { get; set; }
    }

    public interface IResource
    {
        string Uri { get; set; }
        string Name { get; set; }
        DateTime LastChanged { get; set; }
    }

    public interface IWellObject
    {
        string Uid { get; set; }
        string Name { get; set; }
    }

    public interface IWellboreObject : IWellObject
    {
        string UidWell { get; set; }
        string NameWell { get; set; }
        string UidWellbore { get; set; }
        string NameWellbore { get; set; }
    }

    public class LogCurveInfo
    {
        public string Mnemonic { get; set; } = "";
        public string Unit { get; set; } = "";
        public string CurveDescription { get; set; } = "";
    }

    public interface IContainer
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        IEnumerable<T> GetAllInstances<T>();
        IEnumerable<object> GetAllInstances(Type type);
        void BuildUp(object instance);
        T Resolve<T>();
        object Resolve(Type type);
        IEnumerable<T> ResolveAll<T>();
    }

    public class ResourceViewModel : INotifyPropertyChanged
    {
        public string Uri { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime LastChanged { get; set; }
        public string ContentType { get; set; } = "";
        public object Resource { get; set; } = new object();
        public System.Collections.ObjectModel.ObservableCollection<ResourceViewModel> Children { get; set; } = new System.Collections.ObjectModel.ObservableCollection<ResourceViewModel>();
        public bool IsChecked { get; set; }
        public bool IsSelected { get; set; }
        public string MessageId { get; set; } = "";
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; } = "";
        public string Uri { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public object DataItem { get; set; } = new object();
        public object ConnectionNames { get; set; } = new object();
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ConnectionViewModel(object runtime, object connectionType)
        {
        }
        
        public void SaveConnectionFile()
        {
        }
        
        public void OpenConnectionFile()
        {
        }
        
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class AboutViewModel : INotifyPropertyChanged
    {
        public string Version { get; set; } = "1.0.0";
        public string Copyright { get; set; } = "Copyright";
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public class Connection
    {
        public string Name { get; set; } = "";
        public string Uri { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ProxyHost { get; set; } = "";
        public int ProxyPort { get; set; }
        public string ProxyUsername { get; set; } = "";
        public string ProxyPassword { get; set; } = "";
        public bool IsSecure { get; set; }
        public string AuthenticationType { get; set; } = "";
        public string JsonWebToken { get; set; } = "";
        public string SubProtocol { get; set; } = "";
        public string ApplicationName { get; set; } = "";
        public string ApplicationVersion { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string DataVersion { get; set; } = "";
        public string RequestedProtocols { get; set; } = "";
        public string SupportedCompression { get; set; } = "";
        public string SupportedFormats { get; set; } = "";
        public string SupportedDataObjects { get; set; } = "";
        public string PreferRealtime { get; set; } = "";
        public string RedirectUrl { get; set; } = "";
        public string Timeout { get; set; } = "";
        public string SecurityProtocol { get; set; } = "";
        public string SoapRequestCompressionMethod { get; set; } = "";
        public string Version { get; set; } = "1.4.1.1";
        public int TimeoutInMinutes { get; set; } = 5;
        public System.Security.SecureString SecurePassword { get; set; } = new System.Security.SecureString();
        public System.Security.SecureString SecureProxyPassword { get; set; } = new System.Security.SecureString();
        
        public WITSMLWebServiceConnection CreateProxy(object connectionType)
        {
            return new WITSMLWebServiceConnection
            {
                Name = this.Name,
                Uri = this.Uri,
                Username = this.Username,
                Password = this.Password,
                Version = this.Version
            };
        }
        
        public void UpdateProxy()
        {
        }
        
        public void UpdateProxy(object connectionType)
        {
        }
    }


}

namespace WitsmlFramework.Extensions
{
    public static class StringExtensions
    {
        public static string Decrypt(this string value)
        {
            return value;
        }
        
        public static string Encrypt(this string value)
        {
            return value;
        }
        
        public static System.Security.SecureString ToSecureString(this string value)
        {
            var secureString = new System.Security.SecureString();
            if (!string.IsNullOrEmpty(value))
            {
                foreach (char c in value)
                {
                    secureString.AppendChar(c);
                }
            }
            return secureString;
        }
        
        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
        }
    }
    
    public static class ActionExtensions
    {
        public static T NotNull<T>(this T value) where T : class
        {
            return value ?? throw new ArgumentNullException();
        }
    }
    
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return value.ToString();
        }
        
        public static string GetName(this Enum value)
        {
            return value.ToString();
        }
        
        public static object ParseEnum(this Type type, string value)
        {
            return Enum.Parse(type, value);
        }
    }
    
    public static class EnumerableExtensions
    {
        public static string GetDescription(this int value)
        {
            return value.ToString();
        }
        
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}

namespace System.Web.Security
{
    public static class HttpUtility
    {
        public static string HtmlEncode(string value)
        {
            return System.Net.WebUtility.HtmlEncode(value);
        }
    }
    
    public static class Membership
    {
        public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            return "password123";
        }
    }
}

namespace System.Windows.Forms
{
    public class SaveFileDialog
    {
        public string Filter { get; set; } = "";
        public string FileName { get; set; } = "";
        public string Title { get; set; } = "";
        public string DefaultExt { get; set; } = "";
        public DialogResult ShowDialog() => DialogResult.OK;
        public DialogResult ShowDialog(IWin32Window owner) => DialogResult.OK;
    }
}

namespace Energistics.DataAccess
{
    public static class EnergisticsConverter
    {
        public static string XmlToJson(string xml)
        {
            return "{}";
        }
        
        public static string JsonToXml(string json)
        {
            return "<root></root>";
        }
    }
}

namespace Caliburn.Micro
{
    public static class ScreenExtensions
    {
        public static void TryClose(this object screen)
        {
        }
        
        public static void ActivateItem(this object conductor, object item)
        {
        }
        
        public static void ActivateEmbeddedItem(this object screen, object item)
        {
        }
    }
    
    public static class WindowManagerExtensions
    {
        public static bool? ShowDialog(this object windowManager, object viewModel)
        {
            return true;
        }
    }
}
