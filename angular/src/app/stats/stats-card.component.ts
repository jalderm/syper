import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stats-card',
  templateUrl: './stats-card.component.html',
  imports: [CommonModule]
})
export class StatsCardComponent {
  @Input() title!: string;
  @Input() value!: number;
  @Input() subtitle!: string;
  @Input() icon!: string; // icon name or path
  @Input() trend?: { value: number, isPositive: boolean };
  @Input() color: 'blue' | 'green' | 'orange' | 'purple' = 'blue';
}
