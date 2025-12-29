using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.ActivityTypeEnum;

namespace Syper.ScheduleActivities;

public class CreateUpdateScheduleActivityDto : AuditedEntityDto<Guid>
{
    public required ActivityType Type { get; set; } = ActivityType.Workout;
    public Guid WorkoutId { get; set; }
    [Required]
    public required Guid ScheduleDayId { get; set; } = new Guid(); // Foreign key to schedule day

}