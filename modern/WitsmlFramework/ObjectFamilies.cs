using System;
using System.Collections.Generic;

namespace Energistics.DataAccess
{
    public static class ObjectFamilies
    {
        public static string Witsml { get; } = "Witsml";
        
        public static Dictionary<string, string[]> GetFamilies()
        {
            return new Dictionary<string, string[]>
            {
                { "Well", new[] { "well" } },
                { "Wellbore", new[] { "wellbore" } },
                { "Log", new[] { "log", "trajectory", "mudLog" } },
                { "Data", new[] { "log", "trajectory", "mudLog", "cementJob", "stimJob" } }
            };
        }
    }
}
