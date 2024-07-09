using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RickAndMortyApiClient
{
    private readonly HttpClient _httpClient;

    public RickAndMortyApiClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://rickandmortyapi.com/api/")
        };
    }

    public async Task<CharacterResponse> GetCharactersAsync(int page = 1)
    {
        var response = await _httpClient.GetStringAsync($"character/?page={page}");
        return JsonConvert.DeserializeObject<CharacterResponse>(response);
    }
}
