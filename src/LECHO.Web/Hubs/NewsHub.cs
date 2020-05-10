using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LECHO.Web.Hubs
{
    [HubName("news")]
    public class NewsHub : Hub
    {
        public async Task PostNews(string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
