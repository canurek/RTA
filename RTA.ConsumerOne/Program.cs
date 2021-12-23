// See https://aka.ms/new-console-template for more information
using System.Text;
using Confluent.Kafka;

var host = Environment.GetEnvironmentVariable("ADVERTISED_HOST");
var port = Environment.GetEnvironmentVariable("ADVERTISED_PORT");
var topic = Environment.GetEnvironmentVariable("TOPIC");

var config = new ConsumerConfig
{
    BootstrapServers = $"{host}:{port}",
    GroupId = Guid.NewGuid().ToString()
};

using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
{
    c.Subscribe(topic);

    CancellationTokenSource cts = new();

    Console.CancelKeyPress += (_, e) => {
        e.Cancel = true; // prevent the process from terminating.
        cts.Cancel();
    };

    try
    {
        while (true)
        {
            try
            {
                var cr = c.Consume(cts.Token);
                Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error occured: {e.Error.Reason}");
            }
        }
    }
    catch (OperationCanceledException)
    {
        // Close and Release all the resources held by this consumer  
        c.Close();
    }
}
        