using System;
using System.Net;

namespace WitsmlFramework;

public static class ConnectionExtensions
{
    public static bool IsSecure(this Connection connection)
    {
        return connection.Uri?.StartsWith("https", StringComparison.OrdinalIgnoreCase) == true;
    }

    public static void ConfigureSecurityProtocol(this Connection connection)
    {
        ServicePointManager.SecurityProtocol = connection.SecurityProtocol;
    }

    public static TimeSpan GetTimeout(this Connection connection)
    {
        return TimeSpan.FromMinutes(5);
    }

    public static bool HasCredentials(this Connection connection)
    {
        return !string.IsNullOrEmpty(connection.Username) && !string.IsNullOrEmpty(connection.Password);
    }
}
