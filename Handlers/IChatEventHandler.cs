using System;
using RXJS_Dotnetcore_Web.Models;

namespace RXJS_Dotnetcore_Web.Handlers
{
    public interface IChatEventHandler
    {
        void Publish(ChatMessageReceivedEvent receiveEvent);

        void Subscribe(string subscriberName, Action<ChatMessageReceivedEvent> action);

        void Subscribe(string subscriberName, Func<ChatMessageReceivedEvent, bool> predicate, Action<ChatMessageReceivedEvent> action);

    }
}
