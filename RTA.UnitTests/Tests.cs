using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using RTA.Producer.EndpointDefinitions;
using Xunit;

namespace RTA.UnitTests;

public static class IResultExtensions
{
    public static T? GetOkObjectResultValue<T>(this IResult result)
    {
        return (T?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("Value",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetOkObjectResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.OkObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }

    public static int? GetNotFoundResultStatusCode(this IResult result)
    {
        return (int?)Type.GetType("Microsoft.AspNetCore.Http.Result.NotFoundObjectResult, Microsoft.AspNetCore.Http.Results")?
            .GetProperty("StatusCode",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)?
            .GetValue(result);
    }
}

public class Tests
{
    private readonly ProducerBuilder<Null, string> _pbStub =
        Substitute.For<ProducerBuilder<Null, string>>(new ProducerConfig(new ClientConfig { BootstrapServers = "" }));

    public class TestClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
            new Event {
                app = "1231232-321312-12312321-21312",
                type = "",
                time = new DateTime(2020, 2, 10, 13, 40, 27),
                isSucceeded = true,
                meta = new Meta { },
                user = new User
                {
                    isAuthenticated = true,
                    provider = "b2c-internal",
                    id = 231213,
                    EMail = "eser.ozvataf@setur.com"
                },
                attributes = new Attributes
                {
                    hotelId = 4123,
                    hotelRegion = "Antalya",
                    hotelName = "Rixos"
                }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    [Theory]
    [ClassData(typeof(TestClassData))]
    public async Task ProducingTest(Event @event)
    {
        //Arrange
        var events = new Events
        {
            events = new List<Event>()
        };

        events.events.Add(@event);

        var json = JsonSerializer.Serialize<Events>(events);

        var expectedResult = new DeliveryResult<Null, string> {
            Topic = "rtaTestTopic",
            Partition = 0,
            Offset = 0,
            Message = new Message<Null, string> { Value = json }
        };

        _pbStub.Build().ProduceAsync(Arg.Any<string>(),
            Arg.Any<Message<Null, string>>(),
            Arg.Any<CancellationToken>()
            ).Returns(expectedResult);

        //Act
        var result = await EndpointDefinition.ProduceData(events, _pbStub);

        //Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }
}
