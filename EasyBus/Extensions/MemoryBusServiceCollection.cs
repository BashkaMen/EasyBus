using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;

namespace EasyBus.Extensions
{
    public static class EasyBusServiceCollection
    {
        public static void AddEasyBus(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies.Append(typeof(EasyBusServiceCollection).Assembly);
            
            
            services.RegisterServiceMarkers(assemblies);
        }

        private static void RegisterServiceMarkers(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.Scan(source =>
            {
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<ISingletonService>())
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
                
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<IScopedService>())
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
                
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<ITransientService>())
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
            
            services.RemoveAll(typeof(ISingletonService));
            services.RemoveAll(typeof(IScopedService));
            services.RemoveAll(typeof(ITransientService));
        }
    }
}