using SynetraUtils.Models.DataManagement;
using System.Net.Http.Json;

namespace SynetraWeb.Client.Services
{
    public class RoomService
    {
       
        private IHttpClientFactory ClientFactory { get; }

        public RoomService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }
        public async Task<List<Room>> GetAllAsync()
        {
            List<Room> room = new List<Room>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<Room>>("https://localhost:7082/api/Rooms");
            room = userResponse.ToList();
            return room;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            return await _httpClient.GetFromJsonAsync<Room>($"https://localhost:7082/api/Rooms/{id}");
        }

        public async Task CreateAsync(Room room)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PostAsJsonAsync("https://localhost:7082/api/Rooms", room);
        }

        public async Task UpdateAsync(Room room)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Rooms/{room.Id}", room);
        }

        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"https://localhost:7082/api/Rooms/{id}");
        }
    }
}
