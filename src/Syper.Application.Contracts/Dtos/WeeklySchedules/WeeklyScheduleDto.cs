using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.WeeklySchedules;
using Syper.ScheduleDays;


namespace Syper.WeeklySchedules;

public class WeeklyScheduleDto : AuditedEntityDto<Guid>
{
    [Required]
    public required ICollection<ScheduleDayDto> ScheduleDays { get; set; } = new List<ScheduleDayDto>();
    public string? Notes { get; set; } // Optional notes for the week
    [Required]
    public required Guid ProgramId { get; set; } // Foreign key to program
    
}