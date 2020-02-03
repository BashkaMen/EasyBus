using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MemoryBus
{
    public class EventBus : IEventBus
    {
        private readonly IServiceProvider _provider;

        public EventBus(IServiceProvider provider)
        {
            _provider = provider;
        }
        
        public async Task<int> PublishAsync<T>(T @event) where T : class
        {
            var handlers = _provider.GetServices<IEventHandler<T>>().ToList();
            
            var tasks = handlers.Select(s => s.HandleAsync(@event));
            await Task.WhenAll(tasks);

            return handlers.Count;
        }
    }

    public class ManualEventBus : IManualEventBus
    {
        private ConcurrentDictionary<Type, List<Func<dynamic, Task>>> _source;

        public ManualEventBus()
        {
            _source = new ConcurrentDictionary<Type, List<Func<dynamic, Task>>>();
        }
        
        public async Task<int> PublishAsync<T>(T @event) where T : class
        {
            if (!_source.TryGetValue(typeof(T), out var handlers)) return 0;
            
            var tasks = handlers.Select(s => s.Invoke(@event));
            await Task.WhenAll(tasks);

            return handlers.Count;
        }

        public void Subscribe<T>(Func<T, Task> handler)
        {
            Task wrapper(dynamic @event)
            {
                return handler(@event);
            }
            
            _source.AddOrUpdate(typeof(T), new List<Func<dynamic, Task>> {wrapper}, (type, items) =>
            {
                items.Add(wrapper);
                return items;
            });
        }
    }
}