using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Org.BouncyCastle.Bcpg.Sig;
using SynetraUtils.Auth;
using SynetraUtils.Models.DataManagement;
using SynetraUtils.Models.MessageManagement;
using SynetraWeb.Client.Models;
using SynetraWeb.Client.Services;
using SynetraWeb.Components.Pages;
using SynetraWeb.Components.SignalR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SynetraWeb.Components.Hubs
{
    public class ShareHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = 
            new ConnectionMapping<string>();
        public override async Task<Task> OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            
        
            var param1 = Context.GetHttpContext().Request.Query["footPrint"];
            var param2 = Context.GetHttpContext().Request.Query["key"];
            var param3 = Context.GetHttpContext().Request.Query["iv"];
            
            // Decrypt the bytes to a string.
            //string decrypted = HardwareIdentifier.DecryptStringFromBytes_Aes(Convert.FromBase64String(param1), Convert.FromBase64String(param2), Convert.FromBase64String(param3) );

            ComputerService computers = new ComputerService();

            if (param1 != "")
            {
                var c = await computers.GetByFootPrintAsync(param1);
                if (c != null)
                {
                    SynetraUtils.Models.DataManagement.Connection conn = new SynetraUtils.Models.DataManagement.Connection();
                    conn.Connected = true;
                    conn.ConnectionID = Context.ConnectionId;
                    conn.UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"];
                    await computers.CreateConnexionAsync(c.Id, conn);
                }   
            }

            return base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendInfoOfPc(Computer computer)
        {
            ComputerService computers = new ComputerService();
            if (computer != null) {
                var co = await computers.GetByFootPrintAsync(computer.FootPrint);
                if (co == null)
                {
                    var c = await computers.CreateAsync(computer);
                    SynetraUtils.Models.DataManagement.Connection conn = new SynetraUtils.Models.DataManagement.Connection();
                    conn.Connected = true;
                    conn.ConnectionID = Context.ConnectionId;
                    conn.UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"];
                    await computers.CreateConnexionAsync(c.Id, conn);
                }
            }
            //refresh message
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
            await Clients.User(userId).SendAsync("ReceiveDataOfPc" , "start");
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
