using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.ScheduleActivities;

namespace Syper.ScheduleDays;

public class ScheduleDayDto : AuditedEntityDto<Guid>
{
    [Required]
    public required int DayOffSet { get; set; } 
    [Required]
    public required ICollection<ScheduleActivityDto> Activities { get; set; }
    public string? Notes { get; set; } // Optional notes for the day
    [Required]
    public required Guid ProgramId { get; set; } // Foreign key to weekly schedule
    
}