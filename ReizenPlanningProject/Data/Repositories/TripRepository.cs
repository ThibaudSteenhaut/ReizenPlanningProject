using Newtonsoft.Json;
using ReizenPlanningProject.Vault;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Model.Repositories
{
    public class TripRepository : ITripRepository
    {

        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/trip";
        

        public TripRepository()
        {

        }

        public async Task<bool> Add(Trip tripToAdd)
        {

            var tripJson = JsonConvert.SerializeObject(tripToAdd);
            var stringContent = new StringContent(tripJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, stringContent);

            return response.IsSuccessStatusCode; 
        }

        public async Task<bool> Remove(int tripId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_baseUrl}/{tripId}");
            return response.IsSuccessStatusCode; 
        }

        public ObservableCollection<Trip> GetTrips()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync(_baseUrl).Result;
            var trips = JsonConvert.DeserializeObject<ObservableCollection<Trip>>(json);
            return trips;
        }
    }
}
