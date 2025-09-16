using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.WorkoutExercises;

namespace Syper.Workouts;

public class CreateUpdateWorkoutDto
{
    [Required]
    [StringLength(32)]
    public required string Title { get; set; } = string.Empty;

    public Guid? AuthorId { get; set; }
    public required List<WorkoutExerciseDto> WorkoutExercises { get; set; } = new List<WorkoutExerciseDto>();
}