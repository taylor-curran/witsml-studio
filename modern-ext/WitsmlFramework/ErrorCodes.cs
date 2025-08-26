namespace WitsmlFramework;

public class ErrorCodes
{
    public const int Success = 0;
    public const int GeneralError = -1;
    public const int InvalidRequest = -401;
    public const int NotSupported = -402;
    public const int InvalidObject = -403;
    public const int InvalidQuery = -404;
    public const int InvalidOptions = -405;
    public const int InvalidCapabilities = -406;
    public const int InvalidVersion = -407;
    public const int InvalidUid = -408;
    public const int InvalidParentUid = -409;
    public const int InvalidWellUid = -410;
    public const int InvalidWellboreUid = -411;
    public const int InvalidLogUid = -412;
    public const int InvalidTrajectoryUid = -413;
    public const int InvalidRigUid = -414;
    public const int InvalidMessageUid = -415;
    public const int InvalidRiskUid = -416;
    public const int InvalidWbGeometryUid = -417;
    public const int InvalidFormationMarkerUid = -418;
    public const int InvalidMudLogUid = -419;
    public const int InvalidCementJobUid = -420;
    public const int InvalidConvCoreUid = -421;
    public const int InvalidSidewallCoreUid = -422;
    public const int InvalidFluidsReportUid = -423;
    public const int InvalidBhaRunUid = -424;
    public const int InvalidTubularUid = -425;
    public const int InvalidTargetUid = -426;
    public const int InvalidRealtimeUid = -427;
    public const int InvalidSurveyProgramUid = -428;
    public const int InvalidAttachmentUid = -429;
    public const int InvalidChangeLogUid = -430;
    public const int InvalidObjectGroupUid = -431;
    public const int InvalidStimJobUid = -432;
    public const int InvalidDrillReportUid = -433;
    public const int InvalidWellCMLedgerUid = -434;
    public const int InvalidWellCompletionUid = -435;
    public const int InvalidOpsReportUid = -436;
    public const int InvalidToolErrorModelUid = -437;
    public const int InvalidToolErrorTermSetUid = -438;
    public const int InvalidDtsInstalledSystemUid = -439;
    public const int InvalidDtsMeasurementUid = -440;
    public const int ParialSuccess = 1;
    
    public int Value { get; set; }
    
    public ErrorCodes(int value)
    {
        Value = value;
    }
    
    public ErrorCodes(short value)
    {
        Value = value;
    }
    
    public static implicit operator ErrorCodes(short value)
    {
        return new ErrorCodes(value);
    }
    
    public static implicit operator ErrorCodes(int value)
    {
        return new ErrorCodes(value);
    }
    
    public static implicit operator int(ErrorCodes errorCode)
    {
        return errorCode.Value;
    }
    
    public string GetDescription()
    {
        return Value.ToString();
    }
}
