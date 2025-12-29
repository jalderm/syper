using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.ActivityTypeEnum;
using Syper.Workouts;

namespace Syper.ScheduleActivities;

public class ScheduleActivityDto : AuditedEntityDto<Guid>
{
    public required ActivityType Type { get; set; }
    public Guid? WorkoutId { get; set; } // if type is workout, this is the workout id
    public WorkoutDto Workout { get; set; } // if type is workout, this is the workout id
    [Required]
    public required Guid ScheduleDayId { get; set; } // Foreign key to schedule day

}