import { CoreModule } from '@abp/ng.core';
import { NgbAccordionModule, NgbDatepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { StatsCardComponent } from '../stats/stats-card.component';
import { CommonModule } from '@angular/common';
import { CdkDrag, CdkDropList, DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    CommonModule,
    NgbAccordionModule,
    NgbDatepickerModule,
    DragDropModule,
    CdkDropList,
    CdkDrag
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    CommonModule,
    NgbAccordionModule,
    NgbDatepickerModule,
    DragDropModule,
    CdkDropList,
    CdkDrag
  ],
  providers: []
})
export class SharedModule {}
