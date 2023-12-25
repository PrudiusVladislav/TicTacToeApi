using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using TicTacToeApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddApplicationDependencies(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/games").AddGameStatisticsEndpoints();
app.MapGroup("/api/players").AddPlayersEndpoints();

app.Run();