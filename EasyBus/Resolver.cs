using System;
using System.Collections.Generic;
using EasyBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyBus
{
    public class Resolver : IResolver, ITransientService
    {
        private readonly IServiceProvider _provider;    
        
        public Resolver(IServiceScopeFactory scopeFactory)
        {
            _provider = scopeFactory.CreateScope().ServiceProvider;
        }

        public object Resolve(Type type)
        {
            return _provider.GetRequiredService(type);
        }

        public T Resolve<T>()
        {
            return _provider.GetRequiredService<T>();
        }

        public IEnumerable<T> ResolveMany<T>()
        {
            return _provider.GetServices<T>();
        }
    }
}