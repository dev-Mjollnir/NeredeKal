using HotelService.Infrastructure.Data.Extensions;
using HotelService.Infrastructure.Middlewares;
using MediatR;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfigurations:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = "hotelservice-logs-{0:yyyy.MM.dd}",
        })
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers()
    .AddJsonOptions(x => { x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddMediatR(typeof(Program));
builder.Services.RegisterDataModule(builder.Configuration);

var app = builder.Build();
app.UseDataModule();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
