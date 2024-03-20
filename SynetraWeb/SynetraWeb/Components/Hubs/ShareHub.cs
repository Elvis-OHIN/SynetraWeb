using Microsoft.AspNetCore.SignalR;
using SynetraUtils.Models.DataManagement;
using SynetraWeb.Client.Models;

namespace SynetraWeb.Components.Hubs
{
    public class ShareHub : Hub
    {
        public Task ImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }
        public async Task SendMessage(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", title, message);
        }
        public async Task SendNetworkInfo(NetworkInfo networkInfo)
        {
            await Clients.All.SendAsync("ReceiveNetworkInfo", networkInfo);
        }
    }
}
