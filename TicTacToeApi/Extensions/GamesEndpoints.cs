using System.ComponentModel.DataAnnotations;
using Application.Games;
using Application.Games.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace TicTacToeApi.Extensions;

public static class GameStatisticsEndpoints
{
    public static void AddGameStatisticsEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("", async ([FromServices] IGamesService service, CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllAsync(cancellationToken));
        });
        
        endpoints.MapGet("/{id:int}", async (int id,
            [FromServices]IGamesService service, CancellationToken cancellationToken) =>
        {
            var game = await service.GetAsync(id, cancellationToken);
            return game is null ? Results.NotFound() : Results.Ok(game);
        });
        
        endpoints.MapPost("", async ([FromBody]CreateMatchResultDto dto,
            [FromServices]IGamesService service, CancellationToken cancellationToken) =>
        {
            try
            {
                var id = await service.CreateAsync(dto, cancellationToken);
                return Results.Ok(await service.GetAsync(id, cancellationToken));
            }
            catch (ValidationException e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}