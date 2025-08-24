using System;

namespace PDS.WITSMLstudio.Connections
{
    public class Connection
    {
        public string Name { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Version { get; set; } = "1.4.1.1";
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
        public bool IsSecure { get; set; } = true;
        public string SoapRequestCompressionMethod { get; set; } = string.Empty;
        
        public Connection()
        {
        }
        
        public Connection(string name, string uri)
        {
            Name = name;
            Uri = uri;
        }
        
        public object CreateProxy()
        {
            return new object();
        }
        
        public object CreateProxy(string version)
        {
            return new object();
        }
        
        public void UpdateProxy(object proxy)
        {
        }
        
        public Energistics.DataAccess.IWitsmlClient CreateClientProxy()
        {
            return new MockWitsmlClient();
        }
    }
    
    public class MockWitsmlClient : Energistics.DataAccess.IWitsmlClient
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
