using System.ComponentModel.DataAnnotations;
using Application.Players;
using Application.Players.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToeApi.Extensions;

public static class PlayersEndpoints
{
    public static void AddPlayersEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("", async ([FromServices]IPlayersService service,
            CancellationToken cancellationToken) =>
        {
            return Results.Ok(await service.GetAllAsync(cancellationToken));
        });
        
        endpoints.MapGet("/{id:int}", async (int id,
            [FromServices]IPlayersService service, CancellationToken cancellationToken) =>
        {
            var player = await service.GetAsync(id, cancellationToken);
            return player is null ? Results.NotFound() : Results.Ok(player);
        });
        
        endpoints.MapGet("/{name}", async (string name,
            [FromServices]IPlayersService service, CancellationToken cancellationToken) =>
        {
            var player = await service.GetByNameAsync(name, cancellationToken);
            return player is null ? Results.NotFound() : Results.Ok(player);
        });
        
        endpoints.MapPost("", async ([FromBody]CreatePlayerDto dto,
            [FromServices]IPlayersService service, CancellationToken cancellationToken) =>
        {
            try
            {
                var id = await service.CreateAsync(dto, cancellationToken);
                return Results.Created($"/api/players/{id}", await service.GetAsync(id, cancellationToken));
            }
            catch (ValidationException e)
            {
                return Results.BadRequest(e.Message);
            }
        });
    }
}