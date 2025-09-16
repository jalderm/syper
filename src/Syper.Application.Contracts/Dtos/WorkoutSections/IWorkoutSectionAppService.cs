using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.WorkoutSections;

public interface IWorkoutSectionAppService :
    ICrudAppService< //Defines CRUD methods
        WorkoutSectionDto, //Used to show Workouts
        Guid, //Primary key of the Workout entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateWorkoutSectionDto> //Used to create/update a Workout
{

}