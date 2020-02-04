using System;
using System.Threading.Tasks;
using MemoryBus.Abstractions;

namespace MemoryBus.Tests.Events
{
    class EmptyEvent
    {
    }

    class EmptyEventHandler : IEventHandler<EmptyEvent>, ITransientService
    {
        public async Task HandleAsync(EmptyEvent @event)
        {
            Console.WriteLine($"{@event.GetType().FullName} handled");
        }
    }
}