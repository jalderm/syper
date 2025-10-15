using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.WeeklySchedules;
using Syper.ScheduleDays;


namespace Syper.WeeklySchedules;

public class CreateUpdateWeeklyScheduleDto : AuditedEntityDto<Guid>
{
    [Required]
    public required ICollection<CreateUpdateScheduleDayDto> ScheduleDays = new List<CreateUpdateScheduleDayDto>();
    public string? Notes { get; set; } // Optional notes for the week
    [Required]
    public required Guid ProgramId { get; set; } // Foreign key to program
    
}