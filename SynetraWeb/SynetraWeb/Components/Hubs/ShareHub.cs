using Microsoft.AspNetCore.SignalR;
using SynetraWeb.Client.Models;

namespace SynetraWeb.Components.Hubs
{
    public class ShareHub : Hub
    {
        public Task ImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }
    }
}
