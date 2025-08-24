using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Energistics.DataAccess
{
    public static class WitsmlParser
    {
        public static XDocument Parse(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException("XML content cannot be null or empty", nameof(xml));

            try
            {
                return XDocument.Parse(xml, LoadOptions.PreserveWhitespace);
            }
            catch (XmlException ex)
            {
                throw new WitsmlException(ErrorCodes.InvalidXml, $"Invalid XML: {ex.Message}", ex);
            }
        }

        public static XDocument Parse(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            try
            {
                return XDocument.Load(stream, LoadOptions.PreserveWhitespace);
            }
            catch (XmlException ex)
            {
                throw new WitsmlException(ErrorCodes.InvalidXml, $"Invalid XML: {ex.Message}", ex);
            }
        }

        public static string Format(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return xml;

            try
            {
                var doc = Parse(xml);
                return doc.ToString();
            }
            catch
            {
                return xml;
            }
        }

        public static bool IsValidXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return false;

            try
            {
                Parse(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
