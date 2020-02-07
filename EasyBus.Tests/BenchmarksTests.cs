using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using EasyBus.Extensions;
using EasyBus.Tests.Commands;
using EasyBus.Tests.Events;
using EasyBus.Tests.Queries;
using Microsoft.Extensions.DependencyInjection;
using NBomber.Contracts;
using NBomber.CSharp;
using NUnit.Framework;

namespace EasyBus.Tests
{
    public class BenchmarksTests
    {
        private IQueryBus _queryBus;
        private ICommandBus _commandBus;
        private IEventBus _eventBus;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddEasyBus(Assembly.GetExecutingAssembly());

            var provider = services.BuildServiceProvider();
            _queryBus = provider.GetRequiredService<IQueryBus>();
            _commandBus = provider.GetRequiredService<ICommandBus>();
            _eventBus = provider.GetRequiredService<IEventBus>();
        }
        
        private Scenario CreateScenario(string name, IStep[] steps)
        {
            var scenario = ScenarioBuilder.CreateScenario(name, steps)
                .WithConcurrentCopies(1)
                .WithWarmUpDuration(TimeSpan.FromSeconds(1))
                .WithDuration(TimeSpan.FromSeconds(10));

            return scenario;
        }
    

        [Test]
        public void Query()
        {
            var query = new GuidQuery();
            var step = Step.Create("QueryAsync", async context =>
            {
                await _queryBus.QueryAsync(query);
                return Response.Ok();
            });

            var scenario = CreateScenario("Query performance", new[] {step});
            
            NBomberRunner.RegisterScenarios(scenario).RunTest();
        }
        
        [Test]
        public void Command()
        {
            var command = new EmptyCommand();
            var step = Step.Create("CommandAsync", async context =>
            {
                await _commandBus.SendAsync(command);
                return Response.Ok();
            });

            var scenario = CreateScenario("Command performance", new[] {step});
            NBomberRunner.RegisterScenarios(scenario).RunTest();
        }
        
        [Test]
        public void Event()
        {
            var @event = new EmptyEvent();
            var step = Step.Create("EventAsync", async context =>
            {
                await _eventBus.PublishAsync(@event);
                return Response.Ok();
            });

            var scenario = CreateScenario("Event performance", new[] {step});
            
            NBomberRunner.RegisterScenarios(scenario).RunTest();
        }
    }
}