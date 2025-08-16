using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

[TestClass]
public class NameTests
{
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenNameIsValid()
    {
        var name = new Name("John", "Doe");
        Assert.IsTrue(name.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenFirstNameIsEmpty()
    {
        var name = new Name("", "Doe");
        Assert.IsFalse(name.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenLastNameIsEmpty()
    {
        var name = new Name("John", "");
        Assert.IsFalse(name.IsValid);
    }
}