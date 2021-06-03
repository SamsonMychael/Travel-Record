using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySubscription.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }
        public ObservableCollection<About> MyAbout { get => GetAbouts(); }

        public ObservableCollection<About> GetAbouts()
        {
            return new ObservableCollection<About>
                {
                new About { Name = "Description", Color = "#B96CBD", Click = "See more", Number = 1, 
                    detail = new ObservableCollection<Details>{ new Details { Number =" 1", Topic = "Welcome to Travel Record App", Color = "#B96CBD"},
                        new Details{ Number = "2", Topic = "This App makes easy to find the location near by you" } ,
                        new Details{ Number = "3",  Topic ="This App is not only for Travellers ,You have use this app for Directions and more..."} } },


                 new About { Name = "Specifications", Color = "Blue", Click = "See more", Number = 2,
                    detail = new ObservableCollection<Details>{ new Details { Number = "1", Topic = "Used for locating the current locations",Color = "#B96CBD" },
                        new Details{  Number = "2",Topic= "For searching places nearby you, Save the location You visited" },
                        new Details{  Number = "3",Topic="And write about your experience"} } },


                  new About { Name = "Features", Color = "yellow", Click = "See more", Number =3,
                    detail = new ObservableCollection<Details>{ new Details {Number = "1", Topic = "For Access the current location",Color = "#B96CBD" }
                    ,new Details{  Number = "2", Topic=" Add or Save the Locations"  }
                    , new Details{  Number = "3",Topic=" Mark the places is you're visited"} ,
                    new Details{ Number = "4",Topic = "Rate This App"} } },



                   new About { Name = "Help", Color = "black", Click = "See more", Number = 4,
                    detail = new ObservableCollection<Details>{ new Details {Topic = "Mail : asamsonmichael@gmail.com"}
                   } },


            };
        }
    }
    public class About
    {
        public string Name { get; set; }

        public int Number { get; set; }

       

        public  string Click { get; set; }
        public string Color { get; set; }

        public ObservableCollection<Details> detail { get; set; }
    }

    public class Details
    {
        public string Topic { get; set; }

        public string Number { get; set; }
        public string Color { get; set; }
    }
}