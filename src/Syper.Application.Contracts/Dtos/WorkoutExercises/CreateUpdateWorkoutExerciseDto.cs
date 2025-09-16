using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.Sets;

namespace Syper.WorkoutExercises;

public class CreateUpdateWorkoutExerciseDto
{
    [Required]
    public required Guid WorkoutSectionId { get; set; } // Foreign key to workout section
    public required List<SetDto> Sets { get; set; } = new List<SetDto>();
}