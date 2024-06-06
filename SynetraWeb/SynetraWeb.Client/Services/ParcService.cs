using SynetraUtils.Models.DataManagement;
using SynetraWeb.Client.Authentications;
using SynetraWeb.Client.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace SynetraWeb.Client.Services
{
    public class ParcService
    {
        private IHttpClientFactory ClientFactory { get; }

        public ParcService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<List<Parc>> GetAllAsync()
        {
            List<Parc> parc = new List<Parc>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<Parc>>("api/Parcs");
            parc = userResponse.ToList();
            return parc;
        }

        public async Task<Parc> GetByIdAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            return await _httpClient.GetFromJsonAsync<Parc>($"api/Parcs/{id}");
        }

        public async Task CreateAsync(Parc parc)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PostAsJsonAsync("api/Parcs", parc);
        }

        public async Task UpdateAsync(Parc parc)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Parcs/{parc.Id}", parc);
        }

        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"api/Parcs/{id}");
        }
    }
}
