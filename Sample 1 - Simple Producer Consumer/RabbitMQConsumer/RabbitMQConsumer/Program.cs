// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var model = connection.CreateModel();
model.QueueDeclare("demo-queue", durable: true, exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(model);
consumer.Received += (sender, e) =>
{
    var body = e.Body.ToArray();
    var mesage = Encoding.UTF8.GetString(body);
    Console.WriteLine(mesage);

};
model.BasicConsume("demo-queue", true, consumer);

Console.ReadLine();
