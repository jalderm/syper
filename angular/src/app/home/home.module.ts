import { NgModule } from '@angular/core';
import { PageModule } from '@abp/ng.components/page';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { provideAbpCore, withOptions } from '@abp/ng.core';
import { environment } from 'src/environments/environment';
import { registerLocale } from '@abp/ng.core/locale';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


@NgModule({
  declarations: [],
  imports: [HomeComponent, SharedModule, HomeRoutingModule, PageModule],
  exports: [SharedModule],
  providers: [
    provideAbpCore(
      withOptions({
        environment,
        registerLocaleFn: registerLocale(),
      }),
    )
  ],
})
export class HomeModule {}
