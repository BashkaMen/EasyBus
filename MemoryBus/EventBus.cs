using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MemoryBus
{
    public delegate Task EventHandlerFunc(dynamic @event); 
    public class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<EventSubscriber, EventHandlerFunc> _source;
        private readonly IServiceProvider _provider;

        public EventBus(IServiceProvider provider)
        {
            _provider = provider;
            _source = new ConcurrentDictionary<EventSubscriber, EventHandlerFunc>();
        }
        
        public async Task<int> PublishAsync<T>(T @event) where T : class
        {
            var handlers = _provider.GetServices<IEventHandler<T>>();
            var subs = _source.Where(s => s.Key.EventType == TypeOf<T>.Raw);
            
            var tasks = handlers.Select(s => s.HandleAsync(@event)).ToList();
            tasks.AddRange(subs.Select(s=> s.Value.Invoke(@event)));

            await Task.WhenAll(tasks);
            return tasks.Count;
        }
        
        public IDisposable Subscribe<T>(Func<T, Task> handler)
        {
            Task Wrapper(dynamic @event) => handler((T)@event);
            var sub = new EventSubscriber(TypeOf<T>.Raw,(self)=> _source.TryRemove(self, out _));
            
            if (!_source.TryAdd(sub, Wrapper)) 
                throw new InvalidOperationException("Create subscribe failed");

            return sub;
        }
    }
}