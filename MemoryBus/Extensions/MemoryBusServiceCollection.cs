using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MemoryBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MemoryBus.Extensions
{
    public static class MemoryBusServiceCollection
    {
        public static void AddMemoryBus(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.TryAddSingleton<IEventBus, EventBus>();

            services.Scan(s =>
            {
                s.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
        }
    }
}