using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    public class MessagesHub : Hub
    {
        public async Task SendMessage(string name, string message)
        {
            await Clients.All.SendAsync("MessageReceived", name, message);
        }
    }
}
