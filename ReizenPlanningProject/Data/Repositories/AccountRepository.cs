using Newtonsoft.Json;
using ReizenPlanningProject.Model.Authentication;
using ReizenPlanningProject.Model.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReizenPlanningProject.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/Account";


        public async Task<bool> LoginAsync(LoginRequest request)
        {

            Debug.WriteLine("login in repo");

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(new Uri(_baseUrl), stringContent);

            Debug.WriteLine(response.IsSuccessStatusCode); 

            return response.IsSuccessStatusCode; 
        }
    }
}
