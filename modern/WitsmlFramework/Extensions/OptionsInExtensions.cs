using System;
using System.Collections.Generic;
using System.Linq;

namespace Energistics.DataAccess
{
    public static class OptionsInExtensions
    {
        public static IEnumerable<KeyValuePair<OptionsIn.ReturnElements, string>> GetValues(this Type returnElementsType)
        {
            return new[]
            {
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.All, "All"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.IdOnly, "Id Only"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.HeaderOnly, "Header Only"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.DataOnly, "Data Only"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.StationLocationOnly, "Station Location Only"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.LatestChangeOnly, "Latest Change Only"),
                new KeyValuePair<OptionsIn.ReturnElements, string>(OptionsIn.ReturnElements.Requested, "Requested")
            };
        }
    }
    
    public static class OptionsInLegacy
    {
        public enum ReturnElements
        {
            All,
            IdOnly,
            HeaderOnly,
            DataOnly,
            StationLocationOnly,
            LatestChangeOnly,
            Requested
        }
        
        public static string RequestObjectSelectionCapability { get; set; } = "true";
        public static string RequestPrivateGroupOnly { get; set; } = "false";
    }
    
    public partial class OptionsIn
    {
        public static string RequestObjectSelectionCapability { get; set; } = "true";
        public static string RequestPrivateGroupOnly { get; set; } = "false";
    }
}
