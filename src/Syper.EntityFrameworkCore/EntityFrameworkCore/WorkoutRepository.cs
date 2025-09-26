
using Syper.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syper.Workouts;

namespace Syper.WorkoutRepository
{
    public interface IWorkoutRepository : IRepository<Workout, Guid>
    {
        Task<Workout?> GetWithSectionsAndExercisesAsync(Guid id);
    }

    public class WorkoutRepository : EfCoreRepository<SyperDbContext, Workout, Guid>, IWorkoutRepository
    {
        public WorkoutRepository(IDbContextProvider<SyperDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Workout> GetWithSectionsAndExercisesAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();

            return await dbContext.Workouts
                .Include(x => x.WorkoutSections)
                    .ThenInclude(s => s.WorkoutExercises)
                        .ThenInclude(we => we.Sets)
                .Include(x => x.WorkoutSections)
                    .ThenInclude(s => s.WorkoutExercises)
                        .ThenInclude(we => we.Exercise)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}