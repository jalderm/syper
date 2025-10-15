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
using Syper.Programs;

namespace Syper.WeeklySchedules
{
    public class WeeklySchedule : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public required ICollection<ScheduleDay> ScheduleDays = new List<ScheduleDay>();
        public string? Notes { get; set; }

        public required Guid ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public Program Program { get; set; }

        public WeeklySchedule()
        {

        }
    }

}