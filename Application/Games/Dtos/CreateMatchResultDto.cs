namespace Application.Games.Dtos;

public record CreateMatchResultDto(string FirstPlayerName, string SecondPlayerName, bool IsDraw, string? WinnerName, DateTime MatchDateTime);