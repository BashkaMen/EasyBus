using System;
using System.Threading.Tasks;
using EasyBus.Abstractions;

namespace EasyBus.Tests.Queries
{
    public class BoolQuery : IQuery<bool>
    {
        
    }

    public class BoolQueryHandler : IQueryHandler<BoolQuery, bool>, ITransientService
    {
        public Task<bool> QueryAsync(BoolQuery query)
        {
            return Task.FromResult(true);
        }
    }
}