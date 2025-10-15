import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateWeeklyScheduleDto, WeeklyScheduleDto } from '../weekly-schedules/models';

export interface CreateUpdateProgramDto extends AuditedEntityDto<string> {
  name: string;
  duration: number;
  goal?: string;
  shortDescription?: string;
  weeks: CreateUpdateWeeklyScheduleDto[];
}

export interface ProgramDto extends AuditedEntityDto<string> {
  name: string;
  duration: number;
  goal?: string;
  shortDescription?: string;
  weeks: WeeklyScheduleDto[];
}
