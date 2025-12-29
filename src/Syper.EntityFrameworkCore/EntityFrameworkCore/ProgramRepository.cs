
using Syper.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syper.Programs;

namespace Syper.ProgramRepository
{
    public interface IProgramRepository : IRepository<Program, Guid>
    {
        Task<Program?> GetWithDetailsAsync(Guid id);
    }

    public class ProgramRepository : EfCoreRepository<SyperDbContext, Program, Guid>, IProgramRepository
    {
        public ProgramRepository(IDbContextProvider<SyperDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Program> GetWithDetailsAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();

            return await dbContext.Programs
                .Include(x => x.ScheduleDays)
                    .ThenInclude(s => s.Activities)
                        .ThenInclude(we => we.Workout)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}