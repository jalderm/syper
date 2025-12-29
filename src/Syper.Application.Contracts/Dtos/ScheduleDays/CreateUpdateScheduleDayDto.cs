using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.ScheduleActivities;

namespace Syper.ScheduleDays;

public class CreateUpdateScheduleDayDto : AuditedEntityDto<Guid>
{
    [Required]
    public required int DayOffSet { get; set; }  = 0;
    [Required]
    public required List<CreateUpdateScheduleActivityDto> Activities { get; set; } = new List<CreateUpdateScheduleActivityDto>();
    public string? Notes { get; set; } // Optional notes for the day
    [Required]
    public required Guid ProgramId { get; set; } = new Guid(); // Foreign key to weekly schedule
    
}