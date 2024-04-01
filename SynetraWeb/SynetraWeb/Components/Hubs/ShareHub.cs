using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using SynetraUtils.Models.DataManagement;
using SynetraUtils.Models.MessageManagement;
using SynetraWeb.Client.Models;

namespace SynetraWeb.Components.Hubs
{
    public class ShareHub : Hub
    {
        public Task SendImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }
        public async Task SendMessage(string title, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", title, message);
        }
        public Task SendNetworkInfo(NetworkInfo networkInfo)
        {
           return Clients.All.SendAsync("ReceiveNetworkInfo", networkInfo);
        }
        public async Task SendMouseMovement(double x, double y ,  double height , double width)
        {
            await Clients.Others.SendAsync("ReceiveMouseMovement", x, y , height , width);
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
