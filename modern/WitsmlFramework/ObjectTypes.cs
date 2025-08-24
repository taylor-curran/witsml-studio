using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Energistics.DataAccess
{
    public static class ObjectTypes
    {
        public const string CapServer = "capServer";
        public const string Well = "well";
        public const string Wellbore = "wellbore";
        public const string Log = "log";
        public const string Trajectory = "trajectory";
        public const string MudLog = "mudLog";
        public const string CementJob = "cementJob";
        public const string ConvCore = "convCore";
        public const string FluidsReport = "fluidsReport";
        public const string FormationMarker = "formationMarker";
        public const string Message = "message";
        public const string OpsReport = "opsReport";
        public const string Rig = "rig";
        public const string Risk = "risk";
        public const string SidewallCore = "sidewallCore";
        public const string SurveyProgram = "surveyProgram";
        public const string Target = "target";
        public const string TubularComponent = "tubularComponent";
        public const string Tubular = "tubular";
        public const string WbGeometry = "wbGeometry";
        public const string WellLog = "wellLog";
        public const string Attachment = "attachment";
        public const string ChangeLog = "changeLog";
        public const string BhaRun = "bhaRun";
        public const string DrillReport = "drillReport";
        public const string StimJob = "stimJob";
        public const string RealtimeData = "realtimeData";

        private static readonly Dictionary<string, string> _objectTypeMap = new()
        {
            { "wells", Well },
            { "wellbores", Wellbore },
            { "logs", Log },
            { "trajectorys", Trajectory },
            { "mudLogs", MudLog },
            { "cementJobs", CementJob },
            { "convCores", ConvCore },
            { "fluidsReports", FluidsReport },
            { "formationMarkers", FormationMarker },
            { "messages", Message },
            { "opsReports", OpsReport },
            { "rigs", Rig },
            { "risks", Risk },
            { "sidewallCores", SidewallCore },
            { "surveyPrograms", SurveyProgram },
            { "targets", Target },
            { "tubularComponents", TubularComponent },
            { "tubulars", Tubular },
            { "wbGeometrys", WbGeometry },
            { "wellLogs", WellLog },
            { "attachments", Attachment },
            { "changeLogs", ChangeLog },
            { "bhaRuns", BhaRun },
            { "drillReports", DrillReport },
            { "stimJobs", StimJob },
            { "realtimeDatas", RealtimeData }
        };

        public static string GetObjectTypeFromGroup(XElement? element)
        {
            if (element == null) return string.Empty;

            var localName = element.Name.LocalName;
            return _objectTypeMap.TryGetValue(localName, out var objectType) ? objectType : localName;
        }

        public static string GetObjectGroupFromType(string objectType)
        {
            foreach (var kvp in _objectTypeMap)
            {
                if (kvp.Value.Equals(objectType, StringComparison.OrdinalIgnoreCase))
                    return kvp.Key;
            }
            return objectType + "s";
        }
    }
}
