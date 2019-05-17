using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Work.Entities.user;

namespace Work.Hubs
{
    using Infra.Managers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class DepthWorldHub : Hub
    {
        private static readonly ConnectionMapping<string> Connections = new ConnectionMapping<string>();

        private static UserContext UserContext { get; set; } = new UserContext();

        public DepthWorldHub()
        {
            
        }

        public async Task Send(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Connections.Remove(UserContext.UserName, Context.ConnectionId);
            Console.WriteLine(UserContext.UserName + " Disconnected from the server...");
            return base.OnDisconnectedAsync(exception);
        }

        public void HandMakeConnection(string username)
        {
            UserContext.UserName = username;

            Connections.Add(UserContext.UserName, Context.ConnectionId);

            Console.WriteLine(UserContext.UserName + " : " + Context.ConnectionId);
        }

        public async Task CreateGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(UserContext.ConnectionId, groupName);
        }

        
        //Todo: Dev some method when must privatly send a message for some user
        public async Task SendPrivateMessageToGroup(string groupName, string username, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage",
                Clients.User(username).SendAsync("Send", UserContext.UserName, message));
        }
        
        public string GetConnectionIdOfUser(string userName)
        {
//            var result = UserContext.UserName
            return String.Empty;
        }
        
    }
}
