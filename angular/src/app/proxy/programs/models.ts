import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateScheduleDayDto, ScheduleDayDto } from '../schedule-days/models';

export interface CreateUpdateProgramDto extends AuditedEntityDto<string> {
  name: string;
  duration: number;
  goal?: string;
  shortDescription?: string;
  scheduleDays: CreateUpdateScheduleDayDto[];
}

export interface ProgramDto extends AuditedEntityDto<string> {
  name: string;
  duration: number;
  goal?: string;
  shortDescription?: string;
  scheduleDays: ScheduleDayDto[];
}
