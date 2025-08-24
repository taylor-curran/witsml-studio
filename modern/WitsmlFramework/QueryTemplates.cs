using System;
using System.Collections.Generic;

namespace Energistics.DataAccess
{
    public static class QueryTemplates
    {
        public static Dictionary<string, string> GetTemplates()
        {
            return new Dictionary<string, string>
            {
                { "well", "<wells xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><well uid=\"\"><name/></well></wells>" },
                { "wellbore", "<wellbores xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><wellbore uid=\"\" uidWell=\"\"><name/></wellbore></wellbores>" },
                { "log", "<logs xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><log uid=\"\" uidWell=\"\" uidWellbore=\"\"><name/></log></logs>" }
            };
        }
        
        public static string GetTemplate(string objectType, string family, string version, string dataSchemaVersion)
        {
            var templates = GetTemplates();
            return templates.ContainsKey(objectType) ? templates[objectType] : "";
        }
    }
}
