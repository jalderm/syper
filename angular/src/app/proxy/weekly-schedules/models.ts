import type { AuditedEntityDto } from '@abp/ng.core';
import type { ScheduleDayDto } from '../schedule-days/models';

export interface CreateUpdateWeeklyScheduleDto extends AuditedEntityDto<string> {
  notes?: string;
  programId: string;
}

export interface WeeklyScheduleDto extends AuditedEntityDto<string> {
  scheduleDays: ScheduleDayDto[];
  notes?: string;
  programId: string;
}
