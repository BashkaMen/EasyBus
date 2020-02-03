using System.Threading.Tasks;

namespace MemoryBus.Abstractions
{
    public interface IEventHandler<in T> where T : class
    {
        Task HandleAsync(T @event);
    }
}