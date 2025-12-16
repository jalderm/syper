import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateWorkoutExerciseDto, WorkoutExerciseDto } from '../workout-exercises/models';

export interface CreateUpdateWorkoutSectionDto extends AuditedEntityDto<string> {
  title: string;
  colour: string;
  workoutExercises: CreateUpdateWorkoutExerciseDto[];
  workoutId: string;
  sortOrder: number;
}

export interface WorkoutSectionDto extends AuditedEntityDto<string> {
  title: string;
  colour: string;
  workoutExercises: WorkoutExerciseDto[];
  workoutId: string;
  sortOrder: number;
}
