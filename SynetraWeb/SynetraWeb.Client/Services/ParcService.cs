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
        private HttpClient _httpClient = new HttpClient();


        public async Task<List<Parc>> GetAllAsync()
        {
            List<Parc> parc = new List<Parc>();
            
            var userResponse = await _httpClient.GetFromJsonAsync<List<Parc>>("https://localhost:7082/api/Parcs");
            parc = userResponse.ToList();
            return parc;
        }

        public async Task<Parc> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Parc>($"https://localhost:7082/api/Parcs/{id}");
        }

        public async Task CreateAsync(Parc parc)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7082/api/Parcs", parc);
        }

        public async Task UpdateAsync(Parc parc)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Parcs/{parc.Id}", parc);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7082/api/Parcs/{id}");
        }
    }
}
