import type { AuditedEntityDto } from '@abp/ng.core';
import type { CreateUpdateWorkoutSectionDto, WorkoutSectionDto } from '../workout-sections/models';

export interface CreateUpdateWorkoutDto extends AuditedEntityDto<string> {
  name: string;
  workoutSections: CreateUpdateWorkoutSectionDto[];
  shortDescription?: string;
}

export interface WorkoutDto extends AuditedEntityDto<string> {
  name: string;
  workoutSections: WorkoutSectionDto[];
  shortDescription?: string;
}
