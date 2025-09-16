using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Syper.Clients
{
    // 1. Coach
    // public class Coach : FullAuditedAggregateRoot<Guid>
    // {
    //     public Guid UserId { get; set; } // Linked to ABP Identity User
    //     [MaxLength(500)]
    //     public string Bio { get; set; }
    //     // public string Certifications { get; set; }
    //     // public string Specializations { get; set; }
    //     [MaxLength(500)]
    //     public string ImageUrl { get; set; } // URL to coach's profile image

    //     public ICollection<TrainingPlan> TrainingPlans { get; set; }
    //     public ICollection<CoachAthleteAssignment> AthleteAssignments { get; set; }
    // }

    // // 2. Athlete
    // public class Athlete : FullAuditedAggregateRoot<Guid>
    // {
    //     public Guid UserId { get; set; } // Linked to ABP Identity User
    //     public int? Age { get; set; }
    //     public double? HeightCm { get; set; }
    //     public double? WeightKg { get; set; }
    //     public string Goals { get; set; }

    //     public ICollection<CoachAthleteAssignment> CoachAssignments { get; set; }
    //     public ICollection<ActivityRecord> Activities { get; set; } // For later integration
    // }

    // // 3. TrainingPlan
    // public class TrainingPlan : FullAuditedAggregateRoot<Guid>
    // {
    //     public Guid CoachId { get; set; }
    //     public string Name { get; set; }
    //     public string Description { get; set; }
    //     public int DurationWeeks { get; set; }
    //     public TrainingPlanStatus Status { get; set; }

    //     public Coach Coach { get; set; }
    //     public ICollection<WorkoutSession> Sessions { get; set; }
    // }

    // public enum TrainingPlanStatus
    // {
    //     Draft = 0,
    //     Published = 1,
    //     Archived = 2
    // }

    // // 4. WorkoutSession
    // public class WorkoutSession : FullAuditedAggregateRoot<Guid>
    // {
    //     public Guid TrainingPlanId { get; set; }
    //     public int DayNumber { get; set; } // e.g., Day 1, Day 2
    //     public DateTime? ScheduledDate { get; set; }
    //     public string Title { get; set; }
    //     public string Instructions { get; set; }

    //     public TrainingPlan TrainingPlan { get; set; }
    //     public ICollection<Exercise> Exercises { get; set; }
    // }

    // // 5. Exercise
    // public class Exercise : FullAuditedAggregateRoot<Guid>
    // {
    //     public Guid WorkoutSessionId { get; set; }
    //     public string Name { get; set; }
    //     public ExerciseType Type { get; set; }
    //     public int? Sets { get; set; }
    //     public int? Reps { get; set; }
    //     public double? DistanceKm { get; set; }
    //     public TimeSpan? Duration { get; set; }
    //     public string TargetIntensity { get; set; }

    //     public WorkoutSession WorkoutSession { get; set; }
    // }

    // public enum ExerciseType
    // {
    //     Run,
    //     Strength,
    //     Cardio,
    //     Mobility,
    //     Other
    // }

    // // 6. CoachAthleteAssignment (junction table)
    // public class CoachAthleteAssignment : FullAuditedEntity<Guid>
    // {
    //     public Guid CoachId { get; set; }
    //     public Guid AthleteId { get; set; }
    //     public DateTime StartDate { get; set; }
    //     public DateTime? EndDate { get; set; }
    //     public AssignmentStatus Status { get; set; }

    //     public Coach Coach { get; set; }
    //     public Athlete Athlete { get; set; }
    // }

    // public enum AssignmentStatus
    // {
    //     Active = 0,
    //     Paused = 1,
    //     Ended = 2
    // }
}
