using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using WitsmlClient.Models;
using Energistics.DataAccess;

namespace WitsmlClient;

public class WitsmlServiceClient : IDisposable
{
    private readonly ChannelFactory<IWitsmlService> _channelFactory;
    private IWitsmlService? _channel;
    private readonly ConnectionSettings _settings;

    public WitsmlServiceClient(ConnectionSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        
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
                Mode = _settings.Uri.StartsWith("https", StringComparison.OrdinalIgnoreCase) 
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

    public async Task<GetFromStoreResponse> GetFromStoreAsync(
        string objectType, 
        string xmlIn, 
        string? optionsIn = null)
    {
        EnsureChannel();
        
        var request = new GetFromStoreRequest
        {
            WMLtypeIn = objectType ?? throw new ArgumentNullException(nameof(objectType)),
            QueryIn = xmlIn ?? throw new ArgumentNullException(nameof(xmlIn)),
            OptionsIn = optionsIn ?? string.Empty,
            CapabilitiesIn = string.Empty
        };

        return await _channel!.GetFromStoreAsync(request);
    }

    public async Task<AddToStoreResponse> AddToStoreAsync(
        string objectType,
        string xmlIn,
        string? optionsIn = null)
    {
        EnsureChannel();
        
        var request = new AddToStoreRequest
        {
            WMLtypeIn = objectType ?? throw new ArgumentNullException(nameof(objectType)),
            XMLin = xmlIn ?? throw new ArgumentNullException(nameof(xmlIn)),
            OptionsIn = optionsIn ?? string.Empty,
            CapabilitiesIn = string.Empty
        };

        return await _channel!.AddToStoreAsync(request);
    }

    public async Task<UpdateInStoreResponse> UpdateInStoreAsync(
        string objectType,
        string xmlIn,
        string? optionsIn = null)
    {
        EnsureChannel();
        
        var request = new UpdateInStoreRequest
        {
            WMLtypeIn = objectType ?? throw new ArgumentNullException(nameof(objectType)),
            XMLin = xmlIn ?? throw new ArgumentNullException(nameof(xmlIn)),
            OptionsIn = optionsIn ?? string.Empty,
            CapabilitiesIn = string.Empty
        };

        return await _channel!.UpdateInStoreAsync(request);
    }

    public async Task<DeleteFromStoreResponse> DeleteFromStoreAsync(
        string objectType,
        string xmlIn,
        string? optionsIn = null)
    {
        EnsureChannel();
        
        var request = new DeleteFromStoreRequest
        {
            WMLtypeIn = objectType ?? throw new ArgumentNullException(nameof(objectType)),
            QueryIn = xmlIn ?? throw new ArgumentNullException(nameof(xmlIn)),
            OptionsIn = optionsIn ?? string.Empty,
            CapabilitiesIn = string.Empty
        };

        return await _channel!.DeleteFromStoreAsync(request);
    }

    public async Task<GetCapResponse> GetCapAsync(string? optionsIn = null)
    {
        EnsureChannel();
        
        var request = new GetCapRequest
        {
            OptionsIn = optionsIn ?? $"dataVersion={_settings.WitsmlVersion}"
        };

        return await _channel!.GetCapAsync(request);
    }

    public async Task<GetBaseMsgResponse> GetBaseMsgAsync(short returnCode)
    {
        EnsureChannel();
        
        var request = new GetBaseMsgRequest
        {
            ReturnValueIn = returnCode
        };

        return await _channel!.GetBaseMsgAsync(request);
    }

    public async Task<GetVersionResponse> GetVersionAsync()
    {
        EnsureChannel();
        
        var request = new GetVersionRequest();

        return await _channel!.GetVersionAsync(request);
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
        try
        {
            (_channel as IDisposable)?.Dispose();
        }
        catch
        {
        }

        try
        {
            _channelFactory?.Close();
        }
        catch
        {
            _channelFactory?.Abort();
        }
    }
}
