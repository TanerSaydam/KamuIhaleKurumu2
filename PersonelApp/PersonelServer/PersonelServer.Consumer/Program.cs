using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PersonelServer.Consumer;

internal class Program
{
    static void Main(string[] args)
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

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            string id = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Mail {id} numaralı kullanıcıya gönderildi!");
        };

        channel.BasicConsume(
            queue: "email",
            autoAck: true,
            consumer: consumer);

        Console.ReadLine();
    }
}
