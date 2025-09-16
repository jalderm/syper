using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.MultiTenancy;


namespace Syper.MultiTenancy
{
    public class MultiTenantFullAuditedAggregateRoot<TKey> : FullAuditedAggregateRoot<TKey>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        protected MultiTenantFullAuditedAggregateRoot()
        {
        }

        protected MultiTenantFullAuditedAggregateRoot(TKey id)
            : base(id)
        {
        }
    }

}
