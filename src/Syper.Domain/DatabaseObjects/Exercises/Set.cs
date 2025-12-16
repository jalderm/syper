using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Syper.MultiTenancy;

using Syper.WorkoutExercises;

namespace Syper.Sets
{
    public class Set : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public decimal Unit { get; set; } // "kg", "km", "reps", etc.
        public SetUnitType UnitType { get; set; } // "kg", "km", etc. - weight, distance?
        public decimal Quantity { get; set; } // 100, 5, 10
        public SetQuantityType QuantityType { get; set; } // reps / time?
        public TimeSpan? Rest { get; set; } // optional for lifting

        // Target HR zone
        public decimal? UpperPercentageOfMaxHR { get; set; } // 1-10 scale
        // More general
        public int? PerceivedEffort { get; set; } // 1-10 scale

        public Guid WorkoutExerciseId { get; set; } // Foreign key to WorkoutExercise
        [ForeignKey(nameof(WorkoutExerciseId))]
        public required WorkoutExercise WorkoutExercise { get; set; } // Reference to WorkoutExercise]
        public required int SortOrder { get; set; }

        public Set()
        {
        }
    }

}
