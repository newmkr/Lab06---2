using Lab06;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.InProcDataCollector;
namespace StringToolsTest;

// Юнит тесты для класса StringTools

public class UnitTest1
{
    [Theory]
    [InlineData("qweewq", true)]
    [InlineData("Capitalatipac", true)]
    [InlineData("dogeeseseegod", true)]
    [InlineData("22:22", true)]
    [InlineData("20.11.02", true)]
    [InlineData("not a palindrome", false)]
    public void IsAPalindrome_VariousInputs_ReturnsExpected(string value, bool expected)
    {
        bool result = StringTools.IsAPalindrome(value);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void IsAPalindrome_EmptyString_ReturnsEmptyString()
    {
        string input = "";

        bool result = StringTools.IsAPalindrome(input);

        Assert.True(result);
    }

    [Theory]
    [InlineData("01.02.03")]
    [InlineData("01.02.2003")]
    [InlineData("01/02/2003")]
    [InlineData("01 02 2003")]
    public void IsAValidDate_VariousInputs_ReturnsTrue(string value)
    {
        bool result = StringTools.IsAValidDate(value);

        Assert.True(result);
    }

    [Theory]
    [InlineData("Not a date")]
    [InlineData("No.ta.date")]
    [InlineData("No.ta.dt")]
    [InlineData("2short")]
    [InlineData("")]
    [InlineData("Too looonnngggg")]
    [InlineData(" 02.03.2004 ")]
    public void IsAValidDate_VariousInputs_ReturnsFalse(string input)
    {
        bool result = StringTools.IsAValidDate(input);

        Assert.False(result);
    }
}
