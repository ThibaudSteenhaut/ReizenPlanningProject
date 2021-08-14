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

        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/trip";


        public TripRepository()
        {

        }

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

        #region Activity 

        public List<Domain.Activity> GetActivities(int tripId)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/{tripId}/Activities").Result;
            var activities = JsonConvert.DeserializeObject<List<Domain.Activity>>(json);

            return activities;

        }

        public Task<int> AddActivity(int tripId, Domain.Activity activity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
