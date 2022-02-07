﻿// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
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

var message = new { Name = "producer", message = "hello" };

var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

model.BasicPublish("","demo-queue", null, body);

Console.ReadLine();
// new { message = message };