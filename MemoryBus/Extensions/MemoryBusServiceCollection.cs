using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MemoryBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;

namespace MemoryBus.Extensions
{
    public static class MemoryBusServiceCollection
    {
        public static void AddMemoryBus(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies.Append(typeof(MemoryBusServiceCollection).Assembly);
            
            
            services.RegisterServiceMarkers(assemblies);
        }

        private static void RegisterServiceMarkers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.Scan(source =>
            {
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<ISingleService>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
                
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<IScopedService>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
                
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<ITransientService>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
            
            services.RemoveAll(typeof(ISingleService));
            services.RemoveAll(typeof(IScopedService));
            services.RemoveAll(typeof(ITransientService));
        }
    }
}