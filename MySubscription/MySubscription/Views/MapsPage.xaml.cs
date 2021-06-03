

using MySubscription.ViewModel;
using MySubscription.ViewModel.Helpers;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
    public partial class MapsPage : ContentPage
    {
        bool haslocationpermission = false;
        public MapsPage()
        {
            InitializeComponent();
            GetPermission();
        }
        private async void GetPermission()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    await DisplayAlert("Sorry", "We need Location's permission", "Okay");
                }

                var result = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
                if (result == PermissionStatus.Granted)
                {
                    status = result;
                }
            }
            if (status == PermissionStatus.Granted)
            {
                locationmap.IsShowingUser = true;
                haslocationpermission = true;
               
            }
        }
        protected override void OnAppearing()
        {
            if (haslocationpermission)
            {
                base.OnAppearing();
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                locator.StartListeningAsync(TimeSpan.Zero, 100);
            }
            GetLocation();
            DisplayPins();
        }

        private async void DisplayPins()
        {
            try
            {
                var details = await DateBaseHelpers.ReadSubscription();

                foreach (var pin in details)
                {
                    var position = new Xamarin.Forms.Maps.Position(pin.Latitude, pin.Longitude);
                    var pins = new Xamarin.Forms.Maps.Pin()
                    {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = pin.VenueName,
                        Address = pin.Address
                    };
                    locationmap.Pins.Add(pins);
                }
            }
            catch(Exception ex)
            {
               await DisplayAlert("Something wrong", "Please try again later", "Ok");
            }
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            MoveMap(e.Position); 
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var locator = CrossGeolocator.Current;
            locator.PositionChanged -= Locator_PositionChanged;
            locator.StopListeningAsync();
        }
        public async void GetLocation()
        {
            if (haslocationpermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();


                MoveMap(position);
            }
            else
            {
                await DisplayAlert("Error", "Need Permission to access location", "Ok");
            }
        }

      

        private void MoveMap(Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationmap.MoveToRegion(span);
        }
    }
}