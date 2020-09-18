using Microsoft.AspNetCore.SignalR;
using SignalIR.Common;
using System;
using System.Threading.Tasks;

namespace SignalIR.Server.Hubs
{
    public class ClockHub : Hub<IClock>
    {
        public async Task SendTimeToClients(DateTime dateTime)
        {
            await Clients.All.ShowTime(dateTime);
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.ReceiveMessage(user, message);
        }

        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }

        public Task SendMessageToGroup(string message)
        {
            return Clients.Group("SignalR Users").ReceiveMessage(message);
        }
    }
}