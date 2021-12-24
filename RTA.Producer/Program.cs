using System.Text.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using RTA.Producer;
using RTA.Producer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(IAssemblyMarker));

var app = builder.Build();

app.UseEndpointDefinitions();

app.Run();



