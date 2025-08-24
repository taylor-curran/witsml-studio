using System.Threading.Tasks;
using Xunit;
using WitsmlClient;
using WitsmlClient.Models;

namespace WitsmlClient.Tests;

public class ConnectionTests
{
    [Fact]
    public void ConnectionSettings_RequiredProperties_CanBeSet()
    {
        var settings = new ConnectionSettings { Uri = "http://test.example.com" };
        Assert.Equal("http://test.example.com", settings.Uri);
    }

    [Fact]
    public void ConnectionSettings_DefaultValues_AreSet()
    {
        var settings = new ConnectionSettings { Uri = "http://test.example.com" };
        
        Assert.Equal("1.4.1.1", settings.WitsmlVersion);
        Assert.Equal(System.TimeSpan.FromMinutes(5), settings.Timeout);
        Assert.False(settings.EnableCompression);
        Assert.False(settings.AcceptInvalidCertificates);
    }

    [Fact]
    public void WitsmlConnection_Properties_ReflectSettings()
    {
        var settings = new ConnectionSettings 
        { 
            Uri = "http://test.example.com",
            Username = "testuser",
            WitsmlVersion = "1.4.1.1",
            EnableCompression = true
        };

        using var connection = new WitsmlConnection(settings);
        
        Assert.Equal("http://test.example.com", connection.Uri);
        Assert.Equal("testuser", connection.Username);
        Assert.Equal("1.4.1.1", connection.WitsmlVersion);
        Assert.True(connection.EnableCompression);
    }

    [Fact]
    public void WitsmlServiceClient_Constructor_RequiresSettings()
    {
        Assert.Throws<System.ArgumentNullException>(() => new WitsmlServiceClient(null!));
    }

    [Fact]
    public void WitsmlServiceClient_Constructor_ValidSettings_DoesNotThrow()
    {
        var settings = new ConnectionSettings { Uri = "http://test.example.com" };
        
        using var client = new WitsmlServiceClient(settings);
        
        Assert.NotNull(client);
    }
}
