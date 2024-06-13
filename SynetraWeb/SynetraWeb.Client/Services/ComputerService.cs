using SynetraWeb.Client.Models;
using System.Net.Http.Json;
using SynetraUtils.Models.DataManagement;
using System.Net.Http;

namespace SynetraWeb.Client.Services
{
    public class ComputerService
    {
        private IHttpClientFactory ClientFactory { get; }

        public ComputerService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<List<Computer>> GetAllAsync()
        {
            List<Computer> Computer = new List<Computer>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<Computer>>("api/Computers");
            Computer = userResponse.ToList();
            return Computer;
        }
        public async Task<List<Computer>> GetAllByParcAsync(int id)
        {
            List<Computer> computers = new List<Computer>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var Response = await _httpClient.GetFromJsonAsync<List<Computer>>($"api/Computers/Parc/{id}");
            computers =  Response.ToList();
            return computers;
        }
        public async Task<Computer> GetByIdAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            return await _httpClient.GetFromJsonAsync<Computer>($"api/Computers/{id}");
        }
        public async Task<Computer> GetByFootPrintAsync(string footString)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Computers/FootPrint/{footString}");
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<Computer> GetByConnexionAsync(int connexion)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Computers/Connection/{connexion}");
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<Computer> GetByConnexionIdAsync(string connexion)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Computers/Statut/{connexion}");
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<Computer> CreateConnexionAsync(int id,Connection connection)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            HttpResponseMessage response =  await _httpClient.PutAsJsonAsync($"api/Computers/Connection/{id}",connection);
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }

        public async Task<Computer> CreateAsync(Computer Computer)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Computers", Computer);
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }

        public async Task UpdateAsync(Computer Computer)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Computers/{Computer.Id}", Computer);
        }
        public async Task UpdateFootPrintAsync(int id , string footString)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Computers/FootPrint/{3}?footPrint={footString}", footString);
        }
        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"api/Computers/{id}");
        }
    }
}
