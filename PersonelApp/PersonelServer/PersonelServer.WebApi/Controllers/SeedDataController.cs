using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonelServer.Domain.Abstractions;
using PersonelServer.Domain.Users;

namespace PersonelServer.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class SeedDataController(
    IUserRepository repository, 
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CreateUsers(CancellationToken cancellationToken)
    {
        for (int i = 0; i < 1000; i++)
        {
            var faker = new Faker();

            User user = new(
                name: faker.Person.FirstName,
                lastname: faker.Person.LastName,
                email: faker.Person.Email,
                phoneNumber: faker.Person.Phone.Substring(0, 10),
                country: faker.Address.Country(),
                city: faker.Address.City(),
                postalCode: faker.Address.ZipCode(),
                fullAddress: faker.Address.FullAddress());

            await repository.CreateAsync(user, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(await repository.GetAllAsync(cancellationToken));
    }
}
