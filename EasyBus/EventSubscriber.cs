using System;
using System.Threading.Tasks;

namespace EasyBus
{
    internal class EventSubscriber : IDisposable
    {
        private readonly Action<EventSubscriber> _onDispose;

        public Type EventType { get; }

        public EventSubscriber(Type eventType, Action<EventSubscriber> onDispose)
        {
            EventType = eventType;
            _onDispose = onDispose;
        }
        
        public void Dispose()
        {
            _onDispose?.Invoke(this);
        }
    }
}