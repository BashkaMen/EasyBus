using System;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using NUnit.Framework;

namespace EasyBus.Tests.Events
{
    class EmptyEvent : IEvent
    {
    }

    class EmptyEventHandler : IEventHandler<EmptyEvent>, ITransientService
    {
        public async Task HandleAsync(EmptyEvent @event)
        {
            TestContext.WriteLine($"{@event.GetType().FullName} handled");
        }
    }
}