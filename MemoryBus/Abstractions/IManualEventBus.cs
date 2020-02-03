using System;
using System.Threading.Tasks;

namespace MemoryBus.Abstractions
{
    public interface IManualEventBus : IEventBus
    {
        void Subscribe<T>(Func<T, Task> handler);
    }
}