using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Syper.ClientStateEnum;
using Syper.WorkoutSections;

namespace Syper.Workouts;

public class WorkoutDto : AuditedEntityDto<Guid>
{
    public required string Name { get; set; }
    public required List<WorkoutSectionDto> WorkoutSections { get; set; } = new List<WorkoutSectionDto>();
    public string? ShortDescription { get; set; }
}