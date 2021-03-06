﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using EasyBus.Abstractions;

namespace EasyBus
{
    internal delegate Task EventHandlerFunc(IEvent @event);

    public class EventBus : IEventBus, ISingletonService
    {
        private readonly IResolver _resolver;
        private readonly ConcurrentDictionary<EventSubscriber, EventHandlerFunc> _source;

        public EventBus(IResolver resolver)
        {
            _resolver = resolver;
            _source = new ConcurrentDictionary<EventSubscriber, EventHandlerFunc>();
        }

        public async Task<int> PublishAsync<T>(T @event) where T : IEvent
        {
            var handlers = _resolver.ResolveMany<IEventHandler<T>>();
            var subs = _source.Where(s => s.Key.EventType == TypeOf<T>.Raw);

            var tasks = handlers.Select(s => s.HandleAsync(@event)).ToList();
            tasks.AddRange(subs.Select(s => s.Value.Invoke(@event)));

            await Task.WhenAll(tasks);
            return tasks.Count;
        }

        public IDisposable Subscribe<T>(Func<T, Task> handler) where T : IEvent
        {
            Task Wrapper(IEvent @event)
            {
                return handler((T) @event);
            }

            var sub = new EventSubscriber(TypeOf<T>.Raw, self => _source.TryRemove(self, out _));

            if (!_source.TryAdd(sub, Wrapper))
                throw new InvalidOperationException("Create subscribe failed");

            return sub;
        }
    }
}