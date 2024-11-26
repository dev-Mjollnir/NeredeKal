using HotelService.Infrastructure.Data.Extensions;
using HotelService.Infrastructure.Middlewares;
using MediatR;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
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
