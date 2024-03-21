using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
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
        public async Task SendMouseMovement(double x, double y)
        {
            await Clients.Others.SendAsync("ReceiveMouseMovement", x, y);
        }
        public async Task SendKeyPress(string key)
        {
            await Clients.Others.SendAsync("ReceiveKeyPress", key);
        }
        public async Task SendClickPress(string key)
        {
            await Clients.Others.SendAsync("ReceiveClickPress", key);
        }
    }
}
