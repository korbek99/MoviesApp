using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoviesApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new HomePage());

            MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new HomePage()) { Title = "Home" },
                    new NavigationPage(new FavoritesPage()) { Title = "Favorites" },
                    new NavigationPage(new InfoPage()) { Title = "Info" }
                }
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
