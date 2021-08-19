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

        #region Fields 

        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _baseUrl = "https://localhost:44316/api/Account";

        #endregion

        #region Methods 

        public async Task<string> Login(LoginRequest request)
        {

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(new Uri(_baseUrl), stringContent);

            if(response.IsSuccessStatusCode) 
                return await response.Content.ReadAsStringAsync();

            return "";
        }


        public async Task<bool> CheckAvailableUserName(string email)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_baseUrl}/checkusername?email={email}");
            return response.IsSuccessStatusCode; 
        }

        public async Task<bool> Register(RegisterRequest request)
        {

            var requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"{_baseUrl}/register", stringContent);
            
            return response.IsSuccessStatusCode;
        }

        #endregion
    }
}
