using Application.Games;
using Application.Players;
using EfPersistence;
using EfPersistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TicTacToeApi.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<GameStatisticsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlClient"),
                x => x.MigrationsAssembly("EfPersistence"));
        });
        
        services.AddTransient<IGamesRepository, GamesRepository>();
        services.AddTransient<IPlayersRepository, PlayersRepository>();
        
        services.AddTransient<IGamesService, GamesService>();
        services.AddTransient<IPlayersService, PlayersService>();
    }
}