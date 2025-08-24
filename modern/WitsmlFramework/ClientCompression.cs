using System;

namespace Energistics.DataAccess
{
    public static class ClientCompression
    {
        public static string Gzip { get; } = "gzip";
        public static string None { get; } = "none";
        
        public static string GZipCompressAndBase64Encode(string input)
        {
            return input;
        }
        
        public static string SafeDecompress(string input)
        {
            return input;
        }
    }
}
