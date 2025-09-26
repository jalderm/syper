using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Syper.Sets;

namespace Syper.WorkoutExercises;

public class CreateUpdateWorkoutExerciseDto : AuditedEntityDto<Guid>
{
    public required Guid ExerciseId { get; set; } = new Guid(); // Foreign key to Exercise
    [Required]
    public required Guid WorkoutSectionId { get; set; } = new Guid(); // Foreign key to workout section
    public required List<SetDto> Sets { get; set; } = new List<SetDto>();
}