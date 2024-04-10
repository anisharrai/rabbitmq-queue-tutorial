using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "myuser",
            Password = "mypassword"
        };

        // Create connection to RabbitMQ server
        using (var connection = factory.CreateConnection())
        {
            // Create channel
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "delivery_tasks",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for tasks.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var task = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Processing task: {0}", task);
                    Thread.Sleep(2000); // Simulate task processing
                    Console.WriteLine(" [x] Task completed: {0}", task);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "delivery_tasks",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
