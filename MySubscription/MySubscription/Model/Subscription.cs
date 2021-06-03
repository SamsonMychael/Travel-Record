using System;
using System.Collections.Generic;
using System.Text;

namespace MySubscription.Model
{
   public class Subscription
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Name{ get; set; }

       public DateTime SubscribedDate { get; set; }

        public bool IsActive { get; set; }

        public  string VenueName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public int Distance { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Subscription()
        {

        }
    }
}
