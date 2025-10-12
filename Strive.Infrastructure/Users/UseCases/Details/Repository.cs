using Microsoft.EntityFrameworkCore;
using Strive.Application.Users.UseCases.Details;
using Strive.Application.Users.UseCases.Details.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.Users.UseCases.Details;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<ResponseData?> GetUserDataByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Users.Where(x => x.Id == id)
            .AsNoTracking()
            .Select(x => new ResponseData(x.Name.ToString(), x.Email.Address))
            .FirstOrDefaultAsync(cancellationToken);
    }
}