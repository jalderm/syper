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
using System.Text.Json;
using Npgsql;                        // For NpgsqlParameter
using NpgsqlTypes; 
using Volo.Abp.MultiTenancy;



namespace Syper.Workouts;

[Authorize(SyperPermissions.Workouts.Default)]
public class WorkoutAppService : ApplicationService, IWorkoutAppService
{
    private readonly IWorkoutRepository _repository;
    private readonly ICurrentTenant _currentTenant;

    public WorkoutAppService(ICurrentTenant currentTenant, IWorkoutRepository repository)
    {
        _currentTenant = currentTenant;
        _repository = repository;
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
        var dbContext = await _repository.GetDbContextAsync();

        var paramTenantId = new Npgsql.NpgsqlParameter("tenant_id", _currentTenant.Id ?? Guid.Empty)
        {
            NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid
        };

        var paramWorkoutJson = new Npgsql.NpgsqlParameter("p_workout_json", JsonSerializer.Serialize(input))
        {
            NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb
        };


        await dbContext.Database.ExecuteSqlRawAsync(
            "CALL update_workout(@tenant_id, @p_workout_json)",
            paramTenantId,
            paramWorkoutJson
        );
        return ObjectMapper.Map<Workout, WorkoutDto>(await _repository.GetAsync(id));
    }


//     public async Task<WorkoutDto> UpdateAsync(Guid id, CreateUpdateWorkoutDto input)
    // {
    //     var queryable = await _repository.GetQueryableAsync();

    //     var workout = await queryable
    //         .Include(w => w.WorkoutSections)
    //             .ThenInclude(s => s.WorkoutExercises)
    //                 // .ThenInclude(e => e.Sets)
    //         .FirstOrDefaultAsync(w => w.Id == id);

    //     if (workout == null)
    //         throw new Exception("Workout not found");

    //     // ✅ Update top-level properties
    //     workout.Name = input.Name;

    //     // ✅ Handle sections
    //     foreach (var inputSection in input.WorkoutSections)
    //     {
    //         var section = workout.WorkoutSections.FirstOrDefault(s => s.Id == inputSection.Id);

    //         if (section == null)
    //         {
    //             var newSection = ObjectMapper.Map<CreateUpdateWorkoutSectionDto, WorkoutSection>(inputSection);
    //             workout.WorkoutSections.Add(newSection);
    //         }
    //         else
    //         {
    //             section.Title = inputSection.Title;
    //             section.Colour = inputSection.Colour;

    //             foreach (var inputExercise in inputSection.WorkoutExercises)
    //             {
    //                 var ex = section.WorkoutExercises.FirstOrDefault(e => e.Id == inputExercise.Id);

    //                 if (ex == null)
    //                 {
    //                     // New exercise
    //                     var newEx = ObjectMapper.Map<CreateUpdateWorkoutExerciseDto, WorkoutExercise>(inputExercise);
    //                     section.WorkoutExercises.Add(newEx);
    //                 }
    //                 else
    //                 {
    //                     // Update exercise properties
    //                     ex.ExerciseId = inputExercise.ExerciseId;

    //                     // ✅ Handle sets (no re-attach or re-fetch!)
    //                     foreach (var inputSet in inputExercise.Sets)
    //                     {
    //                         var set = ex.Sets.FirstOrDefault(s => s.Id == inputSet.Id);

    //                         if (set != null)
    //                         {
    //                             // Update existing
    //                             set.Unit = inputSet.Unit;
    //                             set.UnitType = inputSet.UnitType;
    //                             set.Quantity = inputSet.Quantity;
    //                             set.QuantityType = inputSet.QuantityType;
    //                             set.Rest = inputSet.Rest;
    //                         }
    //                         else if (inputSet.Id == Guid.Empty)
    //                         {
    //                             // Only add new entities that don't have an ID yet
    //                             var newSet = ObjectMapper.Map<CreateUpdateSetDto, Set>(inputSet);
    //                             ex.Sets.Add(newSet);
    //                         }
    //                     }

    //                     // Remove deleted sets
    //                     var removedSets = ex.Sets
    //                         .Where(s => !inputExercise.Sets.Any(iset => iset.Id == s.Id))
    //                         .ToList();
    //                     foreach (var sToRemove in removedSets)
    //                         ex.Sets.Remove(sToRemove);
    //                 }
    //             }

    //             // Remove deleted exercises
    //             var removedExercises = section.WorkoutExercises
    //                 .Where(e => !inputSection.WorkoutExercises.Any(ie => ie.Id == e.Id))
    //                 .ToList();
    //             foreach (var exToRemove in removedExercises)
    //                 section.WorkoutExercises.Remove(exToRemove);
    //         }
    //     }

    //     // Remove deleted sections
    //     var removedSections = workout.WorkoutSections
    //         .Where(s => !input.WorkoutSections.Any(isec => isec.Id == s.Id))
    //         .ToList();
    //     foreach (var secToRemove in removedSections)
    //         workout.WorkoutSections.Remove(secToRemove);

    //     await _repository.UpdateAsync(workout);

    //     return ObjectMapper.Map<Workout, WorkoutDto>(workout);
    // }

    // public async Task<WorkoutDto> UpdateAsync(Guid id, CreateUpdateWorkoutDto input)
    // {
    //     var queryable = await _repository.GetQueryableAsync();

    //     var workout = await queryable
    //         .Include(w => w.WorkoutSections)
    //             .ThenInclude(s => s.WorkoutExercises)
    //                 .ThenInclude(e => e.Sets)
    //         .FirstOrDefaultAsync(w => w.Id == id);

    //     if (workout == null)
    //         throw new Exception("Workout not found");

    //     // Update scalar properties only (avoid overwriting collections)
    //     workout.Name = input.Name;

    //     // Handle WorkoutSections manually
    //     foreach (var inputSection in input.WorkoutSections)
    //     {
    //         var section = workout.WorkoutSections.FirstOrDefault(s => s.Id == inputSection.Id);

    //         if (section == null)
    //         {
    //             // New section
    //             var newSection = ObjectMapper.Map<CreateUpdateWorkoutSectionDto, WorkoutSection>(inputSection);
    //             workout.WorkoutSections.Add(newSection);
    //         }
    //         else
    //         {
    //             // Update existing section’s scalar properties
    //             section.Title = inputSection.Title;
    //             section.Colour = inputSection.Colour;

    //             // Handle WorkoutExercises manually
    //             foreach (var inputExercise in inputSection.WorkoutExercises)
    //             {
    //                 var ex = section.WorkoutExercises.FirstOrDefault(e => e.Id == inputExercise.Id);

    //                 if (ex == null)
    //                 {
    //                     var newEx = ObjectMapper.Map<CreateUpdateWorkoutExerciseDto, WorkoutExercise>(inputExercise);
    //                     section.WorkoutExercises.Add(newEx);
    //                 }
    //                 else
    //                 {
    //                     var removedSets = ex.Sets
    //                         .Where(s => !inputExercise.Sets.Any(iset => iset.Id == s.Id))
    //                         .ToList();

    //                     foreach (var sToRemove in removedSets)
    //                         ex.Sets.Remove(sToRemove);

    //                     // Update properties of existing exercise
    //                     ex.ExerciseId = inputExercise.ExerciseId;

    //                     // Update sets if you have them
    //                     foreach (var inputSet in inputExercise.Sets)
    //                     {
    //                         // Try to find an existing tracked set
    //                         var existingSet = ex.Sets.FirstOrDefault(s => s.Id == inputSet.Id);

    //                         if (existingSet != null)
    //                         {
    //                             // ✅ Update in place
    //                             existingSet.Unit = inputSet.Unit;
    //                             existingSet.UnitType = inputSet.UnitType;
    //                             existingSet.Quantity = inputSet.Quantity;
    //                             existingSet.QuantityType = inputSet.QuantityType;
    //                             existingSet.Rest = inputSet.Rest;
    //                         }
    //                         else
    //                         {
    //                             // ✅ Only add truly new sets (no ID)
    //                             if (inputSet.Id == Guid.Empty)
    //                             {
    //                                 var newSet = ObjectMapper.Map<SetDto, Set>(inputSet);
    //                                 ex.Sets.Add(newSet);
    //                             }
    //                             else
    //                             {
    //                                 // If an existing ID wasn’t in the tracked list, attach instead of add
    //                                 var dbContext = await _repository.GetDbContextAsync();
    //                                 var trackedSet = await dbContext.Set<Set>().FindAsync(inputSet.Id);

    //                                 if (trackedSet != null)
    //                                 {
    //                                     // Already exists in DB, update it directly
    //                                     ObjectMapper.Map(inputSet, trackedSet);
    //                                     ex.Sets.Add(trackedSet); // attach to parent if necessary
    //                                 }
    //                                 else
    //                                 {
    //                                     // Could not find — treat as new
    //                                     var newSet = ObjectMapper.Map<SetDto, Set>(inputSet);
    //                                     ex.Sets.Add(newSet);
    //                                 }
    //                             }
    //                         }
    //                     }

    //                 }


    //             }


    //             // Remove deleted exercises
    //             var removedExercises = section.WorkoutExercises
    //                 .Where(e => !inputSection.WorkoutExercises.Any(ie => ie.Id == e.Id))
    //                 .ToList();
    //             foreach (var exToRemove in removedExercises)
    //                 section.WorkoutExercises.Remove(exToRemove);
    //         }
    //     }

    //     // Remove deleted sections
    //     var removedSections = workout.WorkoutSections
    //         .Where(s => !input.WorkoutSections.Any(isec => isec.Id == s.Id))
    //         .ToList();
    //     foreach (var secToRemove in removedSections)
    //         workout.WorkoutSections.Remove(secToRemove);

    //     await _repository.UpdateAsync(workout);

    //     return ObjectMapper.Map<Workout, WorkoutDto>(workout);
    // }


    [Authorize(SyperPermissions.Workouts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
