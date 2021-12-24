using System.Text.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using RTA.Producer.Extensions;
namespace RTA.Producer.EndpointDefinitions;

public class EndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("/producedata", ProduceData);
    }

    public void DefineServices(IServiceCollection services)
    {
        var host = Environment.GetEnvironmentVariable("ADVERTISED_HOST");
        var port = Environment.GetEnvironmentVariable("ADVERTISED_PORT");

        var config = new ProducerConfig
        {
            BootstrapServers = $"{host}:{port}"
        };

        services.AddScoped(_ => new ProducerBuilder<Null, string>(config));
    }

    public static async Task<IResult> ProduceData([FromBody] Events events, ProducerBuilder<Null, string> pb)
    {
        try
        {
            var json = JsonSerializer.Serialize<Events>(events);

            var dr = await pb.Build().ProduceAsync("rtaTestTopic", new Message<Null, string> { Value = json });

            return Results.Ok($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<Null, string> e)
        {
            return Results.Ok($"Delivery failed: {e.Error.Reason}");
        }
    }
}
