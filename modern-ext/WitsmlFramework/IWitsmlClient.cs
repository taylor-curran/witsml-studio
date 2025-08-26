using System.Threading.Tasks;

namespace WitsmlFramework;

public interface IWitsmlClient
{
    Task<string> GetFromStoreAsync(string objectType, string query, string options, string capabilities);
    Task<string> AddToStoreAsync(string objectType, string xml, string options, string capabilities);
    Task<string> UpdateInStoreAsync(string objectType, string xml, string options, string capabilities);
    Task<string> DeleteFromStoreAsync(string objectType, string query, string options, string capabilities);
    Task<string> GetCapAsync(string options);
    Task<string> GetVersionAsync();
    
    short WMLS_GetFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string xmlOut, out string suppMsgOut);
    short WMLS_AddToStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
    short WMLS_UpdateInStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
    short WMLS_DeleteFromStore(string objectType, string xmlIn, string optionsIn, string capabilitiesIn, out string suppMsgOut);
    short WMLS_GetCap(string optionsIn, out string xmlOut, out string suppMsgOut);
    string WMLS_GetBaseMsg(string options);
    bool CompressRequests { get; set; }
}
