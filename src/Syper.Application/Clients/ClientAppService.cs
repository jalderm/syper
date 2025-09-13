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
using Syper.Coaching;

namespace Syper.Clients;

[Authorize(SyperPermissions.Clients.Default)]
public class ClientAppService : ApplicationService, IClientAppService
{
    private readonly IRepository<Client, Guid> _repository;

    public ClientAppService(IRepository<Client, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<ClientDto> GetAsync(Guid id)
    {
        var Client = await _repository.GetAsync(id);
        return ObjectMapper.Map<Client, ClientDto>(Client);
    }

    public async Task<PagedResultDto<ClientDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "FirstName" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Clients = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ClientDto>(
            totalCount,
            ObjectMapper.Map<List<Client>, List<ClientDto>>(Clients)
        );
    }

    [Authorize(SyperPermissions.Clients.Create)]
    public async Task<ClientDto> CreateAsync(CreateUpdateClientDto input)
    {
        var Client = ObjectMapper.Map<CreateUpdateClientDto, Client>(input);
        await _repository.InsertAsync(Client);
        return ObjectMapper.Map<Client, ClientDto>(Client);
    }

    [Authorize(SyperPermissions.Clients.Edit)]
    public async Task<ClientDto> UpdateAsync(Guid id, CreateUpdateClientDto input)
    {
        var Client = await _repository.GetAsync(id);
        ObjectMapper.Map(input, Client);
        await _repository.UpdateAsync(Client);
        return ObjectMapper.Map<Client, ClientDto>(Client);
    }

    [Authorize(SyperPermissions.Clients.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
