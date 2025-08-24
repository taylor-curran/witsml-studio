using System;

namespace Energistics.DataAccess
{
    public class WitsmlResult
    {
        public string XmlOut { get; set; } = string.Empty;
        public string SuppMsgOut { get; set; } = string.Empty;
        public string OptionsIn { get; set; } = string.Empty;
        public short ReturnCode { get; set; }
        public bool IsSuccessful => ReturnCode > 0;
    }
}
