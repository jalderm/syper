using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.WeeklySchedules;
using Syper.ActivityTypeEnum;

namespace Syper.ScheduleActivities;

public class CreateUpdateScheduleActivityDto : AuditedEntityDto<Guid>
{
    public required ActivityType ActivityType { get; set; }
    public Guid? WorkoutId { get; set; } // if type is workout, this is the workout id
    [Required]
    public required Guid ScheduleDayId { get; set; } // Foreign key to schedule day

}