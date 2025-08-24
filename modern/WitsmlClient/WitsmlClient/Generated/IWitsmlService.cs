using System.ServiceModel;
using System.Threading.Tasks;

namespace WitsmlClient.Generated;

[ServiceContract(Namespace = "http://www.witsml.org/wsdl/120")]
public interface IWitsmlService
{
    [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_GetFromStore")]
    Task<GetFromStoreResponse> GetFromStoreAsync(GetFromStoreRequest request);
    
    [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_AddToStore")]
    Task<AddToStoreResponse> AddToStoreAsync(AddToStoreRequest request);
    
    [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_UpdateInStore")]
    Task<UpdateInStoreResponse> UpdateInStoreAsync(UpdateInStoreRequest request);
    
    [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_DeleteFromStore")]
    Task<DeleteFromStoreResponse> DeleteFromStoreAsync(DeleteFromStoreRequest request);
    
    [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_GetCap")]
    Task<GetCapResponse> GetCapAsync(GetCapRequest request);
}

[MessageContract(WrapperName = "WMLS_GetFromStore")]
public class GetFromStoreRequest
{
    [MessageBodyMember] public required string WMLtypeIn;
    [MessageBodyMember] public required string QueryIn;
    [MessageBodyMember] public required string OptionsIn;
    [MessageBodyMember] public required string CapabilitiesIn;
}

[MessageContract(WrapperName = "WMLS_GetFromStoreResponse")]
public class GetFromStoreResponse
{
    [MessageBodyMember] public short Result;
    [MessageBodyMember] public string? XMLout;
    [MessageBodyMember] public string? SuppMsgOut;
}

[MessageContract(WrapperName = "WMLS_AddToStore")]
public class AddToStoreRequest
{
    [MessageBodyMember] public required string WMLtypeIn;
    [MessageBodyMember] public required string XMLin;
    [MessageBodyMember] public required string OptionsIn;
    [MessageBodyMember] public required string CapabilitiesIn;
}

[MessageContract(WrapperName = "WMLS_AddToStoreResponse")]
public class AddToStoreResponse
{
    [MessageBodyMember] public short Result;
    [MessageBodyMember] public string? SuppMsgOut;
}

[MessageContract(WrapperName = "WMLS_UpdateInStore")]
public class UpdateInStoreRequest
{
    [MessageBodyMember] public required string WMLtypeIn;
    [MessageBodyMember] public required string XMLin;
    [MessageBodyMember] public required string OptionsIn;
    [MessageBodyMember] public required string CapabilitiesIn;
}

[MessageContract(WrapperName = "WMLS_UpdateInStoreResponse")]
public class UpdateInStoreResponse
{
    [MessageBodyMember] public short Result;
    [MessageBodyMember] public string? SuppMsgOut;
}

[MessageContract(WrapperName = "WMLS_DeleteFromStore")]
public class DeleteFromStoreRequest
{
    [MessageBodyMember] public required string WMLtypeIn;
    [MessageBodyMember] public required string QueryIn;
    [MessageBodyMember] public required string OptionsIn;
    [MessageBodyMember] public required string CapabilitiesIn;
}

[MessageContract(WrapperName = "WMLS_DeleteFromStoreResponse")]
public class DeleteFromStoreResponse
{
    [MessageBodyMember] public short Result;
    [MessageBodyMember] public string? SuppMsgOut;
}

[MessageContract(WrapperName = "WMLS_GetCap")]
public class GetCapRequest
{
    [MessageBodyMember] public required string OptionsIn;
}

[MessageContract(WrapperName = "WMLS_GetCapResponse")]
public class GetCapResponse
{
    [MessageBodyMember] public short Result;
    [MessageBodyMember] public string? CapabilitiesOut;
    [MessageBodyMember] public string? SuppMsgOut;
}
