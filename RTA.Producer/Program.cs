using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!")
   .WithName("Hello");

app.UseHttpsRedirection();

app.MapPost("/producedata", async (string data) =>
{
    var host = Environment.GetEnvironmentVariable("ADVERTISED_HOST");
    var port = Environment.GetEnvironmentVariable("ADVERTISED_PORT");

    var config = new ProducerConfig
    {
        BootstrapServers = $"{host}:{port}"
    };

    using var p = new ProducerBuilder<Null, string>(config).Build();

    try
    {
        var dr = await p.ProduceAsync("rtaTestTopic", new Message<Null, string> { Value = data });
        return $"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'";
    }
    catch (ProduceException<Null, string> e)
    {
        return $"Delivery failed: {e.Error.Reason}";
    }
});

app.Run();
