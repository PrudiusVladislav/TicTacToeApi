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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchResult>()
            .HasOne(m => m.FirstPlayer)
            .WithMany()
            .HasForeignKey(m => m.FirstPlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<MatchResult>()
            .HasOne(m => m.SecondPlayer)
            .WithMany()
            .HasForeignKey(m => m.SecondPlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<MatchResult>()
            .HasOne(m => m.WinnerPlayer)
            .WithMany()
            .HasForeignKey(m => m.WinnerPlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}