using INFRASTRUCTURE;
using INFRASTRUCTURE.Context.User;
using INFRASTRUCTURE.Mapping;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DE.WORKFLOWS.Hubs
{
    public class OpenHub : CustomHub
    {
        private static readonly ConnectionMapping<string> connection = new ConnectionMapping<string>();

        private static readonly UserContext userContext = new UserContext();

        public OpenHub()
        {
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            connection.Remove(userContext.UserName, userContext.ConnectingId);
            return base.OnDisconnectedAsync(exception);
        }

        public override Task BackConnectedAsync(string username)
        {
            userContext.AddUser(username, Context.ConnectionId);

            connection.Add(userContext.UserName, userContext.ConnectingId);

            return base.BackConnectedAsync(username);
        }

        public async Task UpdateOrAddGroup(string groupName)
        {
            await Groups.AddToGroupAsync(UserContext.ConnectingId, groupName);
        }

        public async Task SendPrivateMessage(string groupName, string username, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", 
                Clients.User(username).SendAsync("Send", UserContext.UserName, message));
        }
    }
}
