using EfPersistence;
using EfPersistence.Models;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Dtos;
using TicTacToeApi.Extensions;

namespace TicTacToeApi.Services;

public class GameStatisticsService
{
    private readonly GameStatisticsDbContext _dbContext;
    
    public GameStatisticsService(GameStatisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<ResultGamesDto>> GetAllAsync()
    {
        return await _dbContext.MatchResults
            .IncludePlayers()
            .MapGamesToResultDto()
            .ToListAsync();
    }
    
    public async Task<MatchResult?> GetAsync(int id)
    {
        return await _dbContext.MatchResults.FirstOrDefaultAsync(x => x.Id == id);
    }
}