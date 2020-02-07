using System;
using System.Collections.Generic;

namespace EasyBus.Abstractions
{
    public interface IResolver
    {
        object Resolve(Type type);
        T Resolve<T>();
        IEnumerable<T> ResolveMany<T>();
    }
}