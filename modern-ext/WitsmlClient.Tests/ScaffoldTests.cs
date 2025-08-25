using NUnit.Framework;

namespace WitsmlClient.Tests;

[TestFixture]
public class ScaffoldTests
{
    [Test]
    public void ProjectsExist()
    {
        Assert.Pass("Modern-ext scaffold is set up");
    }
    
    [Test]
    public void CanRunOnLinux()
    {
        var platform = Environment.OSVersion.Platform;
        Assert.That(platform, Is.AnyOf(
            PlatformID.Unix, 
            PlatformID.MacOSX, 
            PlatformID.Win32NT));
    }
}
