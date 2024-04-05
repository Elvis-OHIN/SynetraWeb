using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using SynetraUtils.Models.DataManagement;
using SynetraUtils.Models.MessageManagement;
using SynetraWeb.Client.Models;
using SynetraWeb.Client.Services;
using SynetraWeb.Components.SignalR;

namespace SynetraWeb.Components.Hubs
{
    public class ShareHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = 
            new ConnectionMapping<string>();
        /*public override async Task<Task> OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            
            _connections.Add(name, Context.ConnectionId);
            var param1 = Context.GetHttpContext().Request.Query["param1"];
            string key = "clé de cryptage partagée";
            
            /*ComputerService computers = new ComputerService();
            var computer =  computers.GetByIdAsync(1);
        
            string decryptedId = Auth.Decrypt(param1, key);
            if (decryptedId == "")
            {
                 await SendDataOfPc(Context.ConnectionId);
            }
            
            // Utilisez decryptedId pour l'authentification ou toute autre logique
            return base.OnConnectedAsync();
        }*/
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public Task SendImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }
        public async Task Send(string userId, double x, double y ,  double height , double width)
        {
            await Clients.User(userId).SendAsync("ReceiveMouseMovement", x, y , height , width);
        }
        public async Task SendDataOfPc(string userId)
        {
            await Clients.User(userId).SendAsync("ReceiveDataOfPc");
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
