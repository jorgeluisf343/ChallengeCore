using ChallengeCore.Api.Response;
using ChallengeCore.Application.Dtos;
using ChallengeCore.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController(PokemonService pokemonService) : ControllerBase
    {
        private readonly PokemonService _pokemonService = pokemonService;

        [HttpGet("images")]
        public async Task<IActionResult> GetImages()
        {
            var images = await _pokemonService.GetImagesAll();
            return Ok(new ApiResponse<List<PokemonDto>>(images));
        }
    }
}
