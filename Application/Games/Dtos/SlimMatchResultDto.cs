using Domain.Models;

namespace Application.Games.Dtos;

public record SlimMatchResultDto(int Id, Player FirstPlayer, Player SecondPlayer, Player? WinnerPlayer, bool IsDraw, DateTime MatchDateTime);