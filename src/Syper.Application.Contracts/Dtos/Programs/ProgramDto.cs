using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.WeeklySchedules;

namespace Syper.Programs;

public class ProgramDto : AuditedEntityDto<Guid>
{
    public required string Name { get; set; }
    public int Duration { get; set; }
    public string? Goal { get; set; }
    public string? ShortDescription { get; set; }
    public required ICollection<WeeklyScheduleDto> Weeks { get; set; } = new List<WeeklyScheduleDto>();
}