import type { AuditedEntityDto } from '@abp/ng.core';
import type { ScheduleActivityDto } from '../schedule-activities/models';

export interface ScheduleDayDto extends AuditedEntityDto<string> {
  dayOfWeek: any;
  activities: ScheduleActivityDto[];
  notes?: string;
  weeklyScheduleId: string;
}
