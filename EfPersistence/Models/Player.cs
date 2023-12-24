namespace EfPersistence.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public IEnumerable<MatchResult> MatchResults { get; set; } = new List<MatchResult>();
}