using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Syper.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace Syper.Workouts;

[Authorize(SyperPermissions.Workouts.Default)]
public class WorkoutAppService : ApplicationService, IWorkoutAppService
{
    private readonly IRepository<Workout, Guid> _repository;

    public WorkoutAppService(IRepository<Workout, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<WorkoutDto> GetAsync(Guid id)
    {
        var Workout = await _repository.GetAsync(id);
        return ObjectMapper.Map<Workout, WorkoutDto>(Workout);
    }

    public async Task<PagedResultDto<WorkoutDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Workouts = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<WorkoutDto>(
            totalCount,
            ObjectMapper.Map<List<Workout>, List<WorkoutDto>>(Workouts)
        );
    }

    [Authorize(SyperPermissions.Workouts.Create)]
    public async Task<WorkoutDto> CreateAsync(CreateUpdateWorkoutDto input)
    {
        var Workout = ObjectMapper.Map<CreateUpdateWorkoutDto, Workout>(input);
        await _repository.InsertAsync(Workout);
        return ObjectMapper.Map<Workout, WorkoutDto>(Workout);
    }

    [Authorize(SyperPermissions.Workouts.Edit)]
    public async Task<WorkoutDto> UpdateAsync(Guid id, CreateUpdateWorkoutDto input)
    {
        var Workout = await _repository.GetAsync(id);
        ObjectMapper.Map(input, Workout);
        await _repository.UpdateAsync(Workout);
        return ObjectMapper.Map<Workout, WorkoutDto>(Workout);
    }

    [Authorize(SyperPermissions.Workouts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
