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
using System.Net.Http;
using System.Diagnostics;

namespace ReizenPlanningProject.ViewModel
{
    class MainPageViewModel
    {

        private static readonly HttpClient _client = new HttpClient();
        private static readonly String _baseUrl = "https://localhost:44316/api/trip";
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
            var json = await _client.GetStringAsync(new Uri(_baseUrl));
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
            var stringContent = new StringContent(tripJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response =  await _client.PostAsync(new Uri(_baseUrl), stringContent);

            if (response.IsSuccessStatusCode)
            {
                Trips.Add(JsonConvert.DeserializeObject<Trip>(response.Content.ReadAsStringAsync().Result));
            } 
        }
    }
}
