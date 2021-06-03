using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using MySubscription.Model;
using MySubscription.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly : Dependency(typeof(MySubscription.Droid.Dependencies.Firestore))]
namespace MySubscription.Droid.Dependencies
{
    public class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        public IntPtr Handle => throw new NotImplementedException();

        List<Subscription> Subscriptions;

        bool hasReadSubscription = false;
        public Firestore()
        {
            Subscriptions = new List<Subscription>();
        }

        public async Task<bool> DeleteSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                collection.Document(subscription.Id).Delete();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool InsertSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                var subscriptionDocument = new JavaDictionary<string, Java.Lang.Object>
            {
                {"author" , Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                {"name" , subscription.Name },
                {"isActive" , subscription.IsActive },
                {"VenueName" , subscription.VenueName },
                    {"CategoryId" , subscription.CategoryId },
                    { "CategoryName" , subscription.CategoryName},
                    {"Address" , subscription.Address },
                    {"Distance" , subscription.Distance },
                    {"Longitude" , subscription.Longitude },
                    {"Latitude" ,subscription.Latitude }
             

            };
                collection.Add(subscriptionDocument);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Subscription>> ReadSubscription()
        {
            try
            {
                hasReadSubscription = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                var query = collection.WhereEqualTo("author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);

                for (int i = 0; i < 25; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasReadSubscription)
                        break;
                }
                return Subscriptions;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                collection.Document(subscription.Id).Update("name", subscription.Name, "isActive", subscription.IsActive);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
   

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
           if(task.IsSuccessful)
            {
                var documents = (QuerySnapshot) task.Result;


                Subscriptions.Clear();
                foreach(var doc in documents.Documents)
                {
                    Subscription subscription = new Subscription
                    {
                        IsActive = (bool)doc.Get("isActive"),

                        Name = doc.Get("name").ToString(),

                        UserId = doc.Get("author").ToString(),

                        VenueName = doc.Get("VenueName").ToString(),

                       CategoryName = doc.Get("CategoryName").ToString(),

                        CategoryId = doc.Get("CategoryId").ToString(),

                      //  Address = doc.Get("Address").ToString(),

                        Distance = (int)doc.Get("Distance"),

                        Longitude = (double)doc.Get("Longitude"),

                        Latitude = (double)doc.Get("Latitude"),

                        Id = doc.Id
                       

                    };
                    Subscriptions.Add(subscription);
                    
                }
                hasReadSubscription = true;

            }
           else
            {
                Subscriptions.Clear();
            }
           
        }
    }
}
        


       
       
    
