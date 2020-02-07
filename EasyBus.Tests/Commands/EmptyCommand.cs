using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EasyBus.Abstractions;
using NUnit.Framework;

namespace EasyBus.Tests.Commands
{
    public class EmptyCommand : ICommand
    {
        
    }

    public class EmptyCommandHandler : ICommandHandler<EmptyCommand>, ITransientService
    {
        public Task HandleAsync(EmptyCommand command)
        {
            return Task.CompletedTask;
        }
    }
}