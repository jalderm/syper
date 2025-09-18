import type { ExerciseCategoryEnum } from '../exercise-categories/exercise-category-enum.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateExerciseDto {
  title: string;
  exerciseCategory?: ExerciseCategoryEnum;
}

export interface ExerciseDto extends AuditedEntityDto<string> {
  title: string;
  exerciseCategory: ExerciseCategoryEnum;
}
