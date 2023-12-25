using System.ComponentModel.DataAnnotations;
using Application.Players.Dtos;
using Domain.Models;

namespace Application.Players;

public class PlayersService: IPlayersService
{
    private readonly IPlayersRepository _playersRepository;
    
    public PlayersService(IPlayersRepository playersRepository)
    {
        _playersRepository = playersRepository;
    }
    
    public async Task<IReadOnlyCollection<Player>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _playersRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Player?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _playersRepository.GetAsync(id, cancellationToken);
    }

    public async Task<Player?> GetByNameAsync(string? name, CancellationToken cancellationToken)
    {
        return await _playersRepository.GetByNameAsync(name, cancellationToken);
    }

    public async Task<int> CreateAsync(CreatePlayerDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateCreatePlayerDtoAsync(dto, cancellationToken);
        if(validationResult != ValidationResult.Success)
            throw new ValidationException(validationResult?.ErrorMessage);
        var player = new Player{ Name = dto.Name };
        return await _playersRepository.CreateAsync(player, cancellationToken);
    }
    
    public async Task<ValidationResult?> ValidateCreatePlayerDtoAsync(CreatePlayerDto dto, CancellationToken cancellationToken)
    {
        if (dto.Name.Length is <= 2 or >= 50)
            return new ValidationResult("Name must be between 2 and 50 characters long");
        var player = await _playersRepository.GetByNameAsync(dto.Name, cancellationToken);
        if(player is not null)
            return new ValidationResult("Player with this name already exists");
        return ValidationResult.Success;
        
    }
}