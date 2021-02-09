using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RXJS_Dotnetcore_Web.Handlers;
using RXJS_Dotnetcore_Web.Models;

namespace RXJS_Dotnetcore_Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatEventHandler _chateventHandler;

        public ChatHub(IChatEventHandler chateventHandler)
        {
            _chateventHandler = chateventHandler;
        }

        public async Task SendMessage(string sender, string message)
        {
            await Clients.All.SendAsync("chat", sender, message);

            _chateventHandler.Publish(new ChatMessageReceivedEvent
            {
                Message = message
            });
        }


    }
}
