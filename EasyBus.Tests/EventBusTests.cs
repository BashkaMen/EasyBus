using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using EasyBus.Extensions;
using EasyBus.Tests.Events;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EasyBus.Tests
{
    public class EventBusTests
    {
        private IEventBus _bus;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddEasyBus(new []{Assembly.GetExecutingAssembly()});

            var provider = services.BuildServiceProvider();
            _bus = provider.GetRequiredService<IEventBus>();
        }

        [Test]
        public async Task Simple_Usage_1()
        {
            var count = await _bus.PublishAsync(new EmptyEvent());

            Assert.AreEqual(1, count);
        }
        
        [Test]
        public async Task Simple_Usage_2()
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