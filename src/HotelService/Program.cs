using HotelService.Infrastructure.Data.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program));
builder.Services.RegisterDataModule(builder.Configuration);

var app = builder.Build();
app.UseDataModule();
app.MapControllers();
app.Run();
