import type { AuditedEntityDto } from '@abp/ng.core';
import type { SetDto } from '../sets/models';

export interface WorkoutExerciseDto extends AuditedEntityDto<string> {
  workoutSectionId: string;
  sets: SetDto[];
}
