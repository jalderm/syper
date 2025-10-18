import { AuthService } from "@abp/ng.core";
import { Component, OnInit } from "@angular/core";

@Component({ template: '' })
export class BaseComponent implements OnInit{
    
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated
  }

  constructor(protected authService: AuthService) {}
    ngOnInit(): void {
        this.checkLoginRequired()
    }

  checkLoginRequired() {
    if (!this.hasLoggedIn) {
      this.authService.navigateToLogin();
    }
  }

  consts = {
    primaryColour: getComputedStyle(document.documentElement)
      .getPropertyValue('--syper-primary-color')
      .trim(),
    successColour: getComputedStyle(document.documentElement)
      .getPropertyValue('--syper-success-color')
      .trim(),
    warningColour: getComputedStyle(document.documentElement)
      .getPropertyValue('--syper-warning-color')
      .trim(),
  }
}
