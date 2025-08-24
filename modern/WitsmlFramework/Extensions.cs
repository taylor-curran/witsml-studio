using System;
using System.Globalization;
using System.Xml.Linq;

namespace Energistics.DataAccess
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            if (input.Length == 1)
                return input.ToLower();

            return char.ToLower(input[0]) + input.Substring(1);
        }

        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            if (input.Length == 1)
                return input.ToUpper();

            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }

    public static class DateTimeExtensions
    {
        public static string ToWitsmlDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        }

        public static string ToWitsmlDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        public static DateTime? FromWitsmlDateTime(this string? dateTimeString)
        {
            if (string.IsNullOrWhiteSpace(dateTimeString))
                return null;

            if (DateTime.TryParse(dateTimeString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result))
                return result;

            return null;
        }
    }

    public static class XElementExtensions
    {
        public static string? GetAttributeValue(this XElement element, string attributeName)
        {
            return element.Attribute(attributeName)?.Value;
        }

        public static string? GetElementValue(this XElement element, string elementName)
        {
            return element.Element(elementName)?.Value;
        }

        public static T? GetAttributeValue<T>(this XElement element, string attributeName) where T : struct
        {
            var value = element.GetAttributeValue(attributeName);
            if (string.IsNullOrWhiteSpace(value))
                return null;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static T? GetElementValue<T>(this XElement element, string elementName) where T : struct
        {
            var value = element.GetElementValue(elementName);
            if (string.IsNullOrWhiteSpace(value))
                return null;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}
