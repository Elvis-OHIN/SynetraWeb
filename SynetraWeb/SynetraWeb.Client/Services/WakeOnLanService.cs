using SynetraUtils.Models.MessageManagement;
using System.Net.Http.Json;

namespace SynetraWeb.Client.Services
{
    public class WakeOnLanService
    {
        private IHttpClientFactory ClientFactory { get; }

        public WakeOnLanService(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory;
        }

        public async Task<string> SendWakeOnLanAsync(WakeRequest wakeRequest)
        {
            HttpClient _httpClient = ClientFactory.CreateClient("Auth");
            var response = await _httpClient.PostAsJsonAsync("api/WakeOnLan", wakeRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }
        }
    }
}
