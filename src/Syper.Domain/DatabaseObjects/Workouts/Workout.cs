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


namespace Syper.Workouts
{
    public class Workout : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        [MaxLength(32)]
        public required string Name { get; set; }

        public required List<WorkoutSection> WorkoutSections { get; set; } = new List<WorkoutSection>();

        // Do this off another table if required
        [MaxLength(255)]
        public string? ShortDescription { get; set; }

        public Workout()
        {

        }
    }

}
