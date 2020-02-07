using System;
using System.Threading.Tasks;
using EasyBus.Abstractions;

namespace EasyBus.Tests.Queries
{
    public class GuidQuery : IQuery<Guid>
    {
        
    }

    public class GuidQueryHandler : IQueryHandler<GuidQuery, Guid>, ITransientService
    {
        public async Task<Guid> QueryAsync(GuidQuery query)
        {
            return Guid.NewGuid();
        }
    }
}