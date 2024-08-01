using MovieApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class MovieService
{
    private readonly HttpClient _httpClient;

    // Constructor que recibe una instancia de HttpClient
    public MovieService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<MovieResponse> GetMoviesAsync()
    {
        try
        {
            // URL de la API de películas populares (ajusta según tu necesidad)
            //var url = "https://api.themoviedb.org/3/movie/popular?api_key=34738023d27013e6d1b995443764da44";
            var url = "https://api.themoviedb.org/3/movie/top_rated?api_key=34738023d27013e6d1b995443764da44";

            var response = await _httpClient.GetStringAsync(url);

            var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(response);

            return movieResponse;
        }
        catch (HttpRequestException httpEx)
        {

            Console.WriteLine($"HTTP Error: {httpEx.Message}");
            throw; // Rethrow or handle accordingly
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"JSON Error: {jsonEx.Message}");
            throw; // Rethrow or handle accordingly
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            throw; // Rethrow or handle accordingly
        }
    }

    public async Task<MovieResponse> GetMoviesAsyncPop()
    {
        try
        {
            // URL de la API de películas populares (ajusta según tu necesidad)
            var url = "https://api.themoviedb.org/3/movie/popular?api_key=34738023d27013e6d1b995443764da44";
            //var url = "https://api.themoviedb.org/3/movie/top_rated?api_key=34738023d27013e6d1b995443764da44";

            var response = await _httpClient.GetStringAsync(url);

            var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(response);

            return movieResponse;
        }
        catch (HttpRequestException httpEx)
        {

            Console.WriteLine($"HTTP Error: {httpEx.Message}");
            throw; // Rethrow or handle accordingly
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"JSON Error: {jsonEx.Message}");
            throw; // Rethrow or handle accordingly
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
            throw; // Rethrow or handle accordingly
        }
    }
}

