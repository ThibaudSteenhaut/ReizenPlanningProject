using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace ReizenPlanningProject.Data.Repositories
{
    class CategoryRepository : ICategoryRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/Category/";

        public async Task<bool> Add(Category categoryToAdd)
        {
            var categoryJson = JsonConvert.SerializeObject(categoryToAdd);
            var stringContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(new Uri(_baseUrl), stringContent);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Toevoegen gelukt");
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteItemAsync(int categoryId, int itemId)
        {

            string urlExtra = $"{categoryId}/Item/{itemId}";
            string url = new Uri(_baseUrl) + urlExtra;
            HttpResponseMessage response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false; 
        }

        public ObservableCollection<Category> GetAllCategoriesWithItems()
        {
            var json = _client.GetStringAsync(new Uri(_baseUrl)).Result;
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
    
            return new ObservableCollection<Category>(categories.OrderByDescending(c => c.Name.Length));
        }
    }
}
