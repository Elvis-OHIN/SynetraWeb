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
            var userResponse = await _httpClient.GetFromJsonAsync<List<Room>>("api/Rooms");
            room = userResponse.ToList();
            return room;
        }
        public async Task<List<Room>> GetAllByParcAsync(int id)
        {
            List<Room> room = new List<Room>();
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var userResponse = await _httpClient.GetFromJsonAsync<List<Room>>($"api/Rooms/Parc/{id}");
            room = userResponse.ToList();
            return room;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            return await _httpClient.GetFromJsonAsync<Room>($"api/Rooms/{id}");
        }

        public async Task CreateAsync(Room room)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PostAsJsonAsync("api/Rooms", room);
        }

        public async Task UpdateAsync(Room room)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.PutAsJsonAsync($"api/Rooms/{room.Id}", room);
        }

        public async Task DeleteAsync(int id)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            await _httpClient.DeleteAsync($"api/Rooms/{id}");
        }
    }
}
