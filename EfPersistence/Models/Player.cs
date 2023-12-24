using System.ComponentModel.DataAnnotations;

namespace EfPersistence.Models;

public class Player
{
    public int Id { get; set; }
    
    [MaxLength(50), MinLength(2)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// symbol a player uses to play the game (X or O)
    /// </summary>
    public char PlayerSymbol { get; set; }
    
    public IEnumerable<MatchResult>? MatchResults { get; set; } = new List<MatchResult>();
}