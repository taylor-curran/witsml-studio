using System;
using System.Collections.Generic;

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
    }
}

namespace Energistics.Etp.Common.Datatypes.Object
{
    public interface IResource
    {
        string Name { get; set; }
        string Uri { get; set; }
        int? TargetCount { get; set; }
    }
}

namespace Energistics.Etp.v11.Datatypes.Object
{
    public class Resource : Energistics.Etp.Common.Datatypes.Object.IResource
    {
        public string Name { get; set; } = "";
        public string Uri { get; set; } = "";
        public int? TargetCount { get; set; }
    }
}

namespace PDS.WITSMLstudio.Framework
{
    public enum ErrorCodes : short
    {
        Success = 0,
        GeneralError = -1
    }
}

namespace WitsmlFramework
{
    public static class Extensions
    {
        public static string Uri(this object obj) => obj?.ToString() ?? "";
        public static T NotNull<T>(this T value) where T : class => value ?? throw new ArgumentNullException(nameof(value));
    }

    public static class ObjectTypes
    {
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
    }

    public class Connection
    {
        public string Name { get; set; } = "";
        public string Uri { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        
        public Connection() { }
        public Connection(string name, string uri) { Name = name; Uri = uri; }
        public override string ToString() => Name;
    }
    
    
    public interface IWitsmlClient
    {
        Connection Connection { get; set; }
        bool IsConnected { get; }
        short WMLS_GetFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string xmlOut, out string suppMsgOut);
    }
}

namespace Caliburn.Micro
{
    public static class ScreenExtensions
    {
        public static void ActivateEmbeddedItem(this object screen, object item) { }
    }
    
    public static class ScreenMethods
    {
        public static void ActivateItem(object item) { }
        public static void TryClose() { }
        public static void TryClose(bool? dialogResult) { }
    }
    
    public class Win32WindowHandle
    {
        public IntPtr Handle { get; set; }
        public Win32WindowHandle(IntPtr handle) { Handle = handle; }
    }
}
