using Strive.Application.UseCases.Users.ResendVerification;
using Strive.Application.UseCases.Users.ResendVerification.Contracts;
using Strive.Tests.UseCases.Users.ResendVerification.Mocks;

namespace Strive.Tests.UseCases.Users.ResendVerification.Handlers;

[TestClass]
public class HandlerTests
{
    private Handler _handler;

    public HandlerTests()
    {
        IRepository repository = new Repository();
        IEmailService emailService = new EmailService();
        _handler = new Handler(repository, emailService);
    }
    
    [TestMethod]
    [TestCategory("ResendVerificationHandler")]
    public async Task ShouldReturnStatusCode200WhenTheCodeIsSent()
    {
        var request = new Request("test@email.com");
        var result = await _handler.HandleAsync(request);
        
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    [TestCategory("ResendVerificationHandler")]
    public async Task ShouldReturnStatusCode404WhenUserIsNotFound()
    {
        var request = new Request("notfound@email.com");
        var result = await _handler.HandleAsync(request);
        
        Assert.AreEqual(404, result.StatusCode);
    }
}