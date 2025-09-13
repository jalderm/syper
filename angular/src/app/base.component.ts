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
}
