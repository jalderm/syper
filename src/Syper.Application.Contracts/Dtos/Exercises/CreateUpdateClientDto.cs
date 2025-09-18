using System;
using System.ComponentModel.DataAnnotations;
using Syper.ExerciseCategories;

namespace Syper.Exercises;

public class CreateUpdateExerciseDto
{
    [Required]
    [StringLength(32)]
    public string Title { get; set; } = string.Empty;

    public ExerciseCategoryEnum ExerciseCategory { get; set; } = ExerciseCategoryEnum.Distance;
}