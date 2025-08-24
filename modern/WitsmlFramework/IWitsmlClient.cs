using System;
using System.Threading.Tasks;

namespace Energistics.DataAccess
{
    public interface IWitsmlClient : IDisposable
    {
        bool CompressRequests { get; set; }
        
        Task<string> GetCapAsync();
        Task<string> GetFromStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn);
        Task<string> AddToStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn);
        Task<string> UpdateInStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn);
        Task<string> DeleteFromStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn);
        
        string WMLS_GetCap(string optionsIn, string capabilitiesIn, string dataVersion);
        string WMLS_GetBaseMsg(short returnValueIn);
        string WMLS_AddToStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion);
        string WMLS_UpdateInStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion);
        string WMLS_DeleteFromStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion);
        string WMLS_GetFromStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion, out string suppMsgOut);
    }
    
    public static class WitsmlClientExtensions
    {
        public static IWitsmlClient WithUserAgent(this IWitsmlClient client, string userAgent = "WitsmlBrowser")
        {
            return client;
        }
    }
}
