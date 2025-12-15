using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Syper.Clients;
using Syper.MultiTenancy;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syper.ClientCoachSubscriptions
{
    public class ClientCoachSubscription : MultiTenantFullAuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public required Client Client { get; set; }

        public ClientCoachSubscription()
        {

        }
    }

}
