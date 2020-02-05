using System;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyBus
{
    public class CommandBus : ICommandBus, ITransientService
    {
        private readonly IServiceProvider _provider;

        public CommandBus(IServiceScopeFactory scopeFactory)
        {
            _provider = scopeFactory.CreateScope().ServiceProvider;
        }
        
        public async Task SendAsync<T>(T message) where T : ICommand
        {
            var handler = _provider.GetRequiredService<ICommandHandler<T>>();

            await handler.HandleAsync(message);
        }
    }
}