using System;
using System.Collections.Generic;
using System.Linq;

namespace Energistics.DataAccess
{
    public partial class OptionsIn
    {
        public string Key { get; }
        public string Value { get; }

        public OptionsIn(string key, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString() => $"{Key}={Value}";

        public static implicit operator string(OptionsIn optionsIn) => optionsIn?.ToString() ?? string.Empty;

        public static string Join(params OptionsIn[] options)
        {
            return string.Join(";", options.Where(o => o != null).Select(o => o.ToString()));
        }

        public static Dictionary<string, string> Parse(string? optionsIn)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(optionsIn)) return result;

            var pairs = optionsIn.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                var parts = pair.Split('=', 2);
                if (parts.Length == 2)
                {
                    result[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return result;
        }

        public static string? GetValue(Dictionary<string, string> options, OptionsIn option)
        {
            return options.TryGetValue(option.Key, out var value) ? value : null;
        }

        public class DataVersion : OptionsIn
        {
            public DataVersion(string value) : base(Keyword, value) { }

            public const string Keyword = "dataVersion";

            public static readonly DataVersion Version131 = new("1.3.1.1");
            public static readonly DataVersion Version141 = new("1.4.1.1");
            public static readonly DataVersion Version200 = new("2.0.0.0");
            public static readonly DataVersion Version210 = new("2.1.0.0");
        }

        public class ReturnElements : OptionsIn
        {
            public ReturnElements(string value) : base(Keyword, value) { }

            public const string Keyword = "returnElements";

            public static readonly ReturnElements All = new("all");
            public static readonly ReturnElements IdOnly = new("id-only");
            public static readonly ReturnElements HeaderOnly = new("header-only");
            public static readonly ReturnElements DataOnly = new("data-only");
            public static readonly ReturnElements StationLocationOnly = new("station-location-only");
            public static readonly ReturnElements LatestChangeOnly = new("latest-change-only");
            public static readonly ReturnElements Requested = new("requested");
            
            public static string[] GetValues()
            {
                return new[] { "all", "id-only", "header-only", "data-only", "station-location-only", "latest-change-only", "requested" };
            }
        }

        public class MaxReturnNodes : OptionsIn
        {
            public MaxReturnNodes(int value) : base(Keyword, value.ToString()) { }

            public const string Keyword = "maxReturnNodes";
        }

        public class RequestLatestValues : OptionsIn
        {
            public RequestLatestValues(int value) : base(Keyword, value.ToString()) { }

            public const string Keyword = "requestLatestValues";
        }

        public class CompressionMethod : OptionsIn
        {
            public CompressionMethod(string value) : base(Keyword, value) { }

            public const string Keyword = "compressionMethod";

            public static readonly CompressionMethod Gzip = new("gzip");
        }

        public class CascadedDelete : OptionsIn
        {
            public CascadedDelete(string value) : base(Keyword, value) { }

            public const string Keyword = "cascadedDelete";

            public static readonly CascadedDelete True = new("true");
            public static readonly CascadedDelete False = new("false");
        }
    }
}
