using System;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Syper.Clients;

public class ClientDto : AuditedEntityDto<Guid>
{
        public Guid ClientId { get; set; } // Linked to ABP Identity User
        
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
}