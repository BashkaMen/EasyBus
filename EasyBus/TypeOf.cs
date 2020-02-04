using System;

namespace EasyBus
{
    internal static class TypeOf<T>
    {
        public static Type Raw { get; } = typeof(T); 
    }
}