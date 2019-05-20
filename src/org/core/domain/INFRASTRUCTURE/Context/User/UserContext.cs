using System;
using System.Collections.Generic;
using System.Text;

namespace INFRASTRUCTURE.Context.User
{
    public class UserContext
    {
        public string UserName { get; private set; }

        public string ConnectingId { get; private set; }

        public void AddUser(string username, string connectingId)
        {
            this.UserName = username;
            this.ConnectingId = connectingId;
        }
    }
}
