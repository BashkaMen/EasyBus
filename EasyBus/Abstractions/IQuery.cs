using System.Threading.Tasks;

namespace EasyBus.Abstractions
{
    public interface IQuery<in TResult>
    {
        
    }

    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> QueryAsync(TQuery query);
    }

    public interface IQueryBus
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}