using SynetraUtils.Models.DataManagement;
using SynetraWeb.Client.Models;
using System.Net;
using System.Net.Http.Json;

namespace SynetraWeb.Client.Services
{
    public class NetworkService
    {
        private IHttpClientFactory ClientFactory { get; }

        public NetworkService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<List<NetworkInfo>> GetAllAsync()
        {
            List<NetworkInfo> network = new List<NetworkInfo>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<NetworkInfo>>("api/Network");
            network = userResponse.ToList();
            return network;
        }
        
        public async Task<NetworkInfo> GetByIdAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            return await _httpClient.GetFromJsonAsync<NetworkInfo>($"api/Network/{id}");
        }

        public async Task CreateAsync(NetworkInfo networkInfo)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PostAsJsonAsync("api/Network", networkInfo);
        }

        public async Task UpdateAsync(NetworkInfo networkInfo)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Network/{networkInfo.Id}", networkInfo);
        }

        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"api/Network/{id}");
        }
    }
}
