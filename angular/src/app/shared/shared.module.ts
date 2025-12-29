import { CoreModule } from '@abp/ng.core';
import { NgbAccordionModule, NgbDatepickerModule, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { StatsCardComponent } from '../stats/stats-card.component';
import { CommonModule } from '@angular/common';
import { CdkDrag, CdkDropList, DragDropModule } from '@angular/cdk/drag-drop';


// PrimeNG Modules
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ProgressBarModule } from 'primeng/progressbar';
import { TagModule } from 'primeng/tag';
import { SharedModule as PngShared } from 'primeng/api';

// NgxEcharts for charts
import { NgxEchartsModule } from 'ngx-echarts';
import * as echarts from 'echarts';

import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { CalendarComponent } from '../calendar/calendar.component';



@NgModule({
  declarations: [
    
  ],
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
    CdkDrag,
    CalendarComponent,

    PngShared,

    // Export PrimeNG Modules
    CardModule,
    TableModule,
    ButtonModule,
    ProgressBarModule,
    TagModule,
    NgxEchartsModule.forRoot({ echarts })
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
    CdkDrag,

    PngShared,

    // Export PrimeNG Modules
    CardModule,
    TableModule,
    ButtonModule,
    ProgressBarModule,
    TagModule,

    NgxEchartsModule
  ],
  providers: [
    provideAnimationsAsync(),
    providePrimeNG({
        theme: {
            preset: Aura
        }
    })
  ]
})
export class SharedModule {}
