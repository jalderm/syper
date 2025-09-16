import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';

import { WorkoutRoutingModule } from './workout-routing.module';
import { WorkoutComponent } from './workout.component';


@NgModule({
  declarations: [
    WorkoutComponent
  ],
  imports: [SharedModule, 
    CommonModule,
    WorkoutRoutingModule
  ]
})
export class WorkoutModule { }
