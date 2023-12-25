using System.ComponentModel.DataAnnotations;
using Application.Games;
using Application.Games.Dtos;
using Application.Players;
using Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace UnitTests;

public class GamesServiceTests
{
    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_ValidInput_ReturnsSuccess()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player2",
            IsDraw:  false,
            WinnerName:  "Player1",
            MatchDateTime: new DateTime(2023, 12, 25)
        );
    
        var playersService = Substitute.For<IPlayersService>();
        playersService.GetByNameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(new Player());

        var service = new GamesService(null!, playersService);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(ValidationResult.Success);
    }
    
    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_PlayersSame_ReturnsErrorMessage()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player1",
            IsDraw:  false,
            WinnerName:  "Player1",
            MatchDateTime: new DateTime(2023, 12, 25)
        );
        
        var service = new GamesService(null!, null!);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new ValidationResult("Players cannot be the same"));
    }

    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_WinnerNotPlayer_ReturnsErrorMessage()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player2",
            IsDraw:  false,
            WinnerName:  "Player3",
            MatchDateTime: new DateTime(2023, 12, 25)
        );

        var service = new GamesService(null!, null!);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new ValidationResult("Winner must be one of the players"));
    }
    
    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_DrawWithWinner_ReturnsErrorMessage()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player2",
            IsDraw: true,
            WinnerName:  "Player1",
            MatchDateTime: new DateTime(2023, 12, 25)
        );
        
        var service = new GamesService(null!, null!);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new ValidationResult("Winner cannot be specified if the match is a draw"));
    }
    
    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_FirstPlayerNotExist_ReturnsErrorMessage()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player2",
            IsDraw: false,
            WinnerName:  "Player1",
            MatchDateTime: new DateTime(2023, 12, 25)
        );
    
        var playersService = Substitute.For<IPlayersService>();
        playersService.GetByNameAsync("Player1", Arg.Any<CancellationToken>()).Returns((Player)null!);

        var service = new GamesService(null!, playersService);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new ValidationResult("First player does not exist"));
    }

    [Fact]
    public async Task ValidateCreateMatchResultDtoAsync_SecondPlayerNotExist_ReturnsErrorMessage()
    {
        // Arrange
        var dto = new CreateMatchResultDto
        (
            FirstPlayerName: "Player1",
            SecondPlayerName: "Player2",
            IsDraw: false,
            WinnerName: "Player1",
            MatchDateTime: new DateTime(2023, 12, 25)
        );

        var playersService = Substitute.For<IPlayersService>();
        playersService.GetByNameAsync("Player1", Arg.Any<CancellationToken>()).Returns(new Player());
        playersService.GetByNameAsync("Player2", Arg.Any<CancellationToken>()).Returns((Player)null!);

        var service = new GamesService(null!, playersService);

        // Act
        var result = await service.ValidateCreateMatchResultDtoAsync(dto, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(new ValidationResult("Second player does not exist"));
    }

}