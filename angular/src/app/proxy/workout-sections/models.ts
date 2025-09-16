import type { AuditedEntityDto } from '@abp/ng.core';
import type { WorkoutExerciseDto } from '../workout-exercises/models';

export interface WorkoutSectionDto extends AuditedEntityDto<string> {
  title: string;
  colour: string;
  workoutExercises: WorkoutExerciseDto[];
  workoutId: string;
}
