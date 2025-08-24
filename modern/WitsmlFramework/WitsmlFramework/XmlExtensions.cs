using System;
using System.Xml;
using System.Xml.Linq;

namespace WitsmlFramework;

public static class XmlExtensions
{
    public static string GetAttributeValue(this XElement element, string attributeName)
    {
        return element?.Attribute(attributeName)?.Value;
    }
    
    public static string GetElementValue(this XElement element, string elementName)
    {
        return element?.Element(elementName)?.Value;
    }
    
    public static bool HasAttribute(this XElement element, string attributeName)
    {
        return element?.Attribute(attributeName) != null;
    }
    
    public static bool HasElement(this XElement element, string elementName)
    {
        return element?.Element(elementName) != null;
    }
    
    public static XElement GetOrCreateElement(this XElement parent, string elementName)
    {
        var element = parent.Element(elementName);
        if (element == null)
        {
            element = new XElement(elementName);
            parent.Add(element);
        }
        return element;
    }
    
    public static void SetAttributeValue(this XElement element, string attributeName, string value)
    {
        if (element != null)
        {
            element.SetAttributeValue(attributeName, value);
        }
    }
}
