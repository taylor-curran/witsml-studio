using NUnit.Framework;
using WitsmlFramework;
using System;

namespace WitsmlClient.Tests;

public class FrameworkTests
{
    [Test]
    public void ErrorCodesParseWorksCorrectly()
    {
        var successCode = ErrorCodesExtensions.Parse("1");
        Assert.AreEqual(ErrorCodes.Success, successCode);

        var errorCode = ErrorCodesExtensions.Parse("-401");
        Assert.AreEqual(ErrorCodes.MissingPluralRootElement, errorCode);

        var invalidCode = ErrorCodesExtensions.Parse("invalid");
        Assert.AreEqual(ErrorCodes.Unset, invalidCode);
    }

    [Test]
    public void ReturnCodesWorkCorrectly()
    {
        Assert.IsTrue(ReturnCodes.IsSuccess(1));
        Assert.IsTrue(ReturnCodes.IsSuccess(2));
        Assert.IsTrue(ReturnCodes.IsSuccess(1001));
        Assert.IsTrue(ReturnCodes.IsSuccess(1002));
        
        Assert.IsTrue(ReturnCodes.IsError(-401));
        Assert.IsFalse(ReturnCodes.IsError(1));
        
        Assert.IsTrue(ReturnCodes.HasWarnings(1001));
        Assert.IsFalse(ReturnCodes.HasWarnings(1));
    }

    [Test]
    public void WMLSVersionWorksCorrectly()
    {
        Assert.AreEqual("1.4.1.1", WMLSVersion.GetLatest());
        
        Assert.IsTrue(WMLSVersion.IsSupported("1.4.1"));
        Assert.IsTrue(WMLSVersion.IsSupported("1.4.1.1"));
        Assert.IsFalse(WMLSVersion.IsSupported("1.0.0"));
    }

    [Test]
    public void DateTimeExtensionsWorkCorrectly()
    {
        var now = DateTime.Now;
        var truncated = now.TruncateToSeconds();
        
        Assert.AreEqual(0, truncated.Millisecond);
        Assert.AreEqual(now.Year, truncated.Year);
        Assert.AreEqual(now.Month, truncated.Month);
        Assert.AreEqual(now.Day, truncated.Day);
        Assert.AreEqual(now.Hour, truncated.Hour);
        Assert.AreEqual(now.Minute, truncated.Minute);
        Assert.AreEqual(now.Second, truncated.Second);
    }

    [Test]
    public void StringExtensionsWorkCorrectly()
    {
        Assert.IsTrue("".IsNullOrEmpty());
        Assert.IsTrue(((string)null).IsNullOrEmpty());
        Assert.IsFalse("test".IsNullOrEmpty());
        
        Assert.IsTrue("test".EqualsIgnoreCase("TEST"));
        Assert.IsFalse("test".EqualsIgnoreCase("other"));
        
        Assert.AreEqual("testValue", "TestValue".ToCamelCase());
        Assert.AreEqual("t", "T".ToCamelCase());
    }
}
