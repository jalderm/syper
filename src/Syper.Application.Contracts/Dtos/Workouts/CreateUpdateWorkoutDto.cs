using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Syper.WorkoutSections;

namespace Syper.Workouts;

public class CreateUpdateWorkoutDto : AuditedEntityDto<Guid>
{
    [Required]
    [StringLength(32)]
    public required string Name { get; set; } = string.Empty;

    // public Guid? AuthorId { get; set; }
    public required List<CreateUpdateWorkoutSectionDto> WorkoutSections { get; set; } = new List<CreateUpdateWorkoutSectionDto>();
    public string? ShortDescription { get; set; }
}