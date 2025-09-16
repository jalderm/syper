import type { WorkoutExerciseDto } from '../workout-exercises/models';
import type { AuditedEntityDto } from '@abp/ng.core';
import type { WorkoutSectionDto } from '../workout-sections/models';

export interface CreateUpdateWorkoutDto {
  title: string;
  authorId?: string;
  workoutExercises: WorkoutExerciseDto[];
}

export interface WorkoutDto extends AuditedEntityDto<string> {
  name: string;
  workoutSections: WorkoutSectionDto[];
  shortDescription?: string;
}
