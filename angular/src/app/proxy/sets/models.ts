import type { AuditedEntityDto } from '@abp/ng.core';
import type { SetUnitType } from './set-unit-type.enum';
import type { SetQuantityType } from './set-quantity-type.enum';

export interface CreateUpdateSetDto extends AuditedEntityDto<string> {
  unit: number;
  unitType?: SetUnitType;
  quantity: number;
  quantityType?: SetQuantityType;
  rest?: string;
  workoutExerciseId: string;
}

export interface SetDto extends AuditedEntityDto<string> {
  unit: number;
  unitType?: SetUnitType;
  quantity: number;
  quantityType?: SetQuantityType;
  rest?: string;
  workoutExerciseId: string;
}
