using System;
using System.Threading.Tasks;

namespace Energistics.DataAccess
{
    public class WITSMLWebServiceConnection
    {
        private readonly Connection _connection;
        private readonly WMLSVersion _version;

        public WITSMLWebServiceConnection(Connection connection, WMLSVersion version)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _version = version;
        }

        public Connection Connection => _connection;
        public WMLSVersion Version => _version;

        public async Task<WitsmlResult> WMLS_GetCapAsync(string optionsIn)
        {
            return new WitsmlResult
            {
                XmlOut = "<capServers version=\"1.4.1.1\"><capServer apiVers=\"1.4.1.1\"><contact><name>Mock Server</name></contact></capServer></capServers>",
                ReturnCode = 1,
                OptionsIn = optionsIn ?? string.Empty
            };
        }

        public async Task<WitsmlResult> WMLS_GetFromStoreAsync(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return new WitsmlResult
            {
                XmlOut = "<wells version=\"1.4.1.1\"><well uid=\"mock-well\"><name>Mock Well</name></well></wells>",
                ReturnCode = 1,
                OptionsIn = optionsIn ?? string.Empty
            };
        }

        public async Task<WitsmlResult> WMLS_AddToStoreAsync(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return new WitsmlResult
            {
                XmlOut = string.Empty,
                ReturnCode = 1,
                OptionsIn = optionsIn ?? string.Empty,
                SuppMsgOut = "Data added successfully"
            };
        }

        public async Task<WitsmlResult> WMLS_UpdateInStoreAsync(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return new WitsmlResult
            {
                XmlOut = string.Empty,
                ReturnCode = 1,
                OptionsIn = optionsIn ?? string.Empty,
                SuppMsgOut = "Data updated successfully"
            };
        }

        public async Task<WitsmlResult> WMLS_DeleteFromStoreAsync(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return new WitsmlResult
            {
                XmlOut = string.Empty,
                ReturnCode = 1,
                OptionsIn = optionsIn ?? string.Empty,
                SuppMsgOut = "Data deleted successfully"
            };
        }

        public async Task<WitsmlResult> WMLS_GetBaseMsgAsync(short returnValueIn)
        {
            return new WitsmlResult
            {
                XmlOut = string.Empty,
                ReturnCode = 1,
                SuppMsgOut = "Base message retrieved successfully"
            };
        }
        
        public IWitsmlClient CreateClientProxy()
        {
            return new MockWitsmlClient();
        }
        
        public string GetVersion()
        {
            return _version.ToString();
        }
    }
    
    public class MockWitsmlClient : IWitsmlClient
    {
        public bool CompressRequests { get; set; } = false;
        
        public void Dispose()
        {
        }
        
        public System.Threading.Tasks.Task<string> GetCapAsync()
        {
            return System.Threading.Tasks.Task.FromResult("<capServers xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><capServer><contact><name>Mock Server</name></contact></capServer></capServers>");
        }
        
        public System.Threading.Tasks.Task<string> GetFromStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return System.Threading.Tasks.Task.FromResult($"<{objectType}s xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"></{objectType}s>");
        }
        
        public System.Threading.Tasks.Task<string> AddToStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return System.Threading.Tasks.Task.FromResult("1");
        }
        
        public System.Threading.Tasks.Task<string> UpdateInStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return System.Threading.Tasks.Task.FromResult("1");
        }
        
        public System.Threading.Tasks.Task<string> DeleteFromStoreAsync(string objectType, string xmlIn, string optionsIn, string capabilitiesIn)
        {
            return System.Threading.Tasks.Task.FromResult("1");
        }
        
        public string WMLS_GetCap(string optionsIn, string capabilitiesIn, string dataVersion)
        {
            return "<capServers xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><capServer><contact><name>Mock Server</name></contact></capServer></capServers>";
        }
        
        public string WMLS_GetBaseMsg(short returnValueIn)
        {
            return "Success";
        }
        
        public string WMLS_AddToStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion)
        {
            return "1";
        }
        
        public string WMLS_UpdateInStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion)
        {
            return "1";
        }
        
        public string WMLS_DeleteFromStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion)
        {
            return "1";
        }
        
        public string WMLS_GetFromStore(string wmlTypeIn, string xmlIn, string optionsIn, string capabilitiesIn, string dataVersion, out string suppMsgOut)
        {
            suppMsgOut = "";
            return $"<{wmlTypeIn}s xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"></{wmlTypeIn}s>";
        }
    }
}
