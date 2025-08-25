using System;
using System.Globalization;

namespace WitsmlFramework;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }
    
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;
            
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
    }
    
    public static bool EqualsIgnoreCase(this string value, string other)
    {
        return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
    }
    
    public static string ToCamelCase(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;
            
        if (value.Length == 1)
            return value.ToLower();
            
        return char.ToLower(value[0]) + value.Substring(1);
    }
}
