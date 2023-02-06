using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message, string to)
        {
            try
            {
                if (Context.UserIdentifier is string userName)
                {
                    await Clients.Users(to, userName).SendAsync("Receive", message, userName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
      
        }

        public override async Task OnConnectedAsync()
        {
            Console.Write("connected");
            await Clients.All.SendAsync("Notify", $"Приветствуем {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
    }

}

