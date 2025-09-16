import { mapEnumToOptions } from '@abp/ng.core';

export enum SetUnitType {
  Weight = 0,
  Distance = 1,
  Time = 2,
}

export const setUnitTypeOptions = mapEnumToOptions(SetUnitType);
