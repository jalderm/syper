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
using Syper.Programs;

namespace Syper.ScheduleDays
{
    public class ScheduleDay : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public required int DayOffSet { get; set; } // 0 based index for day in week
        public required ICollection<ScheduleActivity> Activities { get; set; }
        public string? Notes { get; set; }

        public required Guid ProgramId { get; set; }
        [ForeignKey(nameof(ProgramId))]
        public required Program Program { get; set; }

        public ScheduleDay()
        {

        }
    }

}