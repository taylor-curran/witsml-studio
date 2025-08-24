using System;
using System.Threading.Tasks;
using WitsmlClient.Models;
using Energistics.DataAccess;

namespace WitsmlClient;

public class WitsmlConnection : IDisposable
{
    private WitsmlServiceClient? _client;
    private readonly ConnectionSettings _settings;

    public WitsmlConnection(ConnectionSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public string Name => _settings.Uri;
    public string Uri => _settings.Uri;
    public string? Username => _settings.Username;
    public string WitsmlVersion => _settings.WitsmlVersion;
    public bool EnableCompression => _settings.EnableCompression;

    public async Task<WitsmlResult> GetCapabilitiesAsync(string? optionsIn = null)
    {
        EnsureClient();
        
        try
        {
            var response = await _client!.GetCapAsync(optionsIn);
            return new WitsmlResult(
                objectType: null,
                xmlIn: string.Empty,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: response.CapabilitiesOut,
                messageOut: response.SuppMsgOut,
                returnCode: response.Result
            );
        }
        catch (Exception ex)
        {
            return new WitsmlResult(
                objectType: null,
                xmlIn: string.Empty,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: ex.Message,
                returnCode: (short)ErrorCodes.ErrorProcessingRequest
            );
        }
    }

    public async Task<WitsmlResult> GetFromStoreAsync(string objectType, string xmlIn, string? optionsIn = null)
    {
        EnsureClient();
        
        try
        {
            var response = await _client!.GetFromStoreAsync(objectType, xmlIn, optionsIn);
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: response.XMLout,
                messageOut: response.SuppMsgOut,
                returnCode: response.Result
            );
        }
        catch (Exception ex)
        {
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: ex.Message,
                returnCode: (short)ErrorCodes.ErrorProcessingRequest
            );
        }
    }

    public async Task<WitsmlResult> AddToStoreAsync(string objectType, string xmlIn, string? optionsIn = null)
    {
        EnsureClient();
        
        try
        {
            var response = await _client!.AddToStoreAsync(objectType, xmlIn, optionsIn);
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: response.SuppMsgOut,
                returnCode: response.Result
            );
        }
        catch (Exception ex)
        {
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: ex.Message,
                returnCode: (short)ErrorCodes.ErrorProcessingRequest
            );
        }
    }

    public async Task<WitsmlResult> UpdateInStoreAsync(string objectType, string xmlIn, string? optionsIn = null)
    {
        EnsureClient();
        
        try
        {
            var response = await _client!.UpdateInStoreAsync(objectType, xmlIn, optionsIn);
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: response.SuppMsgOut,
                returnCode: response.Result
            );
        }
        catch (Exception ex)
        {
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: ex.Message,
                returnCode: (short)ErrorCodes.ErrorProcessingRequest
            );
        }
    }

    public async Task<WitsmlResult> DeleteFromStoreAsync(string objectType, string xmlIn, string? optionsIn = null)
    {
        EnsureClient();
        
        try
        {
            var response = await _client!.DeleteFromStoreAsync(objectType, xmlIn, optionsIn);
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: response.SuppMsgOut,
                returnCode: response.Result
            );
        }
        catch (Exception ex)
        {
            return new WitsmlResult(
                objectType: objectType,
                xmlIn: xmlIn,
                optionsIn: optionsIn,
                capClientIn: null,
                xmlOut: null,
                messageOut: ex.Message,
                returnCode: (short)ErrorCodes.ErrorProcessingRequest
            );
        }
    }

    private void EnsureClient()
    {
        if (_client == null)
        {
            _client = new WitsmlServiceClient(_settings);
        }
    }

    public void Dispose()
    {
        _client?.Dispose();
        _client = null;
    }
}

public class WitsmlResult
{
    public WitsmlResult(string? objectType, string xmlIn, string? optionsIn, string? capClientIn, 
                      string? xmlOut, string? messageOut, short returnCode)
    {
        ObjectType = objectType;
        XmlIn = xmlIn;
        OptionsIn = optionsIn;
        CapClientIn = capClientIn;
        XmlOut = xmlOut;
        MessageOut = messageOut;
        ReturnCode = returnCode;
    }

    public string? ObjectType { get; }
    public string XmlIn { get; }
    public string? OptionsIn { get; }
    public string? CapClientIn { get; }
    public string? XmlOut { get; }
    public string? MessageOut { get; }
    public short ReturnCode { get; }
}
