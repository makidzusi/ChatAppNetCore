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
                // получение текущего пользователя, который отправил сообщение
                //var userName = Context.UserIdentifier;
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
            await Clients.All.SendAsync("Notify", $"Приветствуем {Context.UserIdentifier}");
            await base.OnConnectedAsync();
        }
    }

}

