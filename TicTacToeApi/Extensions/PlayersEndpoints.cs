using TicTacToeApi.Services;

namespace TicTacToeApi.Extensions;

public static class PlayersEndpoints
{
    public static void AddPlayersEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("", async (PlayersService service) => Results.Ok(await service.GetAllAsync()));
        
        endpoints.MapGet("/{id:int}", async (int id, PlayersService service) =>
        {
            var player = await service.GetAsync(id);
            return player is null ? Results.NotFound() : Results.Ok(player);
        });
    }
}