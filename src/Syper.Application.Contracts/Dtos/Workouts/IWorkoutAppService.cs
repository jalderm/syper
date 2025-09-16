using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.Workouts;

public interface IWorkoutAppService :
    ICrudAppService< //Defines CRUD methods
        WorkoutDto, //Used to show Workouts
        Guid, //Primary key of the Workout entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateWorkoutDto> //Used to create/update a Workout
{

}