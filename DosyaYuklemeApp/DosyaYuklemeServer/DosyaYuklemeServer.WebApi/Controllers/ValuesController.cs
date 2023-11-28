using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace DosyaYuklemeServer.WebApi.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpPost]
    public IActionResult Save(List<IFormFile> files)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "upload",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        foreach (var item in files)
        {
            var body = FileService.FileConvertByteArrayToDatabase(item);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: "upload",
                basicProperties: null,
                body: body);
        }
        return NoContent();
    }
}
