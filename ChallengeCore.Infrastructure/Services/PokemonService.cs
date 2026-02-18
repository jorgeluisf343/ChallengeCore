using ChallengeCore.Application.Dtos;
using ChallengeCore.Application.Interfaces;
using ChallengeCore.Domain.Exceptions;
using ChallengeCore.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ChallengeCore.Infrastructure.Services
{
    public class PokemonService(HttpClient httpClient, IOptions<PathImage> pathImage) : IPokemonService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly PathImage _pathImage = pathImage.Value;

        public async Task<List<PokemonDto>> GetImagesAll()
        {
            try
            {
                var response = await _httpClient.GetAsync(_pathImage.url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var images = new List<PokemonDto>();
                foreach (var pokemon in doc.RootElement.GetProperty("results").EnumerateArray())
                {
                    var url = pokemon.GetProperty("url").GetString();
                    var detailResponse = await _httpClient.GetStringAsync(url);
                    using var detailDoc = JsonDocument.Parse(detailResponse);
                    var sprites = detailDoc.RootElement.GetProperty("sprites");

                    //official-artwork
                    var official = sprites.GetProperty("other").GetProperty("official-artwork").GetProperty("front_default").GetString();
                    if (!string.IsNullOrEmpty(official) && (official.EndsWith(".png") || official.EndsWith(".jpg")))
                    {
                        images.Add(new PokemonDto()
                        {
                            Url = official
                        });
                    }
                }
                return images;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Ocurrio un error en el servicio de imagenes" + ex.Message);
            }
        }
    }
}
