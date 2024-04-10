using RabbitMQ.Client;
using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // RabbitMQ connection details
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

                //  Console.WriteLine("Enter delivery tasks (press 'q' to quit):");

                // while (true)
                // {
                //     string task = Console.ReadLine();

                //     if (task.ToLower() == "q")
                //         break;

                //     var body = Encoding.UTF8.GetBytes(task);

                //     var properties = channel.CreateBasicProperties();
                //     properties.Persistent = true;

                //     channel.BasicPublish(exchange: "",
                //                          routingKey: "delivery_tasks",
                //                          basicProperties: properties,
                //                          body: body);

                //     Console.WriteLine(" [x] Sent task: {0}", task);
                // }

                //infinite tasks
                string task = "nakhipot-delivery";
                do
                {
                    if (task.ToLower() != "q")
                    {
                        var body = Encoding.UTF8.GetBytes(task);

                        var properties = channel.CreateBasicProperties();
                        properties.Persistent = true;

                        channel.BasicPublish(exchange: "",
                                             routingKey: "delivery_tasks",
                                             basicProperties: properties,
                                             body: body);

                        Console.WriteLine(" [x] Sent task: {0}", task);
                    }

                } while (task.ToLower() != "q");
            }
        }
    }
}
