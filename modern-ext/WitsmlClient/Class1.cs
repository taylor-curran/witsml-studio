using WitsmlFramework;

namespace WitsmlClient;

public class WitsmlClient : IWitsmlClient
{
    private readonly Connection _connection;
    
    public WitsmlClient(Connection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }
    
    public Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities)
    {
        return Task.FromResult("<stub>GetFromStore not implemented</stub>");
    }
    
    public Task<string> AddToStoreAsync(string objectType, string xml, string options, string capabilities)
    {
        return Task.FromResult("<stub>AddToStore not implemented</stub>");
    }
    
    public Task<string> UpdateInStoreAsync(string objectType, string xml, string options, string capabilities)
    {
        return Task.FromResult("<stub>UpdateInStore not implemented</stub>");
    }
    
    public Task<string> DeleteFromStoreAsync(string objectType, string query, string options, string capabilities)
    {
        return Task.FromResult("<stub>DeleteFromStore not implemented</stub>");
    }
    
    public short WMLS_GetFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string xmlOut, out string suppMsgOut)
    {
        xmlOut = "<stub>WMLS_GetFromStore not implemented</stub>";
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
    
    public short WMLS_GetCap(string optionsIn, out string xmlOut, out string suppMsgOut)
    {
        xmlOut = "<stub>WMLS_GetCap not implemented</stub>";
        suppMsgOut = "";
        return 0;
    }
    
    public string WMLS_GetBaseMsg(string options)
    {
        return "<stub>WMLS_GetBaseMsg not implemented</stub>";
    }
    
    public bool CompressRequests { get; set; } = false;
    
    public Task<string> GetCapAsync(string options)
    {
        return Task.FromResult("<stub>GetCap not implemented</stub>");
    }
    
    public Task<string> GetVersionAsync()
    {
        return Task.FromResult("1.4.1.1");
    }
}
