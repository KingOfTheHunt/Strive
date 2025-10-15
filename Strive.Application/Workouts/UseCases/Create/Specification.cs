using Flunt.Notifications;
using Flunt.Validations;

namespace Strive.Application.Workouts.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.Name, "name", "Informe um nome para o treino.")
            .IsGreaterOrEqualsThan(request.Name.Length, 3, "name",
                "O nome do treino precisa ter no mínimo 3 letras.")
            .IsLowerOrEqualsThan(request.Name.Length, 30, "name",
                "O nome do treino só pode ter no máximo 30 letras.")
            .IsGreaterThan(request.UserId, 0, "userId", "O Id do usuário não é válido.");
}