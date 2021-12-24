using System;
using Microsoft.OpenApi.Models;
using RTA.Producer.Extensions;

namespace RTA.Producer.EndpointDefinitions;

public class SwaggerDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RTA API"));
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RTA API", Version = "v1" });
        });
    }
}


