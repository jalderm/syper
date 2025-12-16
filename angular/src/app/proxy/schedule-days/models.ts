import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateScheduleActivityDto, ScheduleActivityDto } from '../schedule-activities/models';

export interface CreateUpdateScheduleDayDto extends AuditedEntityDto<string> {
  dayOffSet: number;
  activities: CreateUpdateScheduleActivityDto[];
  notes?: string;
  programId: string;
}

export interface ScheduleDayDto extends AuditedEntityDto<string> {
  dayOffSet: number;
  activities: ScheduleActivityDto[];
  notes?: string;
  programId: string;
}
