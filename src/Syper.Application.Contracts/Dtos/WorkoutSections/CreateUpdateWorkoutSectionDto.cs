using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.WorkoutExercises;

namespace Syper.WorkoutSections;

public class CreateUpdateWorkoutSectionDto
{
    public required string Title { get; set; } = string.Empty;
    public required string Colour { get; set; } = string.Empty;
    public required List<WorkoutExerciseDto> WorkoutExercises { get; set; } = new List<WorkoutExerciseDto>();
    public required Guid WorkoutId { get; set; } // Foreign key to Workout
}