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
        public static void AddEasyBus(this IServiceCollection services, params Assembly[] assemblies)
        {
            assemblies = assemblies.Append(typeof(EasyBusServiceCollection).Assembly).ToArray();
            
            services.RegisterServiceMarkers(assemblies);
        }

        private static void RegisterServiceMarkers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(source =>
            {
                source.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo<ISingletonService>())
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
            
            services.RemoveAll(typeof(ISingletonService));
            services.RemoveAll(typeof(IScopedService));
            services.RemoveAll(typeof(ITransientService));
        }
    }
}