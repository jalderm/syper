using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Syper.Coaching;
using Syper.ClientStateEnum;

namespace Syper;

public class SyperDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Client, Guid> _clientRepository;

    public SyperDataSeederContributor(IRepository<Client, Guid> clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _clientRepository.GetCountAsync() <= 0)
        {
            await _clientRepository.InsertAsync(
                new Client("John", "Doe", "john.doe@gmail.com", ClientState.Active),
                autoSave: true
            );
            await _clientRepository.InsertAsync(
                new Client("Jane", "Smith", "jane.smith@gmail.com", ClientState.Pending),
                autoSave: true
            );
        }
    }
}