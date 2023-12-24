using System.ComponentModel.DataAnnotations.Schema;

namespace EfPersistence.Models;

public class MatchResult
{
    public int Id { get; set; }
    
    public int FirstPlayerId { get; set; }
    public Player FirstPlayer { get; set; }
    
    public int SecondPlayerId { get; set; }
    public Player SecondPlayer { get; set; }
    
    public int? WinnerPlayerId { get; set; }
    public Player? WinnerPlayer { get; set; }
    
    
    public bool IsDraw { get; set; }
    public DateTime MatchDateTime { get; set; }
}