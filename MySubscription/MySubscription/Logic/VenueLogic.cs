using MySubscription.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MySubscription.Logic
{
   public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues(double latitude , double longitude)
        {
            List<Venue> venues = new List<Venue>();

            var url = VenueRoot.GenerateURL(latitude, longitude);

            using(HttpClient client = new HttpClient())
            {
                var responce =await client.GetAsync(url);
                var json = await responce.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.response.venues as List<Venue>;
            }



            return venues;
        }
    }
}
