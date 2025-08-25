using System.Threading.Tasks;

namespace WitsmlClient;

public interface IWitsmlClient
{
    Task<string> GetCapabilitiesAsync(string version);
    Task<string> GetFromStoreAsync(string query, string options, string capabilities);
    Task<string> AddToStoreAsync(string xml, string options, string capabilities);
    Task<string> UpdateInStoreAsync(string xml, string options, string capabilities);
    Task<string> DeleteFromStoreAsync(string query, string options, string capabilities);
}
