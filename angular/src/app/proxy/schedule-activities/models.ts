import type { AuditedEntityDto } from '@abp/ng.core';
import type { ActivityType } from '../activity-type-enum/activity-type.enum';

export interface ScheduleActivityDto extends AuditedEntityDto<string> {
  activityType: ActivityType;
  workoutId?: string;
  scheduleDayId: string;
}
