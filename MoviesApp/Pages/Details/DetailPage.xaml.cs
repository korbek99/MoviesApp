using System;
using System.Collections.Generic;
using MovieApp.Models;
using Xamarin.Forms;

namespace MoviesApp
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(Movie movie)
        {
            InitializeComponent();

            // Configura los elementos de la página con los datos de la película
            MoviePoster.Source = movie.PosterPath;
            MovieTitle.Text = movie.OriginalTitle;
        }
    }
}


