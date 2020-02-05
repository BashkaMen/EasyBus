using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface ICommandHandler<in T> where T: ICommand
    {
        Task HandleAsync(T command);
    }
}