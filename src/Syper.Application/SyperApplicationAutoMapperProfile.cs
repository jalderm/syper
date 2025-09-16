using AutoMapper;
using Syper.Clients;
using Syper.Sets;
using Syper.Workouts;
using Syper.WorkoutSections;
using Syper.WorkoutExercises;
using Syper.Exercises;

namespace Syper;

public class SyperApplicationAutoMapperProfile : Profile
{
    public SyperApplicationAutoMapperProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<CreateUpdateClientDto, Client>();
        CreateMap<Workout, WorkoutDto>();
        CreateMap<WorkoutSection, WorkoutSectionDto>();
        CreateMap<WorkoutExercise, WorkoutExerciseDto>();
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<Set, SetDto>();
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
