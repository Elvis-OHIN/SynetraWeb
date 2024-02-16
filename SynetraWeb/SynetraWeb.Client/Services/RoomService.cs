using SynetraUtils.Models.DataManagement;
using System.Net.Http.Json;

namespace SynetraWeb.Client.Services
{
    public class RoomService
    {
        private HttpClient _httpClient = new HttpClient();

        public async Task<List<Room>> GetAllAsync()
        {
            List<Room> room = new List<Room>();

            var userResponse = await _httpClient.GetFromJsonAsync<List<Room>>("https://localhost:7082/api/Rooms");
            room = userResponse.ToList();
            return room;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Room>($"https://localhost:7082/api/Rooms/{id}");
        }

        public async Task CreateAsync(Room room)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7082/api/Rooms", room);
        }

        public async Task UpdateAsync(Room room)
        {
            await _httpClient.PutAsJsonAsync($"https://localhost:7082/api/Rooms/{room.Id}", room);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7082/api/Rooms/{id}");
        }
    }
}
