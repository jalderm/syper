import { mapEnumToOptions } from '@abp/ng.core';

export enum ExerciseCategoryEnum {
  Distance = 0,
  Weight = 1,
  Bodyweight = 2,
  Time = 3,
}

export const exerciseCategoryEnumOptions = mapEnumToOptions(ExerciseCategoryEnum);
