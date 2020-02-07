using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using EasyBus.Extensions;
using EasyBus.Tests.Queries;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EasyBus.Tests
{
    public class QueryBusTests
    {
        private IQueryBus _bus;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddEasyBus(Assembly.GetExecutingAssembly());

            var provider = services.BuildServiceProvider();
            _bus = provider.GetRequiredService<IQueryBus>();
        }

        [Test]
        public async Task Simple_Usage()
        {
            var result = await _bus.QueryAsync(new GuidQuery());

            Assert.AreNotEqual(Guid.Empty, result);
        }
    }
}