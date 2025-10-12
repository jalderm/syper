using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Syper.Exercises;
using Syper.MultiTenancy;
using Syper.Sets;
using Syper.WorkoutSections;

namespace Syper.WorkoutExercises
{
    public class WorkoutExercise : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public required Guid ExerciseId { get; set; } // Foreign key to Exercise

        [ForeignKey(nameof(ExerciseId))]
        public required Exercise Exercise { get; set; } // Reference to Exercise
        public required ICollection<Set> Sets { get; set; } = new List<Set>();
        public int? Repeats { get; set; } // Number of times to repeat this exercise in the section


        // References back up the tree
        public required Guid WorkoutSectionId { get; set; } // Foreign key to Exercise

        [ForeignKey(nameof(WorkoutSectionId))]
        public required WorkoutSection WorkoutSection { get; set; } // Reference to Exercise

        
        public WorkoutExercise()
        {
            
        }
    }

}
