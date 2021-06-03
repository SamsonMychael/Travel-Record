using MySubscription.Model;
using MySubscription.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MySubscription.ViewModel
{
    public class SaveVM : INotifyPropertyChanged
    {

        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                
                OnPropertyChanged("Name");
            }
        }
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { 
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        private Venue selectedVenue;

        public Venue SelectedVenue
        {
            get { return selectedVenue; }
            set {
                selectedVenue = value;
                var firstcategory = selectedVenue.categories.FirstOrDefault();
                Name1 = selectedVenue.name;
                Address = selectedVenue.location.address;
                CategoryName = firstcategory.name;
                CategoryId = firstcategory.id;
                Longitude = selectedVenue.location.lng;
                Latitude = selectedVenue.location.lat;
                Distance = selectedVenue.location.distance;

                OnPropertyChanged("SelectedVenue");
            }
        }
        private string name1;

        public string Name1
        {
            get { return name1 ; }
            set {
                name1 = value;
                
                OnPropertyChanged("Name1");
            }
        }
        private string categoryId;

        public string CategoryId
        {
            get { return categoryId; }
            set {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }
        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        private int distance;

        public int Distance
        {
            get { return distance; }
            set {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }




        public ICommand SaveCommand { get; set; }
        public SaveVM()
        {
            SaveCommand = new Command(SavePlace, SaveCanExecute);
        }

        private bool SaveCanExecute(object arg)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void SavePlace(object obj)
        {
            if (SelectedVenue != null)
            {
                bool results = DateBaseHelpers.InsertSubscription(new Model.Subscription
                {
                    Name = Name,
                    IsActive = IsActive,
                   SubscribedDate = DateTime.Now,
                    VenueName = Name1,
                    CategoryId = CategoryId,
                    CategoryName = CategoryName,
                    Address = Address,
                    Longitude = Longitude,
                    Latitude = Latitude,
                    Distance = Distance,
                    UserId = Auth.GetCurrentUserId(),
                });

                if (results)
                    App.Current.MainPage.Navigation.PopAsync();
                else
                    App.Current.MainPage.DisplayAlert("Error", "Failed to be Inserted", "Ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
