using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.Programs;

public interface IProgramAppService :
    ICrudAppService< //Defines CRUD methods
        ProgramDto, //Used to show clients
        Guid, //Primary key of the client entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProgramDto> //Used to create/update a client
{

}