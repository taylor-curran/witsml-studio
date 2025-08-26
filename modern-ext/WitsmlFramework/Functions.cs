namespace WitsmlFramework
{

public enum Functions
{
    GetFromStore,
    AddToStore,
    UpdateInStore,
    DeleteFromStore,
    GetCap,
    GetVersion,
    GetBaseMsg
}

public static class FunctionsExtensions
{
    public static string GetBaseMsg(this Functions function)
    {
        return "<stub>BaseMsg not implemented</stub>";
    }
    
    public static bool RequiresObjectType(this Functions function)
    {
        return true;
    }
    
    public static bool SupportsRequestCompression(this Functions function)
    {
        return false;
    }
}

public static class FunctionsHelper
{
    public static string GetBaseMsg(Functions function)
    {
        return "<stub>BaseMsg not implemented</stub>";
    }
}









public class OptionsIn
{
    private Dictionary<string, string> _options = new Dictionary<string, string>();
    
    public OptionsIn() { }
    
    public OptionsIn(string key, string value)
    {
        _options[key] = value;
    }
    
    public string this[string key]
    {
        get => _options.ContainsKey(key) ? _options[key] : "";
        set => _options[key] = value;
    }
    
    public IEnumerable<string> Keys => _options.Keys;
    
    public void Add(OptionsIn option)
    {
        foreach (var kvp in option._options)
        {
            _options[kvp.Key] = kvp.Value;
        }
    }
    
    public static OptionsIn Parse(string options)
    {
        var result = new OptionsIn();
        if (string.IsNullOrEmpty(options)) return result;
        
        var pairs = options.Split(';');
        foreach (var pair in pairs)
        {
            var parts = pair.Split('=');
            if (parts.Length == 2)
            {
                result[parts[0].Trim()] = parts[1].Trim();
            }
        }
        return result;
    }
    
    public static string Join(OptionsIn[] options)
    {
        var allOptions = new OptionsIn();
        foreach (var option in options)
        {
            allOptions.Add(option);
        }
        
        var pairs = allOptions._options.Select(kvp => $"{kvp.Key}={kvp.Value}");
        return string.Join(";", pairs);
    }
    
    public override string ToString()
    {
        var pairs = _options.Select(kvp => $"{kvp.Key}={kvp.Value}");
        return string.Join(";", pairs);
    }
    
    public static implicit operator string(OptionsIn optionsIn)
    {
        return optionsIn.ToString();
    }
    
    public static class RequestObjectSelectionCapability
    {
        public static string True = "true";
        public static string False = "false";
    }
    
    public static class RequestPrivateGroupOnly
    {
        public static string True = "true";
        public static string False = "false";
    }
    
    public static class CompressionMethod
    {
        public static string Gzip = "gzip";
        public static string None = "none";
    }
    
    public class ReturnElements
    {
        public static readonly ReturnElements All = new ReturnElements("all");
        public static readonly ReturnElements IdOnly = new ReturnElements("id-only");
        public static readonly ReturnElements HeaderOnly = new ReturnElements("header-only");
        public static readonly ReturnElements DataOnly = new ReturnElements("data-only");
        public static readonly ReturnElements StationLocationOnly = new ReturnElements("station-location-only");
        public static readonly ReturnElements LatestChangeOnly = new ReturnElements("latest-change-only");
        public static readonly ReturnElements Requested = new ReturnElements("requested");
        
        private readonly string _value;
        
        public ReturnElements(string value)
        {
            _value = value;
        }
        
        public string Value() => _value;
        public string Key() => _value;
        
        public static implicit operator string(ReturnElements element)
        {
            return element._value;
        }
        
        public static ReturnElements[] GetValues()
        {
            return new[] { All, IdOnly, HeaderOnly, DataOnly, StationLocationOnly, LatestChangeOnly, Requested };
        }
    }
    
    public static implicit operator OptionsIn(ReturnElements element)
    {
        return new OptionsIn("returnElements", element.Value());
    }
    
    public class MaxReturnNodes
    {
        public string Value { get; set; } = "1000";
        
        public MaxReturnNodes(int value)
        {
            Value = value.ToString();
        }
        
        public MaxReturnNodes(string value)
        {
            Value = value;
        }
        
        public static implicit operator string(MaxReturnNodes node)
        {
            return node.Value;
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
    
    public class RequestLatestValues
    {
        public string Value { get; set; } = "";
        
        public RequestLatestValues(int value)
        {
            Value = value.ToString();
        }
        
        public RequestLatestValues(string value)
        {
            Value = value;
        }
        
        public static implicit operator string(RequestLatestValues values)
        {
            return values.Value;
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
    
    
    
    
    public class DataVersion
    {
        public static string Value { get; set; } = "1.4.1.1";
        public static string Version131 = "1.3.1";
        public static string Version141 = "1.4.1";
        
        public DataVersion(string version)
        {
            Value = version;
        }
        
        public static implicit operator string(DataVersion version)
        {
            return Value;
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
    
    public static string GetValue(string key)
    {
        return "";
    }
    
    public static string GetValue(string key, string defaultValue)
    {
        return defaultValue;
    }
    
    public static class CascadedDelete
    {
        public static string True = "true";
        public static string False = "false";
    }
}

public class WITSMLWebServiceConnection : IDisposable
{
    public string Name { get; set; } = "";
    public string Uri { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Version { get; set; } = "1.4.1.1";
    
    public string GetVersion()
    {
        return Version;
    }
    
    public WITSMLWebServiceConnection CreateClientProxy()
    {
        return this;
    }
    
    public short WMLS_GetCap(string optionsIn, out string xmlOut, out string suppMsgOut)
    {
        xmlOut = "<stub>Capabilities not implemented</stub>";
        suppMsgOut = "";
        return 0;
    }
    
    public short WMLS_AddToStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut)
    {
        suppMsgOut = "";
        return 0;
    }
    
    public short WMLS_UpdateInStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut)
    {
        suppMsgOut = "";
        return 0;
    }
    
    public short WMLS_DeleteFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut)
    {
        suppMsgOut = "";
        return 0;
    }
    
    public short WMLS_GetFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string xmlOut, out string suppMsgOut)
    {
        xmlOut = "<stub>GetFromStore not implemented</stub>";
        suppMsgOut = "";
        return 0;
    }
    
    public WITSMLWebServiceConnection WithUserAgent(string userAgent = "")
    {
        return this;
    }
    
    public void Dispose()
    {
    }
}

public enum WMLSVersion
{
    WITSML131,
    WITSML141,
    WITSML200,
    WITSML210
}

public static class ReturnElementsExtensions
{
    public static string ToStringValue(this OptionsIn.ReturnElements? element)
    {
        if (element == null) return "all";
        return element.Value();
    }
}

}
