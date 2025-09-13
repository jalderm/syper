import type { AuditedEntityDto } from '@abp/ng.core';

export interface ClientDto extends AuditedEntityDto<string> {
  clientId?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
}

export interface CreateUpdateClientDto {
  firstName: string;
  lastName: string;
  email: string;
}
