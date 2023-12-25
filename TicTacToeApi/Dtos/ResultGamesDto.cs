using EfPersistence.Models;

namespace TicTacToeApi.Dtos;

public record ResultGamesDto(int Id, Player FirstPlayer, Player SecondPlayer, Player? WinnerPlayer, bool IsDraw, DateTime MatchDateTime);