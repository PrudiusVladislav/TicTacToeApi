using System.ComponentModel.DataAnnotations;

namespace EfPersistence.Models;

public class Player
{
    public int Id { get; set; }
    
    [MaxLength(50), MinLength(2)]
    public string Name { get; set; } = string.Empty;
    
    public IEnumerable<MatchResult>? MatchResults { get; set; } = new List<MatchResult>();
}