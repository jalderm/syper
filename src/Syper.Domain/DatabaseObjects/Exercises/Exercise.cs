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
        public ExerciseCategoryEnum ExerciseCategory { get; set; } = ExerciseCategoryEnum.Distance;


        public Exercise(string title, ExerciseCategoryEnum exerciseCategory)
        {
            Title = title;
            ExerciseCategory = exerciseCategory;
        }
    }

}
