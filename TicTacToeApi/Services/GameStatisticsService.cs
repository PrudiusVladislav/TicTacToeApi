using EfPersistence;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Dtos;
using TicTacToeApi.Dtos.Games;
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
    //
    // public async Task<int> CreateAsync(CreateGameDto gameDto)
    // {
    //     var game = new MatchResult
    //     {
    //         FirstPlayer = gameDto.Player1,
    //         Player2 = gameDto.Player2,
    //         Winner = gameDto.Winner,
    //         Date = gameDto.Date
    //     };
    //     _dbContext.MatchResults.Add(game);
    //     await _dbContext.SaveChangesAsync();
    //     return game.Id;
    // }
    //
    // public async Task<ValidationResult> ValidateCreateGameDto(CreateGameDto dto)
    // {
    //     
    // }
    
    
}