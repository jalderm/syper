import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';

import { WorkoutRoutingModule } from './workout-routing.module';
import { ExerciseDropdownComponent, WorkoutComponent } from './workout.component';
import { SelectDropDownModule } from 'ngx-select-dropdown';


@NgModule({
  declarations: [
    WorkoutComponent,
    ExerciseDropdownComponent
  ],
  imports: [SharedModule, 
    CommonModule,
    WorkoutRoutingModule,
    SelectDropDownModule
  ],
  exports: [ExerciseDropdownComponent]
})
export class WorkoutModule { }
