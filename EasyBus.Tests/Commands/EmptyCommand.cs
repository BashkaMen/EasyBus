using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using NUnit.Framework;

namespace EasyBus.Tests.Commands
{
    public class EmptyCommand
    {
        
    }

    public class EmptyCommandHandler : ICommandHandler<EmptyCommand>, ITransientService
    {
        public async Task HandleAsync(EmptyCommand command)
        {
            TestContext.WriteLine($"{command.GetType().Name} handled");
        }
    }
}