using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using MovieApp.Models;
using System.Linq;
using System;

namespace MoviesApp
{
    public partial class FavoritesPage : ContentPage
    {
        private const string BaseUrl = "https://image.tmdb.org/t/p/w500"; // Para construir URLs completas de imágenes
        private readonly MovieServices _movieServices;

        public ObservableCollection<Movie> Movies { get; set; }
        private ObservableCollection<Movie> AllMovies { get; set; }

        // Constructor sin parámetros, necesario para la inicialización desde XAML
        public FavoritesPage()
        {
            InitializeComponent();
            // Inicializa MovieServices con un HttpClient predeterminado
            _movieServices = new MovieServices(new HttpClient());
            AllMovies = new ObservableCollection<Movie>();
            Movies = new ObservableCollection<Movie>();
            MoviesListView.ItemsSource = Movies;
            LoadMoviesAsync();
        }

        // Constructor que recibe una instancia de MovieServices
        public FavoritesPage(MovieServices movieServices) : this()
        {
            _movieServices = movieServices ?? throw new ArgumentNullException(nameof(movieServices));
        }

        private async Task LoadMoviesAsync()
        {
            try
            {
                var movieResponse = await _movieServices.GetMoviesAsyncPop();
                foreach (var movie in movieResponse.Results)
                {
                    // Agregar la URL completa para la imagen
                    movie.PosterPath = BaseUrl + movie.PosterPath;
                    Movies.Add(movie);
                    AllMovies.Add(movie); // Almacenar en la lista original
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones (puedes mostrar un mensaje de error al usuario)
                Console.WriteLine($"Error loading movies: {ex.Message}");
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
