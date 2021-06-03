using Foundation;
using MySubscription.Model;
using MySubscription.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly : Dependency(typeof(MySubscription.iOS.Dependencies.Firestore))]
namespace MySubscription.iOS.Dependencies
{
    public class Firestore : IFirestore
    {
        public Task<bool> DeleteSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public bool InsertSubscription(Subscription subscription)
        {
            try
            {


                var keys = new[]
                {
                new NSString("author"),
                new NSString("name"),
                new NSString("isActive"),
                new NSString("subscribedDate"),
            };

                var values = new NSObject[]
                {
                new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                new NSString(subscription.Name),
                new NSNumber(subscription.IsActive),
                DateTimeToNsDate(subscription.SubscribedDate),
                };
                var subscriptiondoc = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscription").AddDocument(subscriptiondoc);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IList<Subscription>> ReadSubscription()
        {
            var collections = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscription");
            var qeury = collections.WhereEqualsTo("author", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
            var document = await collections.GetDocumentsAsync();

            List<Subscription> subscription = new List<Subscription>();

            foreach(var doc in document.Documents)
            {
                var docs = doc.Data;
                var subscriptions = new Subscription
                {
                    IsActive = (bool)(docs.ValueForKey(new NSString("isActive")) as NSNumber),
                    Name = docs.ValueForKey(new NSString("name")) as NSString,
                    UserId = docs.ValueForKey(new NSString("author")) as NSString,
                    SubscribedDate = FireDate(docs.ValueForKey(new NSString("subscribeddate")) as Firebase.CloudFirestore.Timestamp)
                };
                subscription.Add(subscriptions);
            }
            return subscription;
        }

        public Task<bool> UpdateSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }
        private static NSDate DateTimeToNsDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }
        private static DateTime FireDate(Firebase.CloudFirestore.Timestamp date)
        {
            DateTime dayday = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return dayday.AddSeconds(date.Seconds);
        }
    }
}