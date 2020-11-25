using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Web.Http;
using HttpClient = System.Net.Http.HttpClient;

namespace ReizenPlanningProject.ViewModel
{
    class MainPageViewModel
    {
        public ObservableCollection<Trip> Trips { get; set; }

        public RelayCommand AddTripCommand { get; set; }

        

        public MainPageViewModel()
        {
            //this.Trips = new ObservableCollection<Trip>(DummyDataSource.Trips);
            AddTripCommand = new RelayCommand((param) => AddTripAsync(param));

            Trips = new ObservableCollection<Trip>();
            LoadTrips();

        }


        private async void LoadTrips()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync(new Uri("https://localhost:44316/api/trip"));
            var lst = JsonConvert.DeserializeObject<ObservableCollection<Trip>>(json);
            foreach (Trip t in lst)
            {
                this.Trips.Add(t);
            }
        }

        private async Task AddTripAsync(object p)
        {

            Trip trip = new Trip() { Destination = "Brussel", DepartureDate = new DateTime(2022, 12, 30), ReturnDate = new DateTime(2023, 1, 10) };
            var tripJson = JsonConvert.SerializeObject(trip);

            Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient();
            var res = await client.PostAsync(new Uri("https://localhost:44316/api/trip"),
                new HttpStringContent(tripJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
                Trips.Add(JsonConvert.DeserializeObject<Trip>(res.Content.ToString()));
            }
           
        }
    }
}
