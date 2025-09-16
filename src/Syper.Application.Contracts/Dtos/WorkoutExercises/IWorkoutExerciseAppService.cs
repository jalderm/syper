using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.WorkoutExercises;

public interface IWorkoutExerciseAppService :
    ICrudAppService< //Defines CRUD methods
        WorkoutExerciseDto, //Used to show Workouts
        Guid, //Primary key of the Workout entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateWorkoutExerciseDto> //Used to create/update a Workout
{

}