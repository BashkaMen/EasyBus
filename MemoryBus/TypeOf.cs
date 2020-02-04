using System;

namespace MemoryBus
{
    internal static class TypeOf<T>
    {
        public static Type Raw { get; } = typeof(T); 
    }
}