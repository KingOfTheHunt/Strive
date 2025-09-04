using Strive.Application.UseCases.Users.Authenticate;
using Strive.Tests.UseCases.Users.Authenticate.Mocks;

namespace Strive.Tests.UseCases.Users.Authenticate.Handlers;

[TestClass]
public class HandlerTests
{
    private Handler _handler;

    public HandlerTests()
    {
        _handler = new Handler(new Repository());
    }

    [TestMethod]
    [TestCategory("AuthenticateHandler")]
    public async Task ShouldReturnTrueWhenEmailAndPasswordAreCorrect()
    {
        var request = new Request("test@email.com", "123456789");
        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    [TestCategory("AuthenticateHandler")]
    public async Task ShouldReturnFalseWhenEmailDoesNotExists()
    {
        var request = new Request("invalid@email.com", "123456789");
        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Não foi encontrado nenhum usuário com este e-mail.", result.Message);
    }

    [TestMethod]
    [TestCategory("AuthenticateHandler")]
    public async Task ShouldReturnFalseWhenPasswordIsIncorrect()
    {
        var request = new Request("test@email.com", "987654321");
        var result = await _handler.HandleAsync(request, CancellationToken.None);
        
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Senha inválida.", result.Message);
    }
}