using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Users.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.FirstName, "firstName",
                "O primeiro nome precisa ser informado.")
            .IsNotNullOrEmpty(request.LastName, "lastName",
                "O último nome deve ser informado.")
            .IsEmail(request.Email, "email", "Informe um e-mail válido.")
            .IsNotNullOrEmpty(request.Password, "password",
                "A senha precisa ser informada.")
            .IsGreaterOrEqualsThan(request.FirstName.Length, 3, "firstName",
                "O primeiro nome precisa ter ao menos 3 letras.")
            .IsLowerOrEqualsThan(request.FirstName.Length, 20, "firstName",
                "O primeiro nome só pode ter no máximo 20 letras.")
            .IsGreaterOrEqualsThan(request.LastName.Length, 3, "lastName",
                "O último nome precisa ter ao menos 3 letras.")
            .IsLowerOrEqualsThan(request.LastName.Length, 30, "lastName",
                "O último nome só pode ter no máximo 30 letras.")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "password",
                "A senha precisa ter no mínimo 8 caractres.")
            .IsLowerOrEqualsThan(request.Password.Length, 20, "password",
                "A senha só pode ter no máximo 20 caracteres.");
}