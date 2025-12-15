using AutoMapper;
using Syper.Clients;
using Syper.Sets;
using Syper.Workouts;
using Syper.WorkoutSections;
using Syper.WorkoutExercises;
using Syper.Exercises;
using Syper.Programs;
using Syper.ScheduleDays;
using Syper.ScheduleActivities;

namespace Syper;

public class SyperApplicationAutoMapperProfile : Profile
{
    public SyperApplicationAutoMapperProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<CreateUpdateClientDto, Client>();
        CreateMap<CreateUpdateWorkoutDto, Workout>();
        CreateMap<Workout, WorkoutDto>();
        CreateMap<WorkoutSection, WorkoutSectionDto>();
        CreateMap<WorkoutSectionDto, WorkoutSection>();
        CreateMap<CreateUpdateWorkoutSectionDto, WorkoutSection>();
        CreateMap<WorkoutExercise, WorkoutExerciseDto>();
        CreateMap<WorkoutExerciseDto, WorkoutExercise>();
        CreateMap<CreateUpdateWorkoutExerciseDto, WorkoutExercise>();
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<Set, SetDto>();
        CreateMap<SetDto, Set>();
        CreateMap<CreateUpdateSetDto, Set>();

        CreateMap<Program, ProgramDto>();
        CreateMap<ProgramDto, Program>();
        CreateMap<ScheduleDay, ScheduleDayDto>();
        CreateMap<ScheduleDayDto, ScheduleDay>();
        CreateMap<ScheduleActivity, ScheduleActivityDto>();
        CreateMap<ScheduleActivityDto, ScheduleActivity>();

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
