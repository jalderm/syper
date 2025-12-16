using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;

namespace Syper.Sets;

public class SetDto : AuditedEntityDto<Guid>
{
        public decimal Unit { get; set; } // "kg", "km", "reps", etc.
        public SetUnitType UnitType { get; set; } // "kg", "km", "reps", etc. - weight, distance?
        public decimal Quantity { get; set; } // 100, 5, 10
        public SetQuantityType QuantityType { get; set; } // reps / time?
        public TimeSpan? Rest { get; set; } // optional for lifting
        public required Guid WorkoutExerciseId { get; set; } // Foreign key to workout section
        public required int SortOrder { get; set; }
}