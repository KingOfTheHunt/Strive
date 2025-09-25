using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

public class NameTests
{
    [Fact]
    public void ShouldReturnTrueWhenNameIsValid()
    {
        var name = new Name("John", "Doe");
        
        Assert.True(name.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenFirstNameIsEmpty()
    {
        var name = new Name("", "Doe");
        
        Assert.False(name.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenLastNameIsEmpty()
    {
        var name = new Name("John", "");
        
        Assert.False(name.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenFirstNameAndLastNameAreEmpty()
    {
        var name = new Name("", "");
        
        Assert.False(name.IsValid);
    }
}