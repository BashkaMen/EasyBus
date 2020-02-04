using System;
using System.Reflection;
using System.Threading.Tasks;
using MemoryBus.Abstractions;
using MemoryBus.Extensions;
using MemoryBus.Tests.Events;
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
        public async Task Simple_Usage()
        {
            _bus.Subscribe<EmptyEvent>(@event => new EmptyEventHandler().HandleAsync(@event));
            
            var count = await _bus.PublishAsync(new EmptyEvent());

            Assert.AreEqual(2, count);
        }

        [Test]
        public async Task Unsubscribe()
        {
            var count = 0;
            using (_bus.Subscribe<object>(s => Task.CompletedTask))
            {
                count = await _bus.PublishAsync(new object());
                Assert.AreEqual(1, count);
            }
            
            count = await _bus.PublishAsync(new object());
            Assert.AreEqual(0, count);
        }

        [Test]
        public async Task Publish_Without_Handlers()
        {
            _bus.Subscribe<EmptyEvent>(s=> Task.CompletedTask);
            var count = await _bus.PublishAsync(new object());
            
            Assert.AreEqual(0, count);
        }
    }
}