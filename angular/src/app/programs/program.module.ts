import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ProgramRoutingModule } from './program-routing.module';
import { ProgramComponent } from './program.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';

export class YourFeatureModule {}
@NgModule({
  declarations: [ProgramComponent],
  imports: [
    ProgramRoutingModule,
    SharedModule,
    MatCheckboxModule,
    CommonModule
  ]
})
export class ProgramModule { }