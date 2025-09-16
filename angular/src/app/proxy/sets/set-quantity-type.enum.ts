import { mapEnumToOptions } from '@abp/ng.core';

export enum SetQuantityType {
  Reps = 0,
  Time = 1,
  Distance = 2,
}

export const setQuantityTypeOptions = mapEnumToOptions(SetQuantityType);
