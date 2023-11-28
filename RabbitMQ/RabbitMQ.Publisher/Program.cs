using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;

namespace RabbitMQ.Publisher;

internal class Program
{
    static void Main(string[] args)
    {
        var connection = CreateRabbitMQConnection();

        var channel = connection.CreateModel();

        #region Kuyruk Oluşturma
        //channel.QueueDeclare(
        //    queue: "hello",
        //    durable: true, //rabbitmq kapanırsa kuyruğu saklayım mı
        //    exclusive: false,//tek connection üzerindenmi erişim sağlasın
        //    autoDelete: false,//son consumer kopunca kuyruğu sil
        //    arguments: null); //buraya geleceğiz
        #endregion

        channel.ExchangeDeclare(exchange: "topic_logs", ExchangeType.Topic);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;


        string message = args.Length > 1 ? args[1] : "Hello World";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(
            exchange: "topic_logs",
            routingKey: args.Length > 0 ? args[0] : "info.deneme",
            basicProperties: properties, 
            body: body);

        Console.WriteLine("Mesaj başarıyla kuyruğa gönderildi");

        Console.ReadLine();
    }

    private static IConnection CreateRabbitMQConnection()
    {
        var factory = new ConnectionFactory();
        factory.HostName = "localhost";
        //aşağıdaki seçenekler default değerler değiştiyse doldurulmalı
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";

        var connection = factory.CreateConnection();
        
        return connection;
    }
}