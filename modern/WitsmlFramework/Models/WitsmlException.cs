using System;

namespace PDS.WITSMLstudio.Framework
{
    public class WitsmlException : Exception
    {
        public WitsmlException() : base() { }
        
        public WitsmlException(string message) : base(message) { }
        
        public WitsmlException(string message, Exception innerException) : base(message, innerException) { }
        
        public int ErrorCode { get; set; }
    }
}
