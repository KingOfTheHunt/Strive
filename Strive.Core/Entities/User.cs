using Flunt.Validations;
using Strive.Core.Abstractions;
using Strive.Core.ValueObjects;

namespace Strive.Core.Entities;

public class User : Entity
{
    public Name Name { get; set; } = null!;
    public Email Email { get; set; } = null!;
    public Password Password { get; set; } = null!;

    protected User() {}
    
    public User(Name name, Email email, Password password)
    {
        AddNotifications(new Contract<User>()
            .Requires()
            .IsNotNull(name, "Name", "O usuário do usuário deve ser informado.")
            .IsNotNull(email, "Email", "O e-mail do usuário deve ser informado.")
            .IsNotNull(password, "Password", "A senha deve ser informada."));
        
        if (IsValid)
            AddNotifications(name, email, password);

        if (!IsValid)
            return;

        Name = name;
        Email = email;
        Password = password;
    }

    public void UpdatePassword(string newPassword) => 
        Password = new Password(newPassword);

    public void ResetPassword(string resetCode, string newPassword)
    {
        if (string.Equals(Password.ResetCode, resetCode.Trim(), StringComparison.CurrentCultureIgnoreCase) == false)
        {
            AddNotification("ResetCode", "Código de restauração de senha inválido.");
            return;
        }

        Password = new Password(newPassword);
    }
}