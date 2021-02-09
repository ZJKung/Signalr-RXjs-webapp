using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RXJS_Dotnetcore_Web.Handlers;
using RXJS_Dotnetcore_Web.Models;

namespace RXJS_Dotnetcore_Web.Consumer
{
    public class ChatHistoryConsumer : BackgroundService
    {
        private readonly IChatEventHandler _chateventHandler;

        public ChatHistoryConsumer(IChatEventHandler chateventHandler)
        {
            _chateventHandler = chateventHandler;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _chateventHandler.Subscribe(subscriberName: typeof(ChatHistoryConsumer).Name,
                                    action: async (e) =>
                                    {
                                        if (e is ChatMessageReceivedEvent)
                                        {
                                            await PersistChatMessagesToDBAsync((ChatMessageReceivedEvent)e);
                                        }
                                    });
            return Task.CompletedTask;
        }

        private async Task PersistChatMessagesToDBAsync(ChatMessageReceivedEvent e)
        {
            await System.Console.Out.WriteLineAsync($"Chat message received and persisted: {e.Message}");
        }
    }
}
