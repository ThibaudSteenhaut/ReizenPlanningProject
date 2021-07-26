using Newtonsoft.Json;
using ReizenPlanningProject.Model;
using ReizenPlanningProject.Model.IRepositories;
using ReizenPlanningProject.Vault;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/items";

        public ItemRepository()
        {
              
        }

        public ObservableCollection<Item> GetItems()
        {

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenVault.Token);
            var json = _client.GetStringAsync(_baseUrl).Result;
            var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(json);

            return items;
        }
    }
}
