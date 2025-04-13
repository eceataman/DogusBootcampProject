using Microsoft.AspNetCore.SignalR;

namespace DogusBootcampProject.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Tüm bağlı client'lara mesaj gönder
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
