using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using MovieApp.Models;
using System.Linq; // Para usar el método `Where`


namespace MoviesApp
{
    public partial class HomePage : ContentPage
    {
        private const string ApiKey = "34738023d27013e6d1b995443764da44";
        private const string BaseUrl = "https://image.tmdb.org/t/p/w500"; // Para construir URLs completas de imágenes

        public ObservableCollection<Movie> Movies { get; set; }
        private ObservableCollection<Movie> AllMovies { get; set; } // Para mantener la lista original

        public HomePage()
        {
            InitializeComponent();
            Movies = new ObservableCollection<Movie>();
            AllMovies = new ObservableCollection<Movie>();
            MoviesListView.ItemsSource = Movies;
            LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}");

            var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(response);
            foreach (var movie in movieResponse.Results)
            {
                // Agregar la URL completa para la imagen
                movie.PosterPath = BaseUrl + movie.PosterPath;
                Movies.Add(movie);
                AllMovies.Add(movie); // Almacenar en la lista original
            }
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue.ToLower();

            // Filtrar la lista de películas según el texto de búsqueda
            var filteredMovies = AllMovies.Where(movie => movie.OriginalTitle.ToLower().Contains(searchText)).ToList();

            // Limpiar la lista actual y agregar los resultados filtrados
            Movies.Clear();
            foreach (var movie in filteredMovies)
            {
                Movies.Add(movie);
            }
        }

        private async void OnMovieTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && e.Item is Movie selectedMovie)
            {
                // Navegar a la página de detalles y pasar el objeto Movie seleccionado
                await Navigation.PushAsync(new DetailPage(selectedMovie));
            }
        }
    }
}
