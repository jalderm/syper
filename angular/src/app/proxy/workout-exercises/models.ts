import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateSetDto, SetDto } from '../sets/models';
import type { ExerciseDto } from '../exercises/models';

export interface CreateUpdateWorkoutExerciseDto extends AuditedEntityDto<string> {
  exerciseId: string;
  workoutSectionId: string;
  sets: CreateUpdateSetDto[];
  sortOrder: number;
}

export interface WorkoutExerciseDto extends AuditedEntityDto<string> {
  exerciseId: string;
  exercise: ExerciseDto;
  workoutSectionId: string;
  sets: SetDto[];
  sortOrder: number;
}
