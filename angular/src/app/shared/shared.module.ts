import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { StatsCardComponent } from '../stats/stats-card.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    CommonModule
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    CommonModule
  ],
  providers: []
})
export class SharedModule {}
