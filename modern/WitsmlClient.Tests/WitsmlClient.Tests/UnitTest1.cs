using NUnit.Framework;
using WitsmlClient.Models;
using WitsmlFramework;

namespace WitsmlClient.Tests;

public class ConnectionTests
{
    [Test]
    public void CanCreateConnection()
    {
        var conn = new Connection { Uri = "http://test.com/witsml" };
        Assert.IsNotNull(conn);
        Assert.AreEqual("http://test.com/witsml", conn.Uri);
    }

    [Test]
    public void CanParseErrorCode()
    {
        var code = ErrorCodesExtensions.Parse("-401");
        Assert.AreEqual(ErrorCodes.MissingPluralRootElement, code);
    }

    [Test]
    public void CanCreateConnectionSettings()
    {
        var settings = new ConnectionSettings 
        { 
            Uri = "https://test.com/witsml",
            Username = "testuser",
            Password = "testpass"
        };
        
        Assert.IsNotNull(settings);
        Assert.AreEqual("https://test.com/witsml", settings.Uri);
        Assert.AreEqual("testuser", settings.Username);
        Assert.AreEqual("1.4.1.1", settings.WitsmlVersion);
    }
}
