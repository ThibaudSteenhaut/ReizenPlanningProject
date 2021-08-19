using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.Domain;
using ReizenPlanningProject.Model.IRepositories;
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

namespace ReizenPlanningProject.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {

        #region Methods 

        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/items";

        #endregion

        #region Constructors 

        public ItemRepository()
        {

        }

        #endregion

        #region Methods 

        public ObservableCollection<Item> GetItems()
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync(_baseUrl).Result;
            var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(json);

            return items;
        }

        public async Task<int> Add(Item itemToAdd)
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var itemJson = JsonConvert.SerializeObject(itemToAdd);
            var stringContent = new StringContent(itemJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_baseUrl, stringContent);

            string itemId = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(itemId);
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> AddCategory(Category categoryToAdd)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var categoryJson = JsonConvert.SerializeObject(categoryToAdd);
            var stringContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/category", stringContent);

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

        public ObservableCollection<Category> GetCategories()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync($"{_baseUrl}/Categories").Result;
            var categories = JsonConvert.DeserializeObject<ObservableCollection<Category>>(json);

            return categories;
        }

        public async void Delete(int itemId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/{itemId}");
        }

        public async void DeleteCategoryWithItems(int categoryId)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            await _client.DeleteAsync($"{_baseUrl}/Category/{categoryId}");
        }

        #endregion

    }
}
