using Strive.Application.UseCases.Users.Details;
using Strive.Tests.UseCases.Users.Details.Mocks;

namespace Strive.Tests.UseCases.Users.Details.Handlers;

[TestClass]
public class HandleTests
{
    private Handler _handler;

    public HandleTests()
    {
        _handler = new Handler(new Repository());
    }

    [TestMethod]
    [TestCategory("DetailsHandler")]
    public async Task ShouldReturnTheAccountDataWhenIdExists()
    {
        var request = new Request(1);
        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    [TestCategory("DetailsHandler")]
    public async Task ShouldReturnFalseWhenIdIsLowerOrEqualsThanZero()
    {
        var request = new Request(-1);
        var result = await _handler.HandleAsync(request);
        
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Dados inválidos.", result.Message);
    }

    [TestMethod]
    [TestCategory("DetailsHandler")]
    public async Task ShouldReturnFalseWhenIdDoesNotExists()
    {
        var request = new Request(8);
        var result = await _handler.HandleAsync(request);
        
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(404, result.StatusCode);
    }
    
}