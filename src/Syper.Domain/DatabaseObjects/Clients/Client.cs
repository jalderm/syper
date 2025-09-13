using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Syper.ClientStateEnum;

namespace Syper.Coaching
{
    public class Client : FullAuditedAggregateRoot<Guid>
    {

        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        public ClientState ClientState { get; set; } = ClientState.Pending;

        public Client(string firstName, string lastName, string email, ClientState clientState)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            ClientState = clientState;

            if (email.Contains("@") == false)
            {
                throw new ArgumentException("Email must be valid", nameof(email));
            }
        }
    }

}
