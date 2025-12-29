import type { AuditedEntityDto } from '@abp/ng.core';
import type { ActivityType } from '../activity-type-enum/activity-type.enum';
import type { WorkoutDto } from '../workouts/models';

export interface CreateUpdateScheduleActivityDto extends AuditedEntityDto<string> {
  type: ActivityType;
  workoutId?: string;
  scheduleDayId: string;
}

export interface ScheduleActivityDto extends AuditedEntityDto<string> {
  type: ActivityType;
  workoutId?: string;
  workout: WorkoutDto;
  scheduleDayId: string;
}
