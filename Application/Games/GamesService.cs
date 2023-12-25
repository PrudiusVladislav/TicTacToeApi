using System.ComponentModel.DataAnnotations;
using Application.Games.Dtos;
using Application.Players;
using Domain.Models;

namespace Application.Games;

public class GamesService: IGamesService
{
    private readonly IGamesRepository _gamesRepository;
    private readonly IPlayersService _playersService;
    
    public GamesService(IGamesRepository gamesRepository, IPlayersService playersService)
    {
        _gamesRepository = gamesRepository;
        _playersService = playersService;
    }
    
    public async Task<IReadOnlyCollection<SlimMatchResultDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var matchResults = await _gamesRepository.GetAllAsync(cancellationToken);
        return MapMatchResultsToDto(matchResults)
            .ToList()
            .AsReadOnly();

    }

    public async Task<SlimMatchResultDto?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var matchResult = await _gamesRepository.GetAsync(id, cancellationToken);
        return matchResult is null ? null : MapResultToDto(matchResult);
    }

    public async Task<int> CreateAsync(CreateMatchResultDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateCreateMatchResultDtoAsync(dto, cancellationToken);
        if (validationResult != ValidationResult.Success)
            throw new ValidationException(validationResult?.ErrorMessage);
        
        var matchResult = await MapFromDtoToResultAsync(dto, _playersService, cancellationToken);
        return await _gamesRepository.CreateAsync(matchResult, cancellationToken);
    }
    
    public async Task<ValidationResult?> ValidateCreateMatchResultDtoAsync(CreateMatchResultDto dto,
        CancellationToken cancellationToken)
    {
        if (dto.FirstPlayerName == dto.SecondPlayerName)
            return new ValidationResult("Players cannot be the same");
        if (dto.FirstPlayerName != dto.WinnerName && dto.SecondPlayerName != dto.WinnerName)
            return new ValidationResult("Winner must be one of the players");
        
        var firstPlayer = await _playersService.GetByNameAsync(dto.FirstPlayerName, cancellationToken);
        if (firstPlayer is null)
            return new ValidationResult("First player does not exist");
        var secondPlayer = await _playersService.GetByNameAsync(dto.SecondPlayerName, cancellationToken);
        if (secondPlayer is null)
            return new ValidationResult("Second player does not exist");
        
        return ValidationResult.Success;
    }
    
    private static IEnumerable<SlimMatchResultDto> MapMatchResultsToDto(IEnumerable<MatchResult> matchResults)
    {
        return matchResults.Select(MapResultToDto);
    }
    
    private static SlimMatchResultDto MapResultToDto(MatchResult matchResult)
    {
        return new SlimMatchResultDto(
            Id: matchResult.Id,
            FirstPlayer: matchResult.FirstPlayer,
            SecondPlayer: matchResult.SecondPlayer,
            WinnerPlayer: matchResult.WinnerPlayer,
            IsDraw: matchResult.IsDraw,
            MatchDateTime: matchResult.MatchDateTime);
    }
    
    private static async Task<MatchResult> MapFromDtoToResultAsync(CreateMatchResultDto dto,
        IPlayersService playersService, CancellationToken cancellationToken)
    {
        return new MatchResult
        {
            FirstPlayer = (await playersService.GetByNameAsync(dto.FirstPlayerName, cancellationToken))!,
            SecondPlayer = (await playersService.GetByNameAsync(dto.SecondPlayerName, cancellationToken))!,
            WinnerPlayer = await playersService.GetByNameAsync(dto.WinnerName, cancellationToken),
            IsDraw = dto.IsDraw,
            MatchDateTime = dto.MatchDateTime
        };
    }
    
}