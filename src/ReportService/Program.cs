using NeredeKal.Infrastructure.Infrastructure.Data.Extensions;
using NeredeKal.Infrastructure.Infrastructure.Middlewares;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using MediatR;
using NeredeKal.RabbitMQ.Extensions;
using ReportService.Consumer;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers()
    .AddJsonOptions(x => {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddMediatR(typeof(Program));
builder.Services.RegisterDataModule(builder.Configuration);
builder.Services.RegisterRabbitMQSettings(builder.Configuration);
builder.Services.AddHostedService<ReportConsumer>();


var app = builder.Build();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
