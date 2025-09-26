using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;
using Syper.Sets;

namespace Syper.Sets;

public class CreateUpdateSetDto : AuditedEntityDto<Guid>
{
        public decimal Unit { get; set; } = new decimal(0); // "kg", "km", "reps", etc.
        public SetUnitType UnitType { get; set; } // "kg", "km", "reps", etc. - weight, distance?
        public decimal Quantity { get; set; }  = new decimal(0);// 100, 5, 10
        public SetQuantityType QuantityType { get; set; } // reps / time?
        public TimeSpan? Rest { get; set; } // optional for lifting
}