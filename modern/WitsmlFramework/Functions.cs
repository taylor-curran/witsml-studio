namespace Energistics.DataAccess
{
    public enum Functions
    {
        GetCap,
        GetBaseMsg,
        GetFromStore,
        AddToStore,
        UpdateInStore,
        DeleteFromStore,
        GetVersion
    }

    public static class FunctionExtensions
    {
        public static bool RequiresObjectType(this Functions function)
        {
            return function switch
            {
                Functions.GetFromStore => true,
                Functions.AddToStore => true,
                Functions.UpdateInStore => true,
                Functions.DeleteFromStore => true,
                _ => false
            };
        }

        public static bool SupportsRequestCompression(this Functions function)
        {
            return function switch
            {
                Functions.GetFromStore => true,
                Functions.AddToStore => true,
                Functions.UpdateInStore => true,
                Functions.DeleteFromStore => true,
                _ => false
            };
        }
    }
}
