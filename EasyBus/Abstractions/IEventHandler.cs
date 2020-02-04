using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface IEventHandler<in T> where T : class
    {
        Task HandleAsync(T @event);
    }
}