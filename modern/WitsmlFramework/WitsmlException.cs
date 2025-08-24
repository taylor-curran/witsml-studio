using System;

namespace Energistics.DataAccess
{
    public class WitsmlException : Exception
    {
        public ErrorCodes ErrorCode { get; }

        public WitsmlException(ErrorCodes errorCode) : base(errorCode.GetDescription())
        {
            ErrorCode = errorCode;
        }

        public WitsmlException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public WitsmlException(ErrorCodes errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
