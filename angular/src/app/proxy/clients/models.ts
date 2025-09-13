import type { AuditedEntityDto } from '@abp/ng.core';
import type { ClientState } from '../client-state-enum/client-state.enum';

export interface ClientDto extends AuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  email?: string;
  clientState?: ClientState;
}

export interface CreateUpdateClientDto {
  firstName: string;
  lastName: string;
  email: string;
  clientState?: ClientState;
}
