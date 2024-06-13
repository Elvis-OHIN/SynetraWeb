using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Org.BouncyCastle.Bcpg.Sig;
using SynetraUtils.Auth;
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
  
        private readonly ComputerService _computerService;

        public ShareHub(ComputerService computerService)
        {
            _computerService = computerService;
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            
        
            var param1 = Context.GetHttpContext().Request.Query["footPrint"];
            var param2 = Context.GetHttpContext().Request.Query["key"];
            var param3 = Context.GetHttpContext().Request.Query["iv"];
            
            if (param1 != "")
            {
                var c = await _computerService.GetByFootPrintAsync(param1);
                
                
                if (c != null)
                {
                    c.Statut = true;
                    Connection conn = new Connection();
                    conn.Connected = true;
                    conn.ConnectionID = Context.ConnectionId;
                    conn.ComputerId = c.Id;
                    conn.UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"];
                    await _computerService.CreateConnexionAsync(c.Id, conn);
                    await _computerService.UpdateAsync(c);
                }
            }

            return base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var computer = await _computerService.GetByConnexionIdAsync(Context.ConnectionId);
            if (computer is not null)
            {
                computer.Statut = false;
                await _computerService.UpdateAsync(computer);
            }
            
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendInfoOfPc(Computer computer)
        {
          
            if (computer != null) {
                var co = await _computerService.GetByFootPrintAsync(computer.FootPrint);
                if (co == null)
                {
                    var c = await _computerService.CreateAsync(computer);
                    Connection conn = new Connection();
                    conn.Connected = true;
                    conn.ConnectionID = Context.ConnectionId;
                    conn.UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"];
                    await _computerService.CreateConnexionAsync(c.Id, conn);
                }
            }
        }
        public Task SendImageMessage(ImageMessage file)
        {
            return Clients.All.SendAsync("ImageMessage", file);
        }
        public async Task Send(string userId, double x, double y ,  double height , double width)
        {
            await Clients.User(userId).SendAsync("ReceiveMouseMovement", x, y , height , width);
        }
        public async Task SendModeVeille(string userId)
        {
            await Clients.Client(userId).SendAsync("ReceiveModeVeille", "Veille");
        }

        public async Task SendModeOff(string userId)
        {
            await Clients.Client(userId).SendAsync("ReceiveModeOff", "Off");
        }

        public async Task SendDataOfPc(string userId)
        {
            await Clients.Client(userId).SendAsync("ReceiveDataOfPc" , "start");
        }
        public async Task SendMessage(string title, string message , string userid)
        {
            await Clients.Client(userid).SendAsync("ReceiveMessage", title, message);
        }
        public Task SendNetworkInfo(NetworkInfo networkInfo)
        {
           return Clients.All.SendAsync("ReceiveNetworkInfo", networkInfo);
        }
        public async Task SendMouseMovement(double x, double y ,  double height , double width , string userid)
        {
            await Clients.Client(userid).SendAsync("ReceiveMouseMovement", x, y , height , width);
        }
        public async Task SendKeyPress(string key, string userid)
        {
            await Clients.Client(userid).SendAsync("ReceiveKeyPress", key);
        }
        public async Task SendClickPress(string key , string userid)
        {
            await Clients.Client(userid).SendAsync("ReceiveClickPress", key);
        }
    }
}
