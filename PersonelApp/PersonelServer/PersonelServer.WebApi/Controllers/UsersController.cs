using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PersonelServer.Domain.Abstractions;
using PersonelServer.Domain.Users;
using RabbitMQ.Client;
using System.Text;

namespace PersonelServer.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class UsersController(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<User> users = await repository.GetAllAsync(cancellationToken);
        var response = users.Select(s => new UserResponseDto(
            s.Id,
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

    [HttpPost]
    public async Task<IActionResult> RemoveById(RemoveUserDto request, CancellationToken cancellationToken)
    {
        User? user = await repository.GetByIdAsync(request.Id,cancellationToken).ConfigureAwait(false);
        if (user is null)
        {
            return BadRequest(new { Message = "Kullanıcı bulunamadı!" });
        }

        repository.Remove(user);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return NoContent();
    }

    [HttpPost]
    public IActionResult SendEmailSelectedUsers(List<string> ids)
    {
        var factory = new ConnectionFactory();
        factory.HostName = "localhost";

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "email",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);


        foreach (var id in ids)
        {
            var body = Encoding.UTF8.GetBytes(id);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: "email",
                basicProperties: null,
                body: body);
        }       

        return NoContent();
    }
}
