using MySubscription.Model;
using MySubscription.ViewModel.Helpers;
using MySubscription.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MySubscription.ViewModel
{
    
   public class SubscriptionVM : INotifyPropertyChanged
    {
        private Subscription selectedName;

        public event PropertyChangedEventHandler PropertyChanged;

        public Subscription SelectedName
        {
            get { return selectedName; }
            set { 
                selectedName = value;
                OnPropertyChanged("SelectedName");
                if (selectedName != null)
                    App.Current.MainPage.Navigation.PushAsync(new UpdatePage(selectedName));
            }
        }

        public ObservableCollection<Subscription> Subscriptions { get; set; }

        public SubscriptionVM()
        {
            Subscriptions = new ObservableCollection<Subscription>();
        }
        public async void ReadSubscription()
        {
            var Get = await DateBaseHelpers.ReadSubscription();
            Subscriptions.Clear();
            foreach(var sa in Get)
            {
                Subscriptions.Add(sa);
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
