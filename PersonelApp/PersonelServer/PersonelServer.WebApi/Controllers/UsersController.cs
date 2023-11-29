using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonelServer.Domain.Users;

namespace PersonelServer.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class UsersController(
    IUserRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<User> users = await repository.GetAllAsync(cancellationToken);
        var response = users.Select(s => new UserResponseDto(
            s.Name.Value,
            s.Lastname.Value,
            s.Email.Value,
            s.PhoneNumber.Value,
            s.Address.Country,
            s.Address.City,
            s.Address.PostalCode,
            s.Address.FullAddress));

        return Ok(response);
    }
}
