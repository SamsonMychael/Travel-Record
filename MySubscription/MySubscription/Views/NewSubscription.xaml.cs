using MySubscription.Logic;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySubscription.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSubscription : ContentPage
    {
        public NewSubscription()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {

            getting.IsVisible = true;
            getting.IsRunning = true;

            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await VenueLogic.GetVenues(position.Longitude , position.Latitude);
            getting.IsVisible = false;
            getting.IsRunning = false;

            VenueListView.ItemsSource = venues;
        } 
    }
}