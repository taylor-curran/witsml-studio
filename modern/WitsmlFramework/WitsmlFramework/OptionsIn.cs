namespace WitsmlFramework;

public static class OptionsIn
{
    public const string DataVersion = "dataVersion";
    public const string MaxReturnNodes = "maxReturnNodes";
    public const string RequestLatestValues = "requestLatestValues";
    public const string ReturnElements = "returnElements";
    public const string CompressionMethod = "compressionMethod";
    
    public static class ReturnElementsValues
    {
        public const string All = "all";
        public const string IdOnly = "id-only";
        public const string HeaderOnly = "header-only";
        public const string DataOnly = "data-only";
        public const string StationLocationOnly = "station-location-only";
        public const string LatestChangeOnly = "latest-change-only";
    }
}
