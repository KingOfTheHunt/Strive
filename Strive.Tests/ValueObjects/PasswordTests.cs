using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenPasswordIsValid()
    {
        var password = new Password("123456789");
        Assert.IsTrue(password.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenPasswordIsLowerThan6Chars()
    {
        var password = new Password("12345");
        Assert.IsFalse(password.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenPasswordIsEmpty()
    {
        var password = new Password("");
        Assert.IsFalse(password.IsValid);
    }
}