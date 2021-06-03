using MySubscription.Model;
using MySubscription.ViewModel;
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
    public partial class UpdatePage : ContentPage
    {
        UpdateVM vm;
        public UpdatePage()
        {
            InitializeComponent();
            vm = Resources["vm"] as UpdateVM;
        }
        public UpdatePage(Subscription selectedName)
        {
            InitializeComponent();
            vm = Resources["vm"] as UpdateVM;
            vm.Subscription = selectedName;
        }
    }
}