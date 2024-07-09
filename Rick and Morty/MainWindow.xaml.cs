using System.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RickAndMortyWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly RickAndMortyApiClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new RickAndMortyApiClient();
            LoadCharacters();
        }

        private async void LoadCharacters()
        {
            var characters = await _apiClient.GetCharactersAsync();
            CharactersListView.ItemsSource = characters.Results;
        }
    }
}
