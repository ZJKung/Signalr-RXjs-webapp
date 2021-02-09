using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using RXJS_Dotnetcore_Web.Models;
using System.Linq;
using System.Reactive.Linq;

namespace RXJS_Dotnetcore_Web.Handlers
{
    public class ChatEventHandler : IChatEventHandler, IDisposable
    {
        private readonly Subject<ChatMessageReceivedEvent> _subject;
        private readonly Dictionary<string, IDisposable> _subscribers;

        public ChatEventHandler()
        {
            _subject = new Subject<ChatMessageReceivedEvent>();
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Dispose()
        {
            if (_subject != null)
            {
                _subject.Dispose();
            }
            foreach (var subscriber in _subscribers)
            {
                subscriber.Value.Dispose();
            }
        }

        public void Publish(ChatMessageReceivedEvent receiveEvent)
        {
            _subject.OnNext(receiveEvent);
        }

        public void Subscribe(string subscriberName, Action<ChatMessageReceivedEvent> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.TryAdd(subscriberName, _subject.Subscribe(action));
            }
        }

        public void Subscribe(string subscriberName, Func<ChatMessageReceivedEvent, bool> predicate, Action<ChatMessageReceivedEvent> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Where(predicate).Subscribe(action));
            }

        }
    }
}
