using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.ScheduleDays;

namespace Syper.Programs;

public class CreateUpdateProgramDto : AuditedEntityDto<Guid>
{
    public required string Name { get; set; }
    public required int Duration { get; set; }
    public string? Goal { get; set; }
    public string? ShortDescription { get; set; }
    public required List<CreateUpdateScheduleDayDto> ScheduleDays { get; set; } = new List<CreateUpdateScheduleDayDto>();
}