using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Syper.Clients;
using Syper.ClientStateEnum;
using Syper.Exercises;
using Syper.ExerciseCategories;

namespace Syper;

public class SyperDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Client, Guid> _clientRepository;
    private readonly IRepository<Exercise, Guid> _exerciseRepository;

    public SyperDataSeederContributor(
        IRepository<Client, Guid> clientRepository,
        IRepository<Exercise, Guid> exerciseRepository
        )
    {
        _clientRepository = clientRepository;
        _exerciseRepository = exerciseRepository;
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

        if (await _exerciseRepository.GetCountAsync() <= 0)
        {
            await _exerciseRepository.InsertAsync(
                new Exercise("Run (Distance)", ExerciseCategoryEnum.Distance),
                autoSave: true
            );
            await _exerciseRepository.InsertAsync(
                new Exercise("Run (Time)", ExerciseCategoryEnum.Time),
                autoSave: true
            );
        }
    }
}