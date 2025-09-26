using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.Exercises;

using Syper.Sets;

namespace Syper.WorkoutExercises;

public class WorkoutExerciseDto : AuditedEntityDto<Guid>
{
    public required Guid ExerciseId { get; set; } // Foreign key to Exercise
    public required ExerciseDto Exercise { get; set; } // Foreign key to Exercise
    
    
    public required Guid WorkoutSectionId { get; set; } // Foreign key to workout section
    public required List<SetDto> Sets { get; set; } = new List<SetDto>();
        
}