using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.HostName = "localhost"; 

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "topic_logs", ExchangeType.Topic); //bu eşleşmeyen kuyruk elemanlarını siler

        var queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(
            queue: queueName,
            exchange: "topic_logs",
            routingKey: args.Length > 0 ? args[0] : "info");

        //channel.QueueDeclare(
        //    queue: "hello",
        //    durable: true,
        //    exclusive: false,
        //    autoDelete: false,
        //    arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Kuyruk mesajını aldık işlem yapıyoruz...");

            Thread.Sleep(10000);
            Console.WriteLine("Kuyruktan gelen mesaj: " + message);

            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        channel.BasicConsume(
            queue: queueName,
            autoAck: false,
            consumer: consumer);

        Console.ReadLine();

    }
}
