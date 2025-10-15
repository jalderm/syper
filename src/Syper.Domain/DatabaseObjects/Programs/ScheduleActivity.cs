using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Syper.MultiTenancy;
using Syper.ClientStateEnum;
using Syper.Clients;
using Syper.WorkoutExercises;
using Syper.Workouts;
using Syper.ScheduleDays;
using Syper.ActivityTypeEnum;

namespace Syper.ScheduleActivities
{
    // ScheduleActivity - Becomes Program Workout
    public class ScheduleActivity : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public required ActivityType Type { get; set; }

        public Guid? WorkoutId { get; set; } // if type is workout, this is the workout id
        [ForeignKey("WorkoutId")]
        public Workout? Workout { get; set; }

        public required Guid ScheduleDayId { get; set; }
        [ForeignKey("ScheduleDayId")]
        public ScheduleDay ScheduleDay { get; set; }

        public ScheduleActivity()
        {

        }
    }
}