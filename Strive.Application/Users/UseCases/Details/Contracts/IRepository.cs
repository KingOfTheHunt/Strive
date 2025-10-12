namespace Strive.Application.Users.UseCases.Details.Contracts;

public interface IRepository
{
    Task<ResponseData?> GetUserDataByIdAsync(int id, CancellationToken cancellationToken);
}