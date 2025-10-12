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


namespace Syper.ProgramWorkouts
{
    public class ProgramWorkout
    {
        DateTime Date { get; set; }
        public required Guid WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout? Workout { get; set; }

        public ProgramWorkout()
        {

        }
    }

}