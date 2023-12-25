using Application.Games;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfPersistence.Repositories;

public class GamesRepository: IGamesRepository
{
    private readonly GameStatisticsDbContext _dbContext;
    
    public GamesRepository(GameStatisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyCollection<MatchResult>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.MatchResults.ToListAsync(cancellationToken);
    }

    public async Task<MatchResult?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.MatchResults.FirstOrDefaultAsync(mr => mr.Id == id, cancellationToken);
    }

    public async Task<int> CreateAsync(MatchResult game, CancellationToken cancellationToken)
    {
        _dbContext.MatchResults.Add(game);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return game.Id;
    }
}