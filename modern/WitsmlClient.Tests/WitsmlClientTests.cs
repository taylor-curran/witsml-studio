using NUnit.Framework;
using Moq;
using WitsmlClient.Models;
using System.Threading.Tasks;

namespace WitsmlClient.Tests;

public class WitsmlClientTests
{
    private Mock<IWitsmlClient> _mockClient;
    private ConnectionSettings _settings;

    [SetUp]
    public void Setup()
    {
        _mockClient = new Mock<IWitsmlClient>();
        _settings = new ConnectionSettings 
        { 
            Uri = "https://test.witsml.com/Store/WMLS",
            Username = "testuser",
            Password = "testpass"
        };
    }

    [Test]
    public async Task CanCallGetCapabilities()
    {
        _mockClient.Setup(x => x.GetCapabilitiesAsync("1.4.1.1"))
                  .ReturnsAsync("<capServers>test capabilities</capServers>");

        var result = await _mockClient.Object.GetCapabilitiesAsync("1.4.1.1");
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contains("capServers"));
    }

    [Test]
    public async Task CanCallGetFromStore()
    {
        var query = "<wells xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><well uid=\"\"/></wells>";
        var options = "returnElements=all";
        var capabilities = "";

        _mockClient.Setup(x => x.GetFromStoreAsync(query, options, capabilities))
                  .ReturnsAsync("<wells>test data</wells>");

        var result = await _mockClient.Object.GetFromStoreAsync(query, options, capabilities);
        
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Contains("wells"));
    }

    [Test]
    public async Task CanCallAddToStore()
    {
        var xml = "<wells xmlns=\"http://www.witsml.org/schemas/1series\" version=\"1.4.1.1\"><well uid=\"test-123\"><name>Test Well</name></well></wells>";
        var options = "";
        var capabilities = "";

        _mockClient.Setup(x => x.AddToStoreAsync(xml, options, capabilities))
                  .ReturnsAsync("Success");

        var result = await _mockClient.Object.AddToStoreAsync(xml, options, capabilities);
        
        Assert.IsNotNull(result);
        Assert.AreEqual("Success", result);
    }
}
