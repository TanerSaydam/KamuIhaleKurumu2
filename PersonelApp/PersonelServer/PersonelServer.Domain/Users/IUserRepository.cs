namespace PersonelServer.Domain.Users;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
}