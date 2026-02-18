using ChallengeCore.Application.Dtos;

namespace ChallengeCore.Application.Interfaces
{
    public interface IPokemonService
    {
        Task<List<PokemonDto>> GetImagesAll();
    }
}
