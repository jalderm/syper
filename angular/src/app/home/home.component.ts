import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { BaseComponent } from '../base.component';
import { StatsCardComponent } from '../stats/stats-card.component';

// 👇 Import this from ABP
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { LocalizationModule } from '@abp/ng.core';

@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [StatsCardComponent, ThemeSharedModule, LocalizationModule]
})
export class HomeComponent extends BaseComponent {
  activeRunnersCount = 23;
  totalRuns = 81;
  totalKms = 812;
  nextRace = {
    name: 'City-Bay Fun Run',
    date: new Date(),
    dateString: ''
  };

  constructor(protected authService: AuthService) {
    super(authService);
    this.nextRace.date.setDate(this.nextRace.date.getDate() + 8);
    this.nextRace.dateString = this.nextRace.date.toLocaleDateString('en-AU', { year: 'numeric', month: 'short', day: 'numeric' });
  }
}
