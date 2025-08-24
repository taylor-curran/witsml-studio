using System;

namespace WitsmlClient.Models;

public class ConnectionSettings
{
    public required string Uri { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool EnableCompression { get; set; }
    public bool AcceptInvalidCertificates { get; set; }
    public string WitsmlVersion { get; set; } = "1.4.1.1";
    public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(5);
    public string? ProxyHost { get; set; }
    public int? ProxyPort { get; set; }
    public string? ProxyUsername { get; set; }
    public string? ProxyPassword { get; set; }
}
