using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;

namespace Syper.Clients;

public class ClientDto : AuditedEntityDto<Guid>
{       
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        
        public ClientState ClientState { get; set; }
}