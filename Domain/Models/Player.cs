using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Player
{
    public int Id { get; set; }
    
    [MaxLength(50), MinLength(2)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<MatchResult>? MatchResults { get; set; } = new List<MatchResult>();
}