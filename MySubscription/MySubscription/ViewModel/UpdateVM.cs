using MySubscription.Model;
using MySubscription.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    public class UpdateVM : INotifyPropertyChanged
    {
        private Subscription subscription;

        public Subscription Subscription
        {
            get { return subscription; }
            set {
                subscription = value;
                Name = subscription.Name;
                IsActive = subscription.IsActive;
                VenueName = subscription.VenueName;
                Address = subscription.Address;
                Latitude = subscription.Latitude;
                Longitude = subscription.Longitude;
                Distance = subscription.Distance;
                
                   
                OnPropertyChanged("Subcription");

            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                Subscription.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("Subscription");
            }
        }
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set {
                isActive = value;
                Subscription.IsActive = isActive;
                OnPropertyChanged("IsActive");
                OnPropertyChanged("Subscription");
            }
        }
        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set {
                venueName = value;
                subscription.VenueName = venueName;
                OnPropertyChanged("Subscription");
                OnPropertyChanged("VenueName");
            }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set {
                address = value;
                subscription.Address = address;
                OnPropertyChanged("Address");
                OnPropertyChanged("Subscription");
            }
        }
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { 
                latitude = value;
                subscription.Latitude = latitude;
                OnPropertyChanged("Latitude");
                OnPropertyChanged("Subscription");
            }
        }
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { 
                longitude = value;
                subscription.Longitude = longitude;
                OnPropertyChanged("Longitude");
                OnPropertyChanged("Subscription");
            }
        }
        private int distance;

        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                subscription.Distance = distance;
                OnPropertyChanged("Distance");
                OnPropertyChanged("Subscription");
            }
        }





        public ICommand UpdateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public UpdateVM()
        {
            UpdateCommand = new Command(Update, UpdateCanexecute);
            DeleteCommand = new Command(Delete);
        }

        private async void Delete(object obj)
        {
           bool results = await DateBaseHelpers.DeleteSubscription(Subscription);
            if (results)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Please Try Aagin", "Ok");
        }

        private bool UpdateCanexecute(object arg)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private async void Update(object obj)
        {
            bool results = await DateBaseHelpers.UpdateSubscription(Subscription);
            if (results)
              await  App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error" , "Please Try Aagin" ,"Ok");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
