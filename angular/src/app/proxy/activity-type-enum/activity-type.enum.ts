import { mapEnumToOptions } from '@abp/ng.core';

export enum ActivityType {
  Workout = 0,
  Rest = 1,
}

export const activityTypeOptions = mapEnumToOptions(ActivityType);
