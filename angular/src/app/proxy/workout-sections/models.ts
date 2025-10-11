import type { CreateUpdateWorkoutExerciseDto, WorkoutExerciseDto } from '../workout-exercises/models';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateWorkoutSectionDto extends AuditedEntityDto<string>{
  title: string;
  colour: string;
  workoutExercises: CreateUpdateWorkoutExerciseDto[];
  workoutId: string;
}

export interface WorkoutSectionDto extends AuditedEntityDto<string> {
  title: string;
  colour: string;
  workoutExercises: WorkoutExerciseDto[];
  workoutId: string;
}
