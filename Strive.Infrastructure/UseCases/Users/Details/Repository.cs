using Microsoft.EntityFrameworkCore;
using Strive.Application.UseCases.Users.Details;
using Strive.Application.UseCases.Users.Details.Contracts;
using Strive.Infrastructure.Data;

namespace Strive.Infrastructure.UseCases.Users.Details;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<ResponseData?> GetUserByIdAsync(int id, CancellationToken cancellationToken) =>
        await context.Users.Where(x => x.Id == id)
            .Select(x => new ResponseData(x.Name.ToString(), x.Email.Address))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
}