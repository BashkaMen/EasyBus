using System.Threading.Tasks;

namespace MemoryBus.Abstractions
{
    public interface IMessageBus
    {
        Task SendAsync<T>(T message) where T : class;
    }
}