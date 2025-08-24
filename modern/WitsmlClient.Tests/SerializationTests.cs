using System;
using Xunit;
using Energistics.DataAccess;

namespace WitsmlClient.Tests;

public class SerializationTests
{
    [Fact]
    public void WitsmlParser_ValidXml_ParsesSuccessfully()
    {
        var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                   <wells xmlns=""http://www.witsml.org/schemas/1series"">
                     <well uid=""well-001"">
                       <name>Test Well</name>
                     </well>
                   </wells>";

        var doc = WitsmlParser.Parse(xml);
        
        Assert.NotNull(doc);
        Assert.NotNull(doc.Root);
        Assert.Equal("wells", doc.Root.Name.LocalName);
    }

    [Fact]
    public void WitsmlParser_InvalidXml_ThrowsWitsmlException()
    {
        var invalidXml = "<wells><well><name>Test Well</well></wells>";

        var exception = Assert.Throws<WitsmlException>(() => WitsmlParser.Parse(invalidXml));
        Assert.Equal(ErrorCodes.InvalidXml, exception.ErrorCode);
    }

    [Fact]
    public void WitsmlParser_NullOrEmpty_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => WitsmlParser.Parse(string.Empty));
        Assert.Throws<ArgumentException>(() => WitsmlParser.Parse((string)null!));
    }

    [Fact]
    public void ObjectTypes_GetObjectTypeFromGroup_ReturnsCorrectType()
    {
        var xml = @"<wells xmlns=""http://www.witsml.org/schemas/1series""></wells>";
        var doc = WitsmlParser.Parse(xml);
        
        var objectType = ObjectTypes.GetObjectTypeFromGroup(doc.Root);
        
        Assert.Equal("well", objectType);
    }

    [Fact]
    public void OptionsIn_Parse_HandlesValidString()
    {
        var optionsIn = "returnElements=all;maxReturnNodes=100";
        
        var options = OptionsIn.Parse(optionsIn);
        
        Assert.Equal(2, options.Count);
        Assert.Equal("all", options["returnElements"]);
        Assert.Equal("100", options["maxReturnNodes"]);
    }

    [Fact]
    public void OptionsIn_Join_CombinesOptions()
    {
        var option1 = new OptionsIn.ReturnElements("all");
        var option2 = new OptionsIn.MaxReturnNodes(100);
        
        var combined = OptionsIn.Join(option1, option2);
        
        Assert.Equal("returnElements=all;maxReturnNodes=100", combined);
    }

    [Fact]
    public void ErrorCodes_GetDescription_ReturnsCorrectDescription()
    {
        var description = ErrorCodes.Success.GetDescription();
        
        Assert.Equal("Function completed successfully.", description);
    }
}
