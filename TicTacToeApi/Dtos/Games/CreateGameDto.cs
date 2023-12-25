
namespace TicTacToeApi.Dtos.Games;

public record CreateGameDto(string FirstPlayerName, string SecondPlayerName, bool IsDraw, string? WinnerName, DateTime MatchDateTime);