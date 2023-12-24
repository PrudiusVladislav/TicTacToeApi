using EfPersistence.Models;
using Microsoft.EntityFrameworkCore;

namespace EfPersistence;

public class GameStatisticsDbContext: DbContext
{
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<MatchResult> MatchResults { get; set; } = null!;
    
    public GameStatisticsDbContext(DbContextOptions<GameStatisticsDbContext> options) 
        : base(options)
    {
    }
}