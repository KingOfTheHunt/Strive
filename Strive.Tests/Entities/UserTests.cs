using Flunt.Notifications;
using Strive.Core.Entities;
using Strive.Core.ValueObjects;

namespace Strive.Tests.Entities;

public class UserTests
{
    [Fact]
    public void ShouldReturnTrueWhenUserIsValid()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var user = new User(name, email, password);
        
        Assert.True(user.IsValid);
    }

    [Fact]
    public void ShouldReturnFalseWhenUserNameIsNull()
    {
        var email = new Email("jdoe@gmail.com");
        var password = new Password("Abc123!@");
        var user = new User(null, email, password);
        
        Assert.False(user.IsValid);
        Assert.Equal("O nome precisa ser informado.", GetNotification(user.Notifications));
    }

    [Fact]
    public void ShouldReturnFalseWhenEmailIsNull()
    {
        var name = new Name("John", "Doe");
        var password = new Password("Abc123!@");
        var user = new User(name, null, password);
        
        Assert.False(user.IsValid);
        Assert.Equal("O e-mail precisa ser informado.", GetNotification(user.Notifications));
    }

    [Fact]
    public void ShouldReturnFalseWhenPasswordIsNull()
    {
        var name = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var user = new User(name, email, null);
        
        Assert.False(user.IsValid);
        Assert.Equal("A senha precisa ser informada.", GetNotification(user.Notifications));
    }

    [Fact]
    public void ShouldChangeUserNameWhenUpdated()
    {
        var oldName = new Name("Jonh", "Doe");
        var newName = new Name("John", "Doe");
        var email = new Email("jdoe@email.com");
        var password = new Password("Abc123!@");
        var user = new User(oldName, email, password);
        user.ChangeName(newName);
        
        Assert.Equal(newName.ToString(), user.Name.ToString());
    }

    private string? GetNotification(IReadOnlyCollection<Notification> notifications)
    {
        return notifications.Select(x => x.Message).FirstOrDefault();
    }
}