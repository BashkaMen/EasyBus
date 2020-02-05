using System;
using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface IEventBus
    {
        Task<int> PublishAsync<T>(T @event) where T : IEvent;
        IDisposable Subscribe<T>(Func<T, Task> handler) where T : IEvent;
    }
}