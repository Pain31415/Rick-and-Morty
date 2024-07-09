using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace RickAndMortyWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly RickAndMortyApiClient _apiClient;
        private int _currentPage = 1;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new RickAndMortyApiClient();
            LoadData();
        }

        private async void LoadData()
        {
            await LoadCharacters(_currentPage);
            await LoadLocations();
            await LoadEpisodes();
        }

        private async Task LoadCharacters(int page)
        {
            var characters = await _apiClient.GetCharactersAsync(page);
            CharactersItemsControl.ItemsSource = characters.Results;
        }

        private async Task LoadLocations()
        {
            var locations = await _apiClient.GetLocationsAsync();
            LocationsItemsControl.ItemsSource = locations.Results;
        }

        private async Task LoadEpisodes()
        {
            var episodes = await _apiClient.GetEpisodesAsync();
            EpisodesItemsControl.ItemsSource = episodes.Results;
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            await LoadCharacters(_currentPage);
        }

        private async void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadCharacters(_currentPage);
            }
        }
    }

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

        public async Task<LocationResponse> GetLocationsAsync()
        {
            var response = await _httpClient.GetStringAsync("location");
            return JsonConvert.DeserializeObject<LocationResponse>(response);
        }

        public async Task<EpisodeResponse> GetEpisodesAsync()
        {
            var response = await _httpClient.GetStringAsync("episode");
            return JsonConvert.DeserializeObject<EpisodeResponse>(response);
        }
    }

    public class Info
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("prev")]
        public string Prev { get; set; }
    }

    public class Origin
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("dimension")]
        public string Dimension { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class LocationResponse
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("results")]
        public List<Location> Results { get; set; }
    }

    public class Character
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("origin")]
        public Origin Origin { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("episode")]
        public List<string> Episode { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }

    public class CharacterResponse
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("results")]
        public List<Character> Results { get; set; }
    }

    public class Episode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("air_date")]
        public string AirDate { get; set; }

        [JsonProperty("episode")]
        public string EpisodeCode { get; set; }

        [JsonProperty("characters")]
        public List<string> Characters { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class EpisodeResponse
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("results")]
        public List<Episode> Results { get; set; }
    }
}