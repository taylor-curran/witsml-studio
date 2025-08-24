namespace WitsmlFramework;

public static class ReturnCodes
{
    public const short Success = 1;
    public const short PartialSuccess = 2;
    public const short SuccessWithWarnings = 1001;
    public const short PartialSuccessWithWarnings = 1002;
    
    public static bool IsSuccess(short code)
    {
        return code == Success || code == PartialSuccess || 
               code == SuccessWithWarnings || code == PartialSuccessWithWarnings;
    }
    
    public static bool IsError(short code)
    {
        return code < 0;
    }
    
    public static bool HasWarnings(short code)
    {
        return code == SuccessWithWarnings || code == PartialSuccessWithWarnings;
    }
}
