using System.Threading.Tasks;

namespace MemoryBus.Abstractions
{
    public interface IEventBus
    {
        Task<int> PublishAsync<T>(T @event) where T : class;
    }
}