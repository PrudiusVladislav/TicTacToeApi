namespace TicTacToeApi.Extensions;

public static class StatisticsEndpoints
{
    public static void AddStatisticsEndpoints(this RouteGroupBuilder endpoints)
    {
        endpoints.MapGet("/api/statistics", async context =>
        {
            await context.Response.WriteAsJsonAsync(new { Message = "Hello World!" });
        });
    }
}