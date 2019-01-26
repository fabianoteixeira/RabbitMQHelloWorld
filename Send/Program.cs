using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send
{
    class Program
    {
        
        public static void Main()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "dev-fabiano-server.eastus.cloudapp.azure.com",
                UserName = "backup",
                Password = "backup"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while (true)
                {

                    Console.Write("Digite uma mensagem: ");
                    string message = Console.ReadLine();

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                }


            }


        }
    }
}
