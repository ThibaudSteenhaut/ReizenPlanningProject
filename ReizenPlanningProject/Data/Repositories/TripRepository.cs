using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
            HttpResponseMessage response = await _client.PostAsync(new Uri(_baseUrl), stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public ObservableCollection<Trip> GetTrips()
        {

            var json = _client.GetStringAsync(new Uri(_baseUrl)).Result;
            var trips = JsonConvert.DeserializeObject<ObservableCollection<Trip>>(json);

            return trips;
        }

        public Task<Trip> GetTripByDestinationAsync(string destination)
        {
            throw new NotImplementedException();
        }
    }
}
