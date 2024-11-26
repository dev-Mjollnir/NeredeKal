using HotelService.Infrastructure.Data.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Program));
builder.Services.RegisterDataModule(builder.Configuration);

app.Run();
