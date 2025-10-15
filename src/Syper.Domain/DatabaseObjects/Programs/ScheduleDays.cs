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
using Syper.ScheduleActivities;
using Syper.WeeklySchedules;

namespace Syper.ScheduleDays
{
    public class ScheduleDay : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public required DayOfWeek DayOfWeek { get; set; }
        public required ICollection<ScheduleActivity> Activities { get; set; }
        public string? Notes { get; set; }

        public required Guid WeeklyScheduleId { get; set; }
        [ForeignKey("WeeklyScheduleId")]
        public WeeklySchedule WeeklySchedule { get; set; }

        public ScheduleDay()
        {

        }
    }

}