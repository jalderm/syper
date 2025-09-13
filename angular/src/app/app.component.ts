import { Component } from '@angular/core';
import { LoadingService } from './services/loading.service';
import { ReplaceableComponentsService } from '@abp/ng.core';
import { eThemeLeptonXComponents } from '@abp/ng.theme.lepton-x';


@Component({ template: '' })
export class EmptyComponent {}

@Component({
  standalone: false,
  selector: 'app-root',
  template: `
  <div [abpLoading]="isLoading">
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
  </div>
  `,
})
export class AppComponent {
  isLoading = true;

  constructor(private loadingService: LoadingService,
     private replaceableComponents: ReplaceableComponentsService,
  ) {
    // Add a global loading indicator
    this.loadingService.loading$.subscribe(val => this.isLoading = val);

    // Remove the langauge switcher from the header
    this.replaceableComponents.add({
      component: EmptyComponent,
      key: eThemeLeptonXComponents.Languages,
    });
  }
}
