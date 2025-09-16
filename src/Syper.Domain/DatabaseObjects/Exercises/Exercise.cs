using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Syper.ExerciseCategories;

namespace Syper.Exercises
{
    public class Exercise : FullAuditedAggregateRoot<Guid>
    {
        [MaxLength(32)]
        public string Title { get; set; }
        public ExerciseCategory ExerciseCategory { get; set; } = ExerciseCategory.Distance;


        public Exercise(string title, ExerciseCategory exerciseCategory)
        {
            Title = title;
            ExerciseCategory = exerciseCategory;
        }
    }

}
