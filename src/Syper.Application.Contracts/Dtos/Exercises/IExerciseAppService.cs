using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.Exercises;

public interface IExerciseAppService :
    ICrudAppService< //Defines CRUD methods
        ExerciseDto, //Used to show clients
        Guid, //Primary key of the client entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateExerciseDto> //Used to create/update a client
{

}