using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T message) where T : class;
    }
}