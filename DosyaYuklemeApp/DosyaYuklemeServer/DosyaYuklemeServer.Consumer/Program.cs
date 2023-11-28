using GenericFileService.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DosyaYuklemeServer.Consumer;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "upload",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine("Kuyruğu dinlemeye başladım...");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            IFormFile file = 
                ConvertByteArrayToIFormFile(
                    body,Guid.NewGuid().ToString() + ".png", 
                    "application/png");
            string fileName = FileService
            .FileSaveToServer(file, "C:/KamuIhaleKurumu2/DosyaYuklemeApp/DosyaYuklemeClient/src/assets/");
        };

        channel.BasicConsume(queue: "upload", autoAck: true, consumer: consumer);


        Console.ReadLine();
    }

    private static IFormFile ConvertByteArrayToIFormFile(byte[] fileBytes, string fileName, string contentType)
    {
        return new CustomFormFile(fileBytes, fileName, contentType);
    }
}



public class CustomFormFile : IFormFile
{
    private readonly byte[] _fileBytes;
    private readonly string _fileName;
    private readonly string _contentType;

    public CustomFormFile(byte[] fileBytes, string fileName, string contentType)
    {
        _fileBytes = fileBytes ?? throw new ArgumentNullException(nameof(fileBytes));
        _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        _contentType = contentType ?? throw new ArgumentNullException(nameof(contentType));

        Length = fileBytes.Length;
        Name = fileName;
        FileName = fileName;
        Headers = new HeaderDictionary();
        ContentType = contentType;
    }

    public Stream OpenReadStream()
    {
        return new MemoryStream(_fileBytes);
    }

    public void CopyTo(Stream target)
    {
        using (var stream = new MemoryStream(_fileBytes))
        {
            stream.CopyTo(target);
        }
    }

    public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        using (var stream = new MemoryStream(_fileBytes))
        {
            await stream.CopyToAsync(target, 81920, cancellationToken);
        }
    }

    public long Length { get; }

    public string Name { get; }

    public string FileName { get; }

    public IHeaderDictionary Headers { get; }

    public string ContentType { get; }

    public string ContentDisposition
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public IFormFileCollection Files
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }
}
