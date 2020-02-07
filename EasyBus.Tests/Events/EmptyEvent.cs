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
        public Task HandleAsync(EmptyEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}