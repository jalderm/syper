using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Syper.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Syper.WorkoutRepository;
using Microsoft.EntityFrameworkCore;
using Syper.WorkoutSections;
using Syper.WorkoutExercises;
using Syper.Sets;
using Syper.SetRepository;


namespace Syper.Workouts;

[Authorize(SyperPermissions.Workouts.Default)]
public class WorkoutAppService : ApplicationService, IWorkoutAppService
{
    private readonly IWorkoutRepository _repository;
    private readonly ISetRepository _setRepository;

    public WorkoutAppService(IWorkoutRepository repository, ISetRepository setRepository)
    {
        _repository = repository;
        _setRepository = setRepository;
    }

    public async Task<WorkoutDto> GetAsync(Guid id)
    {
        var workout = await _repository.GetWithSectionsAndExercisesAsync(id);
        if (workout == null)
        {
            throw new Exception($"Workout with id {id} was not found.");
        }

        return ObjectMapper.Map<Workout, WorkoutDto>(workout);
    }

    public async Task<PagedResultDto<WorkoutDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var Workouts = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<WorkoutDto>(
            totalCount,
            ObjectMapper.Map<List<Workout>, List<WorkoutDto>>(Workouts)
        );
    }

    [Authorize(SyperPermissions.Workouts.Create)]
    public async Task<WorkoutDto> CreateAsync(CreateUpdateWorkoutDto input)
    {
        var Workout = ObjectMapper.Map<CreateUpdateWorkoutDto, Workout>(input);
        await _repository.InsertAsync(Workout);
        return ObjectMapper.Map<Workout, WorkoutDto>(Workout);
    }

    [Authorize(SyperPermissions.Workouts.Edit)]
    public async Task<WorkoutDto> UpdateAsync(Guid id, CreateUpdateWorkoutDto input)
    {
        var queryable = await _repository.GetQueryableAsync();

        var workout = await queryable
            .Include(w => w.WorkoutSections)
                .ThenInclude(s => s.WorkoutExercises)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
            throw new Exception("Workout not found");

        // Update scalar properties only (avoid overwriting collections)
        workout.Name = input.Name;

        // Handle WorkoutSections manually
        foreach (var inputSection in input.WorkoutSections)
        {
            var section = workout.WorkoutSections.FirstOrDefault(s => s.Id == inputSection.Id);

            if (section == null)
            {
                // New section
                var newSection = ObjectMapper.Map<CreateUpdateWorkoutSectionDto, WorkoutSection>(inputSection);
                workout.WorkoutSections.Add(newSection);
            }
            else
            {
                // Update existing section’s scalar properties
                section.Title = inputSection.Title;
                section.Colour = inputSection.Colour;

                // Handle WorkoutExercises manually
                foreach (var inputExercise in inputSection.WorkoutExercises)
                {
                    var ex = section.WorkoutExercises.FirstOrDefault(e => e.Id == inputExercise.Id);

                    if (ex == null)
                    {
                        var newEx = ObjectMapper.Map<CreateUpdateWorkoutExerciseDto, WorkoutExercise>(inputExercise);
                        section.WorkoutExercises.Add(newEx);
                    }
                    else
                    {
                        // Update properties of existing exercise
                        ex.ExerciseId = inputExercise.ExerciseId;
                        // etc. for relevant fields

                        // Update sets if you have them
                        foreach (var inputSet in inputExercise.Sets)
                        {
                            var set = ex.Sets.FirstOrDefault(s => s.Id == inputSet.Id);
                            if (set == null) {
                                Set existingSet = null;
                                if (inputSet.Id != Guid.Empty)
                                    existingSet = await _setRepository.FindAsync(inputSet.Id);

                                if (existingSet != null)
                                {
                                    ObjectMapper.Map(inputSet, existingSet);
                                    ex.Sets.Add(existingSet);
                                }
                                else
                                {
                                    ex.Sets.Add(ObjectMapper.Map<SetDto, Set>(inputSet));
                                }
                            }
                            else
                            {
                                set.Unit = inputSet.Unit;
                                set.UnitType = inputSet.UnitType;
                                set.Quantity = inputSet.Quantity;
                                set.QuantityType = inputSet.QuantityType;
                                set.Rest = inputSet.Rest;
                            }

                        }
                    }
                }

                // Remove deleted exercises
                var removedExercises = section.WorkoutExercises
                    .Where(e => !inputSection.WorkoutExercises.Any(ie => ie.Id == e.Id))
                    .ToList();
                foreach (var exToRemove in removedExercises)
                    section.WorkoutExercises.Remove(exToRemove);
            }
        }

        // Remove deleted sections
        var removedSections = workout.WorkoutSections
            .Where(s => !input.WorkoutSections.Any(isec => isec.Id == s.Id))
            .ToList();
        foreach (var secToRemove in removedSections)
            workout.WorkoutSections.Remove(secToRemove);

        await _repository.UpdateAsync(workout);

        return ObjectMapper.Map<Workout, WorkoutDto>(workout);
    }


    [Authorize(SyperPermissions.Workouts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
