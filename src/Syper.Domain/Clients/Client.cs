using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace Syper.Coaching
{
    public class Client : FullAuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; } // Linked to ABP Identity User

        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }

        public Client(string firstName, string lastName, string email)
        {
            ClientId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            
            if (email.Contains("@") == false)
            {
                throw new ArgumentException("Email must be valid", nameof(email));
            }
        }
    }

}
