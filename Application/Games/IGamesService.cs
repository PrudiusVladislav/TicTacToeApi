using System.ComponentModel.DataAnnotations;
using Application.Games.Dtos;

namespace Application.Games;

public interface IGamesService
{
    Task<IReadOnlyCollection<SlimMatchResultDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<SlimMatchResultDto?> GetAsync(int id, CancellationToken cancellationToken);
    Task<int> CreateAsync(CreateMatchResultDto dto, CancellationToken cancellationToken);
    Task<ValidationResult?> ValidateCreateMatchResultDtoAsync(CreateMatchResultDto dto, CancellationToken cancellationToken);
}