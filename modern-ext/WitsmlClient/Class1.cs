using WitsmlFramework;

namespace WitsmlClient;

public class WitsmlClient : IWitsmlClient
{
    private readonly Connection _connection;
    
    public WitsmlClient(Connection connection)
    {
        _connection = connection;
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
}
