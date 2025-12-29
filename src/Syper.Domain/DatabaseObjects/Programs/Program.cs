using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Syper.MultiTenancy;
using Syper.ClientStateEnum;
using Syper.Clients;
using Syper.WorkoutExercises;
using Syper.WorkoutSections;
using Syper.ScheduleDays;


namespace Syper.Programs
{
    public class Program : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        [MaxLength(32)]
        public required string Name { get; set; }

        // program type - ongoing or fixed (subcription type)

        // Duration (weeks)
        public int Duration { get; set; }

        // Goals
        // Own data structure?
        [MaxLength(255)]
        public string? Goal { get; set; }

        // Do this off another table if required
        [MaxLength(255)]
        public string? ShortDescription { get; set; }

        public required List<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();

        public Program()
        {

        }
    }

}
