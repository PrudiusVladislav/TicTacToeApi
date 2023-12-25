using System.Text.RegularExpressions;
using Domain.Models;

namespace Application.Games;

public interface IGamesRepository
{
    Task<IReadOnlyCollection<MatchResult>> GetAllAsync(CancellationToken cancellationToken);
    Task<MatchResult?> GetAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateAsync(MatchResult game, CancellationToken cancellationToken);
}