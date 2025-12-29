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
using Volo.Abp.MultiTenancy;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Syper.ProgramRepository;

namespace Syper.Programs;

[Authorize(SyperPermissions.Programs.Default)]
public class ProgramAppService : ApplicationService, IProgramAppService
{
    private readonly IProgramRepository _repository;
    private readonly ICurrentTenant _currentTenant;

    public ProgramAppService(IProgramRepository repository, ICurrentTenant currentTenant)
    {
        _repository = repository;
        _currentTenant = currentTenant;
    }

    public async Task<ProgramDto> GetAsync(Guid id)
    {
        var Program = await _repository.GetWithDetailsAsync(id);
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

    private async Task CreateUpdateProgram(CreateUpdateProgramDto input)
    {
        var dbContext = await _repository.GetDbContextAsync();

        var paramTenantId = new Npgsql.NpgsqlParameter("tenant_id", _currentTenant.Id ?? Guid.Empty)
        {
            NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
        };

        var paramProgramJson = new Npgsql.NpgsqlParameter("p_program_json", JsonSerializer.Serialize(input))
        {
            NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb
        };

        await dbContext.Database.ExecuteSqlRawAsync(
            "CALL createupdate_program(@tenant_id, @p_program_json)",
            paramTenantId,
            paramProgramJson
        );
    }

    [Authorize(SyperPermissions.Programs.Create)]
    public async Task<ProgramDto> CreateAsync(CreateUpdateProgramDto input)
    {
        await CreateUpdateProgram(input);
        return null;
    }

    [Authorize(SyperPermissions.Programs.Edit)]
    public async Task<ProgramDto> UpdateAsync(Guid id, CreateUpdateProgramDto input)
    {
        await CreateUpdateProgram(input);
        return null;
    }

    [Authorize(SyperPermissions.Programs.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
