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

namespace Syper.Exercises;

[Authorize(SyperPermissions.Exercises.Default)]
public class ExerciseAppService : ApplicationService, IExerciseAppService
{
    private readonly IRepository<Exercise, Guid> _repository;

    public ExerciseAppService(IRepository<Exercise, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<ExerciseDto> GetAsync(Guid id)
    {
        var Exercise = await _repository.GetAsync(id);
        return ObjectMapper.Map<Exercise, ExerciseDto>(Exercise);
    }

    public async Task<PagedResultDto<ExerciseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Title" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Exercise = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ExerciseDto>(
            totalCount,
            ObjectMapper.Map<List<Exercise>, List<ExerciseDto>>(Exercise)
        );
    }

    [Authorize(SyperPermissions.Exercises.Create)]
    public async Task<ExerciseDto> CreateAsync(CreateUpdateExerciseDto input)
    {
        var Exercise = ObjectMapper.Map<CreateUpdateExerciseDto, Exercise>(input);
        await _repository.InsertAsync(Exercise);
        return ObjectMapper.Map<Exercise, ExerciseDto>(Exercise);
    }

    [Authorize(SyperPermissions.Exercises.Edit)]
    public async Task<ExerciseDto> UpdateAsync(Guid id, CreateUpdateExerciseDto input)
    {
        var Exercise = await _repository.GetAsync(id);
        ObjectMapper.Map(input, Exercise);
        await _repository.UpdateAsync(Exercise);
        return ObjectMapper.Map<Exercise, ExerciseDto>(Exercise);
    }

    [Authorize(SyperPermissions.Exercises.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
