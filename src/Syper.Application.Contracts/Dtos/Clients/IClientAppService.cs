using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Syper.Clients;

public interface IClientAppService :
    ICrudAppService< //Defines CRUD methods
        ClientDto, //Used to show clients
        Guid, //Primary key of the client entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateClientDto> //Used to create/update a client
{

}