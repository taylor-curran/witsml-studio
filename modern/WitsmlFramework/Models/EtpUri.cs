using System;

namespace Energistics.Etp.Common.Datatypes
{
    public class EtpUri
    {
        public string Uri { get; set; } = string.Empty;
        
        public EtpUri() { }
        
        public EtpUri(string uri)
        {
            Uri = uri ?? string.Empty;
        }
        
        public override string ToString()
        {
            return Uri;
        }
        
        public static implicit operator string(EtpUri etpUri)
        {
            return etpUri?.Uri ?? string.Empty;
        }
        
        public static implicit operator EtpUri(string uri)
        {
            return new EtpUri(uri);
        }
    }
}
