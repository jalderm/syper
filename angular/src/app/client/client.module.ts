import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ClientRoutingModule } from './client-routing.module';
import { ClientComponent } from './client.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap'; // add this line

@NgModule({
  declarations: [ClientComponent],
  imports: [
    ClientRoutingModule,
    SharedModule,
    NgbDatepickerModule,
  ]
})
export class ClientModule { }
