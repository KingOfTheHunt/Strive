using Strive.Core.ValueObjects;

namespace Strive.Tests.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnTrueWhenEmailIsValid()
    {
        var email = new Email("johndoe@email.com");
        Assert.IsTrue(email.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenEmailIsInvalid()
    {
        var email = new Email("johndoe");
        Assert.IsFalse(email.IsValid);
    }

    [TestMethod]
    [TestCategory("ValueObjects")]
    public void ShouldReturnFalseWhenEmailIsEmpty()
    {
        var email = new Email("");
        Assert.IsFalse(email.IsValid);
    }
}