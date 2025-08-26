using Strive.Application.UseCases.Users.Create;
using Strive.Application.UseCases.Users.Create.Contracts;
using Strive.Tests.UseCases.Users.Create.Mocks;

namespace Strive.Tests.UseCases.Users.Create.Handlers;

[TestClass]
public class HandlerTests
{
    private readonly Handler _handler;

    public HandlerTests()
    {
        IRepository repository = new Repository();
        IEmailService emailService = new EmailService();
        _handler = new Handler(repository, emailService);
    }
    
    [TestMethod]
    [TestCategory("UsersHandler")]
    public async Task ShouldReturnStatusCode201WhenUserIsCreated()
    {
        var request = new Request("Davi", "Santos", "davi@gmail.com", "123456789");
        var result = await _handler.HandleAsync(request);
        Assert.AreEqual(201, result.StatusCode);
    }

    [TestMethod]
    [TestCategory("UsersHandler")]
    public async Task ShouldNotCreateAccountWhenTheEmailAlreadyExists()
    {
        var request = new Request("Davi", "Santos", "test@email.com", "123456789");
        var result = await _handler.HandleAsync(request);
        Assert.AreEqual("O e-mail já está cadastrado em nossa base de dados.", result.Message);
    }
}