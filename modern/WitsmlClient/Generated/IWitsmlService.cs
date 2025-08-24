using System.ServiceModel;
using System.Threading.Tasks;

namespace Energistics.DataAccess
{
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

        [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_GetBaseMsg")]
        Task<GetBaseMsgResponse> GetBaseMsgAsync(GetBaseMsgRequest request);

        [OperationContract(Action = "http://www.witsml.org/action/120/Store.WMLS_GetVersion")]
        Task<GetVersionResponse> GetVersionAsync(GetVersionRequest request);
    }

    [MessageContract(WrapperName = "WMLS_GetFromStore")]
    public class GetFromStoreRequest
    {
        [MessageBodyMember] public required string WMLtypeIn { get; set; }
        [MessageBodyMember] public required string QueryIn { get; set; }
        [MessageBodyMember] public required string OptionsIn { get; set; }
        [MessageBodyMember] public required string CapabilitiesIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetFromStoreResponse")]
    public class GetFromStoreResponse
    {
        [MessageBodyMember] public short Result { get; set; }
        [MessageBodyMember] public string? XMLout { get; set; }
        [MessageBodyMember] public string? SuppMsgOut { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_AddToStore")]
    public class AddToStoreRequest
    {
        [MessageBodyMember] public required string WMLtypeIn { get; set; }
        [MessageBodyMember] public required string XMLin { get; set; }
        [MessageBodyMember] public required string OptionsIn { get; set; }
        [MessageBodyMember] public required string CapabilitiesIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_AddToStoreResponse")]
    public class AddToStoreResponse
    {
        [MessageBodyMember] public short Result { get; set; }
        [MessageBodyMember] public string? SuppMsgOut { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_UpdateInStore")]
    public class UpdateInStoreRequest
    {
        [MessageBodyMember] public required string WMLtypeIn { get; set; }
        [MessageBodyMember] public required string XMLin { get; set; }
        [MessageBodyMember] public required string OptionsIn { get; set; }
        [MessageBodyMember] public required string CapabilitiesIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_UpdateInStoreResponse")]
    public class UpdateInStoreResponse
    {
        [MessageBodyMember] public short Result { get; set; }
        [MessageBodyMember] public string? SuppMsgOut { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_DeleteFromStore")]
    public class DeleteFromStoreRequest
    {
        [MessageBodyMember] public required string WMLtypeIn { get; set; }
        [MessageBodyMember] public required string QueryIn { get; set; }
        [MessageBodyMember] public required string OptionsIn { get; set; }
        [MessageBodyMember] public required string CapabilitiesIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_DeleteFromStoreResponse")]
    public class DeleteFromStoreResponse
    {
        [MessageBodyMember] public short Result { get; set; }
        [MessageBodyMember] public string? SuppMsgOut { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetCap")]
    public class GetCapRequest
    {
        [MessageBodyMember] public required string OptionsIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetCapResponse")]
    public class GetCapResponse
    {
        [MessageBodyMember] public short Result { get; set; }
        [MessageBodyMember] public string? CapabilitiesOut { get; set; }
        [MessageBodyMember] public string? SuppMsgOut { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetBaseMsg")]
    public class GetBaseMsgRequest
    {
        [MessageBodyMember] public short ReturnValueIn { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetBaseMsgResponse")]
    public class GetBaseMsgResponse
    {
        [MessageBodyMember] public string? Result { get; set; }
    }

    [MessageContract(WrapperName = "WMLS_GetVersion")]
    public class GetVersionRequest
    {
    }

    [MessageContract(WrapperName = "WMLS_GetVersionResponse")]
    public class GetVersionResponse
    {
        [MessageBodyMember] public string? Result { get; set; }
    }
}
