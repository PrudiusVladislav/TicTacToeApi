using EfPersistence;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Services;

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
        services.AddTransient<GameStatisticsService>();
        services.AddTransient<PlayersService>();
    }
}