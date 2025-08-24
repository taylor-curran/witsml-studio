using System;
using System.ComponentModel;

namespace Energistics.DataAccess
{
    public enum ErrorCodes : short
    {
        [Description("Unset")]
        Unset = 0,

        [Description("Function completed successfully.")]
        Success = 1,

        [Description("Partial success: Function completed successfully, but some growing data-object data-nodes were not returned.")]
        PartialSuccess = 2,
        
        [Description("Partial success: Function completed successfully, but some growing data-object data-nodes were not returned.")]
        ParialSuccess = 2,

        [Description("Function completed successfully with warnings.")]
        SuccessWithWarnings = 1001,

        [Description("Partial success: Function completed successfully with warnings, but some growing data-object data-nodes were not returned.")]
        PartialSuccessWithWarnings = 1002,

        [Description("The Server does not support the requested WITSML Version.")]
        UnsupportedWitsmlVersion = -401,

        [Description("The Server does not support the requested function.")]
        UnsupportedFunction = -402,

        [Description("The Server does not support the requested data object.")]
        UnsupportedDataObject = -403,

        [Description("The Server does not support the requested capability option.")]
        UnsupportedCapabilityOption = -404,

        [Description("The Server does not support the requested query template.")]
        UnsupportedQueryTemplate = -405,

        [Description("The Server does not support the requested sort order.")]
        UnsupportedSortOrder = -406,

        [Description("The Server does not support the requested growing timeout period.")]
        UnsupportedGrowingTimeoutPeriod = -407,

        [Description("The Server does not support the requested cascaded delete.")]
        UnsupportedCascadedDelete = -408,

        [Description("The Server does not support the requested join set.")]
        UnsupportedJoinSet = -409,

        [Description("The Server does not support the requested compression method.")]
        UnsupportedCompressionMethod = -410,

        [Description("The userid and/or password are not recognized.")]
        InvalidUsernameOrPassword = -426,

        [Description("The user is not authorized to perform the requested function.")]
        NotAuthorized = -427,

        [Description("The input template is not valid.")]
        InvalidTemplate = -440,

        [Description("The input template is missing required elements.")]
        MissingRequiredElements = -441,

        [Description("The input template contains invalid or out-of-order elements.")]
        InvalidElements = -442,

        [Description("The input template contains invalid units.")]
        InvalidUnits = -443,

        [Description("The input template contains invalid values.")]
        InvalidValues = -444,

        [Description("The input template contains invalid object UIDs.")]
        InvalidObjectUids = -445,

        [Description("The input template contains invalid parent UIDs.")]
        InvalidParentUids = -446,

        [Description("The input template contains invalid recurring elements.")]
        InvalidRecurringElements = -447,

        [Description("The input template contains invalid selection criteria.")]
        InvalidSelectionCriteria = -448,

        [Description("The input template contains invalid growing data.")]
        InvalidGrowingData = -449,

        [Description("The input template contains invalid date time values.")]
        InvalidDateTimeValues = -450,

        [Description("The input template contains invalid index values.")]
        InvalidIndexValues = -451,

        [Description("The input template contains invalid unit values.")]
        InvalidUnitValues = -452,

        [Description("The input template contains invalid measure values.")]
        InvalidMeasureValues = -453,

        [Description("The input template contains invalid data.")]
        InvalidData = -454,

        [Description("The input template contains invalid keywords.")]
        InvalidKeywords = -455,

        [Description("The input template contains invalid plural object.")]
        InvalidPluralObject = -456,

        [Description("The input template contains invalid singular object.")]
        InvalidSingularObject = -457,

        [Description("The input template contains invalid empty new line.")]
        InvalidEmptyNewLine = -458,

        [Description("The input template contains invalid XML.")]
        InvalidXml = -459,

        [Description("The input template contains invalid namespace.")]
        InvalidNamespace = -460,

        [Description("The input template contains invalid schema version.")]
        InvalidSchemaVersion = -461,

        [Description("The input template contains invalid options in.")]
        InvalidOptionsIn = -462,

        [Description("The input template contains invalid capabilities in.")]
        InvalidCapabilitiesIn = -463,

        [Description("The input template contains invalid compression.")]
        InvalidCompression = -464,

        [Description("The input template contains invalid request.")]
        InvalidRequest = -465,

        [Description("The input template contains invalid object type.")]
        InvalidObjectType = -466,

        [Description("The input template contains invalid query.")]
        InvalidQuery = -467,

        [Description("The input template contains invalid update.")]
        InvalidUpdate = -468,

        [Description("The input template contains invalid delete.")]
        InvalidDelete = -469,

        [Description("The input template contains invalid maximum return nodes.")]
        InvalidMaxReturnNodes = -470,

        [Description("The input template contains invalid request latest values.")]
        InvalidRequestLatestValues = -471,

        [Description("The input template contains invalid request object selection capability.")]
        InvalidRequestObjectSelectionCapability = -472,

        [Description("The input template contains invalid cascaded delete.")]
        InvalidCascadedDelete = -473,

        [Description("The input template contains invalid active timeout period.")]
        InvalidActiveTimeoutPeriod = -474,

        [Description("The input template contains invalid request private group only.")]
        InvalidRequestPrivateGroupOnly = -475,

        [Description("The input template contains invalid request public group only.")]
        InvalidRequestPublicGroupOnly = -476,

        [Description("The input template contains invalid interval range inclusive.")]
        InvalidIntervalRangeInclusive = -477,

        [Description("The input template contains invalid extra options in.")]
        InvalidExtraOptionsIn = -478,

        [Description("The input template contains invalid data delimiter.")]
        InvalidDataDelimiter = -479,

        [Description("The Server encountered an error in processing the request.")]
        ErrorProcessingRequest = -999
    }

    public static class ErrorCodesExtensions
    {
        public static string GetDescription(this ErrorCodes errorCode)
        {
            var field = errorCode.GetType().GetField(errorCode.ToString());
            if (field != null)
            {
                var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                if (attribute != null)
                    return attribute.Description;
            }
            return errorCode.ToString();
        }
    }
}
