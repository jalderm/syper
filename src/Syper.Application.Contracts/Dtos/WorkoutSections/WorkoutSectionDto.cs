using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Syper.Sets;
using Syper.WorkoutExercises;

namespace Syper.WorkoutSections;

public class WorkoutSectionDto : AuditedEntityDto<Guid>
{
    public required string Title { get; set; }
    public required string Colour { get; set; }
    public required List<WorkoutExerciseDto> WorkoutExercises { get; set; } = new List<WorkoutExerciseDto>();
    public required Guid WorkoutId { get; set; } // Foreign key to Workout
        
}