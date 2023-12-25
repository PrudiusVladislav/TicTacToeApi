using Domain.Models;

namespace Application.Players;

public interface IPlayersService
{
    Task<IReadOnlyCollection<Player>> GetAllAsync(CancellationToken cancellationToken);
    Task<Player?> GetAsync(int id, CancellationToken cancellationToken);
    Task<Player?> GetByNameAsync(string? name, CancellationToken cancellationToken);
    Task<int> CreateAsync(Player player, CancellationToken cancellationToken);
}