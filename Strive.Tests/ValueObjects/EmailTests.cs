using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class EmailTests
{
    [Fact]
    public void ShouldReturnTrueWhenEmailIsValid()
    {
        var email = new Email("test@email.com");
        
        Assert.True(email.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenEmailIsInvalid()
    {
        var invalidEmail = new Email("test.com");
        
        Assert.False(invalidEmail.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenEmailIsEmpty()
    {
        var emptyEmail = new Email("");
        
        Assert.False(emptyEmail.IsValid);
    }
}