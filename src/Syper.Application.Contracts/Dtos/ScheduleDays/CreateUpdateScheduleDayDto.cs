using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.WeeklySchedules;
using Syper.ScheduleActivities;

namespace Syper.ScheduleDays;

public class CreateUpdateScheduleDayDto : AuditedEntityDto<Guid>
{
    [Required]
    public required DayOfWeek DayOfWeek { get; set; }
    [Required]
    public required ICollection<CreateUpdateScheduleActivityDto> Activities { get; set; }
    public string? Notes { get; set; } // Optional notes for the day
    [Required]
    public required Guid WeeklyScheduleId { get; set; } // Foreign key to weekly schedule
    
}