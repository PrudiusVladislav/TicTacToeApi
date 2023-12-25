using Application.Players;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfPersistence.Repositories;

public class PlayersRepository: IPlayersRepository
{
    private readonly GameStatisticsDbContext _dbContext;
    
    public PlayersRepository(GameStatisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyCollection<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Players.ToListAsync(cancellationToken);
    }

    public async Task<Player?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(p=> p.Id == id, cancellationToken);
    }

    public async Task<Player?> GetByNameAsync(string? name, CancellationToken cancellationToken)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(p =>
            p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase),
            cancellationToken);
    }

    public async Task<int> CreateAsync(Player player, CancellationToken cancellationToken)
    {
       _dbContext.Players.Add(player);
       await _dbContext.SaveChangesAsync(cancellationToken);
       return player.Id;
    }
}