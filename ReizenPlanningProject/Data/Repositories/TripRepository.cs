using Newtonsoft.Json;
using ReizenPlanningProject.Model.Domain;
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

        #region Fields 

        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/trip";

        #endregion

        #region Constructors 

        public TripRepository()
        {

        }

        #endregion

        #region Methods 

        #region Trip

        public async Task<int> Add(Trip tripToAdd)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var tripJson = JsonConvert.SerializeObject(tripToAdd);
            var stringContent = new StringContent(tripJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, stringContent);

            string tripId = await response.Content.ReadAsStringAsync();

            //na toevoegen van een nieuwe trip stuurt de backend de id terug, 
            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(tripId);
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> Remove(int tripId)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
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


        public ObservableCollection<Trip> GetPastTrips()
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/Past").Result;
            var trips = JsonConvert.DeserializeObject<ObservableCollection<Trip>>(json);

            return trips;
        }

        #endregion

        #region TripItem

        public ObservableCollection<TripItem> GetTripItems(int tripId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/{tripId}/Tripitems").Result;
            var items = JsonConvert.DeserializeObject<ObservableCollection<TripItem>>(json);

            return items;
        }

        public async void UpdateTripItems(List<TripItem> tripItems)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var tripItemsJson = JsonConvert.SerializeObject(tripItems);
            var stringContent = new StringContent(tripItemsJson, Encoding.UTF8, "application/json");
            await _client.PutAsync($"{_baseUrl}/TripItems", stringContent);
        }

        public async Task<int> AddTripItem(int tripId, TripItem tripItem)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var tripItemJson = JsonConvert.SerializeObject(tripItem);
            var stringContent = new StringContent(tripItemJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/{tripId}/TripItems", stringContent);

            string categoryId = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(categoryId);
            }
            else
            {
                return -1;
            }
        }

        public async void DeleteTripItem(int tripItemId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/TripItems/{tripItemId}");
        }

        #endregion

        #region TripCategory 

        public List<Category> GetTripCategories(int tripId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/{tripId}/Categories").Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(json);

            return categories;

        }

        public async Task<int> AddTripCategory(int tripId, Category category)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var categoryJson = JsonConvert.SerializeObject(category);
            var stringContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/{tripId}/Category", stringContent);

            string categoryId = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(categoryId);
            }
            else
            {
                return -1;
            }
        }

        public async void DeleteCategoryWithItems(int categoryId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/Category/{categoryId}");
        }

        #endregion

        #region TripTask 

        public ObservableCollection<TripTask> GetTripTasks(int tripId)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/{tripId}/TripTasks").Result;
            var tripTasks = JsonConvert.DeserializeObject<ObservableCollection<TripTask>>(json);

            return tripTasks;

        }

        public async Task<int> AddTripTask(int tripId, TripTask tripTask)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var tripTaskJson = JsonConvert.SerializeObject(tripTask);
            var stringContent = new StringContent(tripTaskJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/{tripId}/TripTask", stringContent);

            string tripTaskId = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(tripTaskId);
            }
            else
            {
                return -1;
            }
        }

        public async void DeleteTripTask(int tripTaskId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/TripTask/{tripTaskId}");
        }

        public async void UpdateTripTasks(IEnumerable<TripTask> tripTasks)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var tripTaskjson = JsonConvert.SerializeObject(tripTasks);
            var stringContent = new StringContent(tripTaskjson, Encoding.UTF8, "application/json");
            await _client.PutAsync($"{_baseUrl}/TripTasks", stringContent);
        }

        #endregion

        #region ItineraryItem

        public ObservableCollection<ItineraryItem> GetItineraryItems(int tripId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/{tripId}/ItineraryItems").Result;
            var itineraryItems = JsonConvert.DeserializeObject<ObservableCollection<ItineraryItem>>(json);

            return itineraryItems;
        }

        public async Task<int> AddItineraryItem(int tripId, ItineraryItem itineraryItem)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var iiJson = JsonConvert.SerializeObject(itineraryItem);
            var stringContent = new StringContent(iiJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/{tripId}/ItineraryItem", stringContent);

            string iiId = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(iiId);
            }
            else
            {
                return -1;
            }
        }

        public async void DeleteItineraryItem(int id)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/ItineraryItem/{id}");
        }

        #endregion

        #endregion
    }
}
