using MySubscription.ViewModel;
using MySubscription.ViewModel.Helpers;
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
    public partial class SubscriptionPage : ContentPage
    {

        SubscriptionVM vm;
        public SubscriptionPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as SubscriptionVM;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
               await Navigation.PushAsync(new LoginPage());
                    }
            else
            {
                vm.ReadSubscription();
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewSubscription());
        }

        private  void ListView_Refreshing(object sender, EventArgs e)
        {
             
            SubscriptionListView.IsRefreshing = false;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutUsPage());
        }
    }
}