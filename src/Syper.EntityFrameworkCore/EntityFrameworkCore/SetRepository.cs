
using Syper.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syper.Sets;

namespace Syper.SetRepository
{
    public interface ISetRepository : IRepository<Set, Guid>
    {

    }

    public class SetRepository : EfCoreRepository<SyperDbContext, Set, Guid>, ISetRepository
    {
        public SetRepository(IDbContextProvider<SyperDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

}