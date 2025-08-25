using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using WitsmlClient.Generated;
using WitsmlClient.Models;

namespace WitsmlClient;

public class WitsmlServiceClient : IWitsmlClient, IDisposable
{
    private readonly ChannelFactory<IWitsmlService> _channelFactory;
    private IWitsmlService? _channel;
    private readonly ConnectionSettings _settings;

    public WitsmlServiceClient(ConnectionSettings settings)
    {
        _settings = settings;
        
        var binding = CreateBinding();
        var endpoint = new EndpointAddress(settings.Uri);
        
        _channelFactory = new ChannelFactory<IWitsmlService>(binding, endpoint);
        
        if (!string.IsNullOrEmpty(settings.Username))
        {
            _channelFactory.Credentials.UserName.UserName = settings.Username;
            _channelFactory.Credentials.UserName.Password = settings.Password;
        }
    }

    private BasicHttpBinding CreateBinding()
    {
        var binding = new BasicHttpBinding
        {
            MaxReceivedMessageSize = int.MaxValue,
            MaxBufferSize = int.MaxValue,
            SendTimeout = _settings.Timeout,
            ReceiveTimeout = _settings.Timeout,
            Security = new BasicHttpSecurity
            {
                Mode = _settings.Uri.StartsWith("https") 
                    ? BasicHttpSecurityMode.Transport 
                    : BasicHttpSecurityMode.None
            }
        };

        if (_settings.EnableCompression)
        {
            binding.TransferMode = TransferMode.Buffered;
        }

        return binding;
    }

    public async Task<string> GetCapabilitiesAsync(string version)
    {
        EnsureChannel();
        
        var request = new GetCapRequest
        {
            OptionsIn = $"dataVersion={version}"
        };

        var response = await _channel!.GetCapAsync(request);
        return response.CapabilitiesOut ?? string.Empty;
    }

    public async Task<string> GetFromStoreAsync(string query, string options, string capabilities)
    {
        EnsureChannel();
        
        var objectType = ExtractObjectType(query);
        var request = new GetFromStoreRequest
        {
            WMLtypeIn = objectType,
            QueryIn = query,
            OptionsIn = options,
            CapabilitiesIn = capabilities
        };

        var response = await _channel!.GetFromStoreAsync(request);
        return response.XMLout ?? string.Empty;
    }

    public async Task<string> AddToStoreAsync(string xml, string options, string capabilities)
    {
        EnsureChannel();
        
        var objectType = ExtractObjectType(xml);
        var request = new AddToStoreRequest
        {
            WMLtypeIn = objectType,
            XMLin = xml,
            OptionsIn = options,
            CapabilitiesIn = capabilities
        };

        var response = await _channel!.AddToStoreAsync(request);
        return response.SuppMsgOut ?? string.Empty;
    }

    public async Task<string> UpdateInStoreAsync(string xml, string options, string capabilities)
    {
        EnsureChannel();
        
        var objectType = ExtractObjectType(xml);
        var request = new UpdateInStoreRequest
        {
            WMLtypeIn = objectType,
            XMLin = xml,
            OptionsIn = options,
            CapabilitiesIn = capabilities
        };

        var response = await _channel!.UpdateInStoreAsync(request);
        return response.SuppMsgOut ?? string.Empty;
    }

    public async Task<string> DeleteFromStoreAsync(string query, string options, string capabilities)
    {
        EnsureChannel();
        
        var objectType = ExtractObjectType(query);
        var request = new DeleteFromStoreRequest
        {
            WMLtypeIn = objectType,
            QueryIn = query,
            OptionsIn = options,
            CapabilitiesIn = capabilities
        };

        var response = await _channel!.DeleteFromStoreAsync(request);
        return response.SuppMsgOut ?? string.Empty;
    }

    private string ExtractObjectType(string xml)
    {
        if (xml.Contains("<wells"))
            return "well";
        if (xml.Contains("<wellbores"))
            return "wellbore";
        if (xml.Contains("<logs"))
            return "log";
        if (xml.Contains("<trajectorys"))
            return "trajectory";
        
        return "well";
    }

    private void EnsureChannel()
    {
        if (_channel == null)
        {
            _channel = _channelFactory.CreateChannel();
        }
    }

    public void Dispose()
    {
        (_channel as IDisposable)?.Dispose();
        _channelFactory?.Close();
    }
}
