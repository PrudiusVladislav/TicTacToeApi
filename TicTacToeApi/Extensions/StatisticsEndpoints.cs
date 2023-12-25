using Microsoft.AspNetCore.Mvc;
using TicTacToeApi.Services;

namespace TicTacToeApi.Extensions;

public static class GameStatisticsEndpoints
{
    public static void AddGameStatisticsEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("", async ([FromServices]GameStatisticsService service) => Results.Ok(await service.GetAllAsync()));
        
        endpoints.MapGet("/{id:int}", async (int id, [FromServices]GameStatisticsService service) =>
        {
            var game = await service.GetAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });
    }
}