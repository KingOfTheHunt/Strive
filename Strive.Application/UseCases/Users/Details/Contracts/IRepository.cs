namespace Strive.Application.UseCases.Users.Details.Contracts;

public interface IRepository
{
    Task<ResponseData?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
}