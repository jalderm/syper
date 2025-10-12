using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Syper.Exercises;
using Syper.MultiTenancy;
using Syper.Sets;
using Syper.WorkoutExercises;
using Syper.Workouts;

namespace Syper.WorkoutSections
{
    public class WorkoutSection : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        [MaxLength(32)]
        public required string Title { get; set; }
        // Added
        [MaxLength(255)]
        public string? ShortDescription { get; set; }
        [MaxLength(32)]
        public required string Colour { get; set; }
        
        public required ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
        public required Guid WorkoutId { get; set; } // Foreign key to Workout
        [ForeignKey(nameof(WorkoutId))]
        public required Workout Workout { get; set; } // Foreign key to Workout[MaxLength(255)]

        

        public WorkoutSection()
        {

        }
    }

}
