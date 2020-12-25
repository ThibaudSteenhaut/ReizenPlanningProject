using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                return true;
            }

            return false;
        }

        public ObservableCollection<Category> GetCategories()
        {
            var json = _client.GetStringAsync(new Uri(_baseUrl)).Result;
            var categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(json);

            return categories;
        }
    }
}
