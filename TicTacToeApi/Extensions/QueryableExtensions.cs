using EfPersistence.Models;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Dtos;

namespace TicTacToeApi.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<MatchResult> IncludePlayers(this IQueryable<MatchResult> query)
    {
        return query
            .Include(r => r.FirstPlayer)
            .Include(r => r.SecondPlayer)
            .Include(r => r.WinnerPlayer);
    }
    
    public static IQueryable<ResultGamesDto> MapGamesToResultDto(this IQueryable<MatchResult> query)
    {
        return query.Select(r => new ResultGamesDto(r.Id, r.FirstPlayer, r.SecondPlayer, r.WinnerPlayer, r.IsDraw, r.MatchDateTime));
    }
    
    public static IQueryable<Player> IncludeStatistics(this IQueryable<Player> query)
    {
        return query.Include(p => p.MatchResults);
    }
}