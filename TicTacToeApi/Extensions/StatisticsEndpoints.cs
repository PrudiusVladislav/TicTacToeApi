using TicTacToeApi.Services;

namespace TicTacToeApi.Extensions;

public static class GameStatisticsEndpoints
{
    public static void AddGameStatisticsEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("", async (GameStatisticsService service) => Results.Ok(await service.GetAllAsync()));
        
        endpoints.MapGet("/{id:int}", async (int id, GameStatisticsService service) =>
        {
            var game = await service.GetAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });
    }
}