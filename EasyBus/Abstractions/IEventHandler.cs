using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}