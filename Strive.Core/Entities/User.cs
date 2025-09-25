using Flunt.Validations;
using Strive.Core.Abstractions;
using Strive.Core.ValueObjects;

namespace Strive.Core.Entities;

public class User : Entity
{
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;

    protected User() {}
    
    public User(Name name, Email email, Password password)
    {
        AddNotifications(new Contract<User>()
            .Requires()
            .IsNotNull(name, nameof(name), "O nome precisa ser informado")
            .IsNotNull(email, nameof(email), "O e-mail precisa ser informado.")
            .IsNotNull(password, nameof(password), "A senha precisa ser informada."));

        if (!IsValid)
            return;
        
        // Agrupa as notificações dos value object que compõe o 'User'.
        AddNotifications(name, email, password);

        if (!IsValid)
            return;
        
        Name = name;
        Email = email;
        Password = password;
    }
}