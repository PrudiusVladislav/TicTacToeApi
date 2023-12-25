using EfPersistence;
using EfPersistence.Models;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Dtos;

namespace TicTacToeApi.Services;

public class PlayersService
{
    private readonly GameStatisticsDbContext _dbContext;
    
    public PlayersService(GameStatisticsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Player>> GetAllAsync()
    {
        return await _dbContext.Players.ToListAsync();
    }
    
    public async Task<Player?> GetAsync(int id)
    {
        return await _dbContext.Players.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<int> CreateAsync(CreatePlayerDto dto)
    {
        var player = new Player { Name = dto.Name };
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync();
        return player.Id;
    }
}