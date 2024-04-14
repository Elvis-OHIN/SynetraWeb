using SynetraWeb.Client.Models;
using System.Net.Http.Json;
using SynetraUtils.Models.DataManagement;
using System.Net.Http;

namespace SynetraWeb.Client.Services
{
    public class ComputerService
    {
        private HttpClient _httpClient = new HttpClient();


        public async Task<List<Computer>> GetAllAsync()
        {
            List<Computer> Computer = new List<Computer>();

            var userResponse = await _httpClient.GetFromJsonAsync<List<Computer>>("https://localhost:7082/api/Computers");
            Computer = userResponse.ToList();
            return Computer;
        }

        public async Task<Computer> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Computer>($"https://localhost:7082/api/Computers/{id}");
        }
        public async Task<Computer> GetByFootPrintAsync(string footString)
        {
           
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7082/api/Computers/FootPrint/{footString}");
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<Computer> GetByConnexionAsync(string connexion)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7082/api/Computers/Connection/{connexion}");
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<Computer> CreateConnexionAsync(int id,Connection connection)
        {
            HttpResponseMessage response =  await _httpClient.PostAsJsonAsync($"https://localhost:7082/api/Computers/Connection/{id}",connection);
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }
        public async Task<ShareScreen> GetScreenAsync()
        {
            return await _httpClient.GetFromJsonAsync<ShareScreen>($"https://localhost:7082/api/ShareScreens/8");
        }

        public async Task<Computer> CreateAsync(Computer Computer)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7082/api/Computers", Computer);
            if (response.IsSuccessStatusCode)
            {
                Computer jsonResponse = await response.Content.ReadFromJsonAsync<Computer>();
                return jsonResponse;
            }
            return null;
        }

        public async Task UpdateAsync(Computer Computer)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Computers/{Computer.Id}", Computer);
        }
        public async Task UpdateFootPrintAsync(int id , string footString)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Computers/FootPrint/{3}?footPrint={footString}", footString);
        }
        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7082/api/Computers/{id}");
        }
    }
}
