namespace WitsmlFramework;

public static class WMLSVersion
{
    public const string Version131 = "1.3.1";
    public const string Version141 = "1.4.1";
    public const string Version1411 = "1.4.1.1";
    public const string Version200 = "2.0.0";
    public const string Version210 = "2.1.0";
    
    public static string GetLatest()
    {
        return Version1411;
    }
    
    public static bool IsSupported(string version)
    {
        return version == Version131 || version == Version141 || 
               version == Version1411 || version == Version200 || version == Version210;
    }
}
