using Microsoft.AspNetCore.Identity;
using SynetraUtils.Models.DataManagement;
using System.Net.Http.Json;

namespace SynetraWeb.Client.Services
{
    public class RoleService
    {
        private IHttpClientFactory ClientFactory { get; }

        public RoleService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task CreateUserRoleAsync(IdentityUserRole<int> userRole)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PostAsJsonAsync($"api/Roles/User", userRole);
        }
        public async Task<List<IdentityRole<int>>> GetAllAsync()
        {
            List<IdentityRole<int>> roles = new List<IdentityRole<int>>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<IdentityRole<int>>>("api/Roles");
            roles = userResponse.ToList();
            return roles;
        }

    }
}
