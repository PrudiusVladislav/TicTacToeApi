using System.ComponentModel.DataAnnotations;
using Application.Players.Dtos;
using Domain.Models;

namespace Application.Players;

public interface IPlayersService
{
    Task<IReadOnlyCollection<Player>> GetAllAsync(CancellationToken cancellationToken);
    Task<Player?> GetAsync(int id, CancellationToken cancellationToken);
    Task<Player?> GetByNameAsync(string? name, CancellationToken cancellationToken);
    Task<int> CreateAsync(CreatePlayerDto dto, CancellationToken cancellationToken);
    Task<ValidationResult?> ValidateCreatePlayerDtoAsync(CreatePlayerDto dto, CancellationToken cancellationToken);
}