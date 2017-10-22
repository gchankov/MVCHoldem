namespace MVCHoldem.Web.Hubs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR;
    using System.Linq;

    public class Chat : Hub
    {
        private static List<string> chatUsersData = new List<string>();
        private static List<string> chatMessages = new List<string>();

        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", Context.User.Identity.Name, message);
            chatMessages.Add(msg);
            Clients.All.receiveMessage(msg);
        }

        public override Task OnConnected()
        {
            chatUsersData.Add(Context.User.Identity.Name);
            Clients.All.updateUsers(chatUsersData.Distinct().ToArray());
            Clients.Caller.populateMessages(chatMessages.ToArray());
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            chatUsersData.Remove(Context.User.Identity.Name);
            Clients.All.updateUsers(chatUsersData.Distinct().ToArray());
            return base.OnDisconnected(stopCalled);
        }
    }
}