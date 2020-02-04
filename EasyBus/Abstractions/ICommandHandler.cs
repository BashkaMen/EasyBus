using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface ICommandHandler<T> where T: class
    {
        Task HandleAsync(T command);
    }
}