using SynetraWeb.Client.Models;
using System.Net.Http.Json;
using SynetraUtils.Models.DataManagement;

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
        public async Task<ShareScreen> GetScreenAsync()
        {
            return await _httpClient.GetFromJsonAsync<ShareScreen>($"https://localhost:7082/api/ShareScreens/8");
        }

        public async Task CreateAsync(Computer Computer)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7082/api/Computers", Computer);
        }

        public async Task UpdateAsync(Computer Computer)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Computers/{Computer.Id}", Computer);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7082/api/Computers/{id}");
        }
    }
}
