using System;
using System.Reflection;
using System.Threading.Tasks;
using MemoryBus.Abstractions;
using MemoryBus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace MemoryBus.Tests
{
    public class EventBusTests
    {
        private IEventBus _bus;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddMemoryBus(new[] {Assembly.GetExecutingAssembly()});

            var provider = services.BuildServiceProvider();
            _bus = provider.GetRequiredService<IEventBus>();
        }

        [Test]
        public async Task SimpleUsage()
        {
            var count = await _bus.PublishAsync(new EmptyEvent());

            Assert.AreEqual(1, count);
        }
    }

    class EmptyEvent
    {
    }

    class EmptyEventHandler : IEventHandler<EmptyEvent>
    {
        public async Task HandleAsync(EmptyEvent @event)
        {
            Console.WriteLine($"{@event.GetType().FullName} handled");
        }
    }
}