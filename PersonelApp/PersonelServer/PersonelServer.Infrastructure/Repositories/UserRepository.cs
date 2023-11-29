using Microsoft.EntityFrameworkCore;
using PersonelServer.Domain.Users;
using PersonelServer.Infrastructure.Context;

namespace PersonelServer.Infrastructure.Repositories;
internal sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Set<User>().AddAsync(user, cancellationToken);
    }

    public Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Set<User>().ToListAsync(cancellationToken);
    }
}
