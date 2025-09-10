using Strive.Application.UseCases.Users.Details;
using Strive.Application.UseCases.Users.Details.Contracts;

namespace Strive.Tests.UseCases.Users.Details.Mocks;

public class Repository : IRepository
{
    public async Task<ResponseData?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var responseData = new ResponseData("John Doe", "johndoe@email.com");

        if (id == 1)
            return responseData;

        return null;
    }
}