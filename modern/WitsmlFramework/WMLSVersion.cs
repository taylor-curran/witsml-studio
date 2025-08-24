using System;

namespace Energistics.DataAccess
{
    public enum WMLSVersion
    {
        WITSML131,
        WITSML141
    }
    
    public static class WMLSVersionExtensions
    {
        public static string ToVersionString(this WMLSVersion version)
        {
            return version switch
            {
                WMLSVersion.WITSML131 => "1.3.1.1",
                WMLSVersion.WITSML141 => "1.4.1.1", 
                _ => "1.4.1.1"
            };
        }
    }
}
