using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldReturnTrueWhenPasswordIsValid()
    {
        var password = new Password("Abc123!@");
        
        Assert.True(password.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenPasswordIsEmpty()
    {
        var emptyPassword = new Password("");
        
        Assert.False(emptyPassword.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenPasswordDoesNotSatisfiesPolicy()
    {
        var invalidPassword = new Password("12345678");
        
        Assert.False(invalidPassword.IsValid);
    }

    [Fact]
    public void ShouldReturnTrueWhenPasswordIsCorrect()
    {
        var password = new Password("Abc123!@");
        var correctPassword = password.Challenge("Abc123!@");
        
        Assert.True(correctPassword);
    }
}