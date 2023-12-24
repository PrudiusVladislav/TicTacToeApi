namespace EfPersistence.Models;

public class MatchResult
{
    public int Id { get; set; }
    public int FirstPlayerId { get; set; }
    public int SecondPlayerId { get; set; }
    
    public int? WinnerPlayerId { get; set; }
    public bool IsDraw { get; set; }
    
    public DateTime MatchDateTime { get; set; }
    
    public Player FirstPlayer { get; set; } = null!;
    public Player SecondPlayer { get; set; } = null!;
    public Player? WinnerPlayer { get; set; }
}