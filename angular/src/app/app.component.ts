import { Component } from '@angular/core';
import { LoadingService } from './services/loading.service';


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

  constructor(private loadingService: LoadingService) {
    this.loadingService.loading$.subscribe(val => this.isLoading = val);
  }
}
