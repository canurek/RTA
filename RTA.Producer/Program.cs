using System.Text.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/producedata", async ([FromBody] Events events) =>
{
    var host = Environment.GetEnvironmentVariable("ADVERTISED_HOST");
    var port = Environment.GetEnvironmentVariable("ADVERTISED_PORT");
    var topic = Environment.GetEnvironmentVariable("TOPIC");

    var config = new ProducerConfig
    {
        BootstrapServers = $"{host}:{port}"
    };

    using var p = new ProducerBuilder<Null, string>(config).Build();

    try
    {
        var json = JsonSerializer.Serialize<Events>(events);

        var dr = await p.ProduceAsync(topic, new Message<Null, string> { Value = json });
        return $"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'";
    }
    catch (ProduceException<Null, string> e)
    {
        return $"Delivery failed: {e.Error.Reason}";
    }
});

app.Run();


