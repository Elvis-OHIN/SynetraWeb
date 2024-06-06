using Microsoft.AspNetCore.Identity;
using SynetraUtils.Models.DataManagement;
using System.Net.Http.Json;
using System.Security.Principal;

namespace SynetraWeb.Client.Services
{
    public class UserService
    {
        private IHttpClientFactory ClientFactory { get; }

        public UserService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<User>>("api/Users");
            users = userResponse.ToList();
            return users;
        }

        public async Task<List<User>> GetAllExceptCurrentUserAsync(string email)
        {
            List<User> users = new List<User>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<User>>($"api/Users/Except/{email}");
            users = userResponse.ToList();
            return users;
        }
        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"api/Users/{id}");
        }
        public async Task UpdateAsync(User user)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Users/{user.Id}", user);
        }
        public async Task<int> CreateAsync(User user)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var result = await _httpClient.PostAsJsonAsync($"api/Users" , user);
            if (result != null)
            {
               return await result.Content.ReadFromJsonAsync<int>();
            }
            return 0;
        }
       
        public async Task<List<IdentityUserRole<int>>> GetAllRoleAsync()
        {
            List<IdentityUserRole<int>> userRole = new List<IdentityUserRole<int>>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<IdentityUserRole<int>>>("api/Users/Role");
            userRole = userResponse.ToList();
            return userRole;
        }
    }
}
