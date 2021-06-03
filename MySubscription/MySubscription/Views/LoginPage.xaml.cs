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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void login_Tapped(object sender, EventArgs e)
        {
            loginStackLayout.IsVisible = true;
           RegisterStackLayout.IsVisible = false;
        }

        private void SignUp_Tapped(object sender, EventArgs e)
        {
            loginStackLayout.IsVisible = false;
            RegisterStackLayout.IsVisible = true;
        }
      
    }
}