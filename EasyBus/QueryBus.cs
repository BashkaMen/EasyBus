using System.Threading.Tasks;
using EasyBus.Abstractions;

namespace EasyBus
{
    public class QueryBus : IQueryBus, ITransientService
    {
        private readonly IResolver _resolver;

        public QueryBus(IResolver resolver)
        {
            _resolver = resolver;
        }
        
        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var resultType = TypeOf<TResult>.Raw;
            var queryType = query.GetType();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);
            
            dynamic handler = _resolver.Resolve(handlerType);

            var result = (TResult)await handler.QueryAsync((dynamic)query);

            return result;
        }
    }
}