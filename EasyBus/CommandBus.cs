using System.Threading.Tasks;
using EasyBus.Abstractions;

namespace EasyBus
{
    public class CommandBus : ICommandBus, ITransientService
    {
        private readonly IResolver _resolver;

        public CommandBus(IResolver resolver)
        {
            _resolver = resolver;
        }

        public async Task SendAsync<T>(T message) where T : ICommand
        {
            var handler = _resolver.Resolve<ICommandHandler<T>>();

            await handler.HandleAsync(message);
        }
    }
}