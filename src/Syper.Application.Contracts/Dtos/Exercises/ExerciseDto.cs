using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.ClientStateEnum;
using Syper.WorkoutExercises;
using Syper.ExerciseCategories;

namespace Syper.Workouts;

public class ExerciseDto : AuditedEntityDto<Guid>
{
    public required string Title { get; set; }
    public required ExerciseCategory ExerciseCategory { get; set; } = ExerciseCategory.Distance;
}