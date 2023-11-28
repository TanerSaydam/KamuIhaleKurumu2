using RabbitMQ.Client;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace RabbitMQ.Publisher;

internal class Program
{
    static void Main(string[] args)
    {
        var connection = CreateRabbitMQConnection();

        var channel = connection.CreateModel();

        IDictionary<string, object> arguments = new Dictionary<string, object>();
        arguments.Add("x-message-ttl", 5000); //kuyruğa gönderdiğiniz datanın kuyrukta kalabileceği süreyi milisaniye cinsinden gösterir
        arguments.Add("x-expires", 5000); //kuyruk ne kadar süre kullanılmazsa silinsin (milisaniye cinsinden)
        arguments.Add("x-max-length-bytes", 1024868); //kuyruktaki datanın max kaç byte olabileceği
        arguments.Add("x-dead-letter-exchange", "deadLetterExchangeName"); //Mesajın işlenemediği durumlarda yönlendirileceği ölü harf değişimi kuyruğunu belirtir
        arguments.Add("x-dead-letter-routing-key", "my.dead.letter.message"); //Ölü harf kuyruğuna mesaj gönderilirken kullanılacak yönlendirme anahtarı
        arguments.Add("x-max-priority", 10); //Kuyruğun desteklediği maksiumum öncelik seviyesi
        arguments.Add("x-queue-mode", "lazy"); //mesajların diske mi memorye yazılacak belirtir //default(memory) - lazy(disk) parameterleri alır
        arguments.Add("x-queue-master-locator", " client - local"); // client - local - random - min-masters


        //        RabbitMQ'da x-queue-master-locator argümanı, özellikle RabbitMQ'nun kümelenmiş(clustered) bir yapıda çalıştığı durumlarda önem taşır. Bu argüman, bir kuyruğun "master" kopyasının kümelenmiş ortam içindeki hangi düğümde yer alacağını belirlemek için kullanılır.RabbitMQ'da kuyruklar, yüksek kullanılabilirlik ve dayanıklılık için birden fazla düğüm arasında kopyalanabilir. Bu kopyalar arasında biri "master" olarak belirlenir ve diğerleri "slaves" (kopyalar) olarak işlev görür.

        //Master ve Slave Kavramları
        //Master Kopya: Her kuyruk için bir "master" kopya vardır. Bu, kuyruğun ana sürümüdür ve tüm yazma işlemleri öncelikle bu kopya üzerinde gerçekleşir.
        //Slave Kopyalar: Master kopyanın bir veya daha fazla yedek kopyasıdır. Bunlar, yüksek kullanılabilirlik sağlamak amacıyla master kopyayla senkronize edilir.
        //x - queue - master - locator Argümanının Kullanımı
        //x - queue - master - locator argümanı, master kopyanın kümelenmiş ortamda hangi düğümde konumlandırılacağını belirler. Bu, ağ gecikmesini azaltmak ve kuyruğa erişimi hızlandırmak için önemli olabilir. Örneğin:

        //        client - local: Bu değer, master kopyayı, kuyruğu oluşturan istemciye en yakın düğümde konumlandırmaya çalışır. Bu, istemci ile master kopya arasındaki ağ gecikmesini azaltabilir.
        //random: Master kopya, mevcut düğümler arasından rastgele birinde konumlandırılır.
        //min - masters: Bu, kümelenmiş ortamdaki her düğümde master kopya sayısını dengeler.
        //Pratikte Kullanım
        //Kuyruk oluştururken x - queue - master - locator argümanını eklemek, özellikle RabbitMQ'nun birden fazla sunucu üzerinde dağıtılmış bir yapıda çalıştırıldığı durumlarda faydalı olabilir. Bu ayar, kuyrukların hangi düğümlerde master olarak konumlandırılacağını belirleyerek, performansı ve erişilebilirliği optimize etmeye yardımcı olur.

        //Ancak, RabbitMQ'nun tek bir sunucuda çalıştığı basit yapılandırmalarda, x-queue-master-locator argümanının herhangi bir etkisi olmayacaktır, çünkü tüm kuyruklar zaten aynı düğümde bulunacaktır. Bu özellik, daha karmaşık, kümelenmiş yapılandırmalarda önem kazanır.

        #region Kuyruk Oluşturma
        channel.QueueDeclare(
            queue: "hello",
            durable: true, //rabbitmq kapanırsa kuyruğu saklayım mı
            exclusive: false,//tek connection üzerindenmi erişim sağlasın
            autoDelete: false,//son consumer kopunca kuyruğu sil
            arguments: null); 
        #endregion

        //channel.ExchangeDeclare(exchange: "topic_logs", ExchangeType.Topic);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";
        properties.ContentEncoding = "UTF-8";
        properties.Priority = 9;
        properties.CorrelationId = Guid.NewGuid().ToString(); //İstek-yanıt işlemlerinde bir isteğin hangi yanıtla ilişkili olduğunu belirlemek için kullanılır
        properties.ReplyTo = "YanıtKuyruk"; //yanıtım gönderileceği kuyruk adı
        properties.Expiration = "5000"; //mesjaın son kullanıma süresini mili saniye olarak belirtir
        properties.MessageId = Guid.NewGuid().ToString(); //Mesjaın benzersiz bir ID'si olmasını sağlıyor
                                                          // Şu anki zamanı Unix zaman damgası olarak alın
        var unixTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        properties.Timestamp = new AmqpTimestamp(unixTime); //Mesajın oluşturulduğu zaman damgası
        properties.Type = "command"; //mesajın türünü tanımlar //command - event
        properties.UserId = Guid.NewGuid().ToString(); //mesjaın kullanıcı ID'sini belirtiyorsun
        properties.AppId = Guid.NewGuid().ToString(); //Mesajın gönderilecek uygulamanın kimlik ID'si


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