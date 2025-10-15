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
using Syper.Programs;

namespace Syper.Programs;

[Authorize(SyperPermissions.Programs.Default)]
public class ProgramAppService : ApplicationService, IProgramAppService
{
    private readonly IRepository<Program, Guid> _repository;

    public ProgramAppService(IRepository<Program, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<ProgramDto> GetAsync(Guid id)
    {
        var Program = await _repository.GetAsync(id);
        return ObjectMapper.Map<Program, ProgramDto>(Program);
    }

    public async Task<PagedResultDto<ProgramDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "LastModificationTime" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Programs = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ProgramDto>(
            totalCount,
            ObjectMapper.Map<List<Program>, List<ProgramDto>>(Programs)
        );
    }

    [Authorize(SyperPermissions.Programs.Create)]
    public async Task<ProgramDto> CreateAsync(CreateUpdateProgramDto input)
    {
        var Program = ObjectMapper.Map<CreateUpdateProgramDto, Program>(input);
        await _repository.InsertAsync(Program);
        return ObjectMapper.Map<Program, ProgramDto>(Program);
    }

    [Authorize(SyperPermissions.Programs.Edit)]
    public async Task<ProgramDto> UpdateAsync(Guid id, CreateUpdateProgramDto input)
    {
        var Program = await _repository.GetAsync(id);
        ObjectMapper.Map(input, Program);
        await _repository.UpdateAsync(Program);
        return ObjectMapper.Map<Program, ProgramDto>(Program);
    }

    [Authorize(SyperPermissions.Programs.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
