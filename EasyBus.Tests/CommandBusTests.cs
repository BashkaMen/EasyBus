using System.Reflection;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using EasyBus.Extensions;
using EasyBus.Tests.Commands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EasyBus.Tests
{
    public class CommandBusTests
    {
        private ICommandBus _bus;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            
            services.AddEasyBus(new []{Assembly.GetExecutingAssembly()});

            var provider = services.BuildServiceProvider();

            _bus = provider.GetRequiredService<ICommandBus>();
        }

        [Test]
        public async Task Simple_Usage()
        {
            await _bus.SendAsync(new EmptyCommand());
        }
    }
}