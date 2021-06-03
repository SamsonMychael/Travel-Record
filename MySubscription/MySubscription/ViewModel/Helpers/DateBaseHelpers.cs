using MySubscription.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MySubscription.ViewModel.Helpers
{
    public interface IFirestore
    {
        bool InsertSubscription(Subscription subscription);
        Task<bool> UpdateSubscription(Subscription subscription);
        Task<bool> DeleteSubscription(Subscription subscription);

        Task<IList<Subscription>> ReadSubscription();


    }
    public class DateBaseHelpers 
    {
        private static IFirestore firestore = DependencyService.Get<IFirestore>();
        public static Task<bool> DeleteSubscription(Subscription subscription)
        {
            return firestore.DeleteSubscription(subscription);
        }

        public static bool InsertSubscription(Subscription subscription)
        {
            try
            {
                return firestore.InsertSubscription(subscription);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Task<IList<Subscription>> ReadSubscription()
        {
            return firestore.ReadSubscription();
        }

        public static Task<bool> UpdateSubscription(Subscription subscription)
        {
            return firestore.UpdateSubscription(subscription);
        }
    }
}
