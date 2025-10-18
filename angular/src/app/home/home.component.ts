import { AuthService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { BaseComponent } from '../base.component';
import { StatsCardComponent } from '../stats/stats-card.component';

// 👇 Import this from ABP
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { LocalizationModule } from '@abp/ng.core';

import { SharedModule } from '../shared/shared.module';


interface Runner {
  id: number;
  name: string;
  totalDistance: number;
  totalRuns: number;
  avgPace: string;
  weeklyGoal: number;
  currentWeek: number;
}

interface Run {
  date: string;
  distance: number;
  duration: string;
  pace: string;
  type: string;
  elevation: number;
  month: string;
}

interface MonthData {
  month: string;
  distance: number;
  runs: number;
  avgPace: string;
}



@Component({
  standalone: true,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [StatsCardComponent, ThemeSharedModule, LocalizationModule, SharedModule]
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







  view: 'overview' | 'detail' = 'overview';
  selectedRunner: Runner | null = null;
  selectedMonth: string | null = null;

  runners: Runner[] = [
    { id: 1, name: 'Sarah Johnson', totalDistance: 245.3, totalRuns: 28, avgPace: '5:12', weeklyGoal: 50, currentWeek: 42 },
    { id: 2, name: 'Mike Chen', totalDistance: 189.7, totalRuns: 22, avgPace: '5:45', weeklyGoal: 40, currentWeek: 38 },
    { id: 3, name: 'Emma Davis', totalDistance: 312.8, totalRuns: 35, avgPace: '4:58', weeklyGoal: 60, currentWeek: 55 },
    { id: 4, name: 'James Wilson', totalDistance: 156.4, totalRuns: 18, avgPace: '6:10', weeklyGoal: 35, currentWeek: 32 },
    { id: 5, name: 'Lisa Brown', totalDistance: 203.5, totalRuns: 25, avgPace: '5:30', weeklyGoal: 45, currentWeek: 41 },
  ];

  weeklyData = [
    { week: 'Week 1', distance: 180, runs: 15 },
    { week: 'Week 2', distance: 195, runs: 18 },
    { week: 'Week 3', distance: 210, runs: 16 },
    { week: 'Week 4', distance: 225, runs: 20 },
  ];

  runnerDetailData: any = {
    1: {
      recentRuns: [
        { date: '2025-10-14', distance: 10.5, duration: '54:36', pace: '5:12', type: 'Long Run', elevation: 125, month: 'Oct' },
        { date: '2025-10-12', distance: 5.2, duration: '26:50', pace: '5:10', type: 'Tempo', elevation: 45, month: 'Oct' },
        { date: '2025-10-10', distance: 8.0, duration: '42:00', pace: '5:15', type: 'Easy', elevation: 80, month: 'Oct' },
        { date: '2025-10-08', distance: 6.5, duration: '34:15', pace: '5:16', type: 'Recovery', elevation: 35, month: 'Oct' },
        { date: '2025-10-05', distance: 12.0, duration: '62:24', pace: '5:12', type: 'Long Run', elevation: 145, month: 'Oct' },
        { date: '2025-09-28', distance: 9.5, duration: '49:35', pace: '5:13', type: 'Easy', elevation: 95, month: 'Sep' },
        { date: '2025-09-26', distance: 7.2, duration: '37:26', pace: '5:12', type: 'Tempo', elevation: 65, month: 'Sep' },
        { date: '2025-09-23', distance: 11.0, duration: '57:12', pace: '5:12', type: 'Long Run', elevation: 130, month: 'Sep' },
        { date: '2025-09-21', distance: 5.0, duration: '26:15', pace: '5:15', type: 'Recovery', elevation: 30, month: 'Sep' },
        { date: '2025-09-18', distance: 8.5, duration: '44:20', pace: '5:13', type: 'Easy', elevation: 75, month: 'Sep' },
        { date: '2025-08-30', distance: 10.0, duration: '52:00', pace: '5:12', type: 'Long Run', elevation: 120, month: 'Aug' },
        { date: '2025-08-27', distance: 6.0, duration: '31:12', pace: '5:12', type: 'Tempo', elevation: 50, month: 'Aug' },
        { date: '2025-08-25', distance: 8.0, duration: '41:36', pace: '5:12', type: 'Easy', elevation: 70, month: 'Aug' },
        { date: '2025-08-22', distance: 5.5, duration: '28:36', pace: '5:12', type: 'Intervals', elevation: 40, month: 'Aug' },
        { date: '2025-07-28', distance: 9.0, duration: '47:00', pace: '5:13', type: 'Easy', elevation: 85, month: 'Jul' },
        { date: '2025-07-25', distance: 11.5, duration: '59:54', pace: '5:12', type: 'Long Run', elevation: 140, month: 'Jul' },
        { date: '2025-07-22', distance: 6.5, duration: '33:54', pace: '5:13', type: 'Tempo', elevation: 55, month: 'Jul' },
      ],
      monthlyProgress: [
        { month: 'Jul', distance: 185, runs: 7, avgPace: '5:13' },
        { month: 'Aug', distance: 220, runs: 8, avgPace: '5:12' },
        { month: 'Sep', distance: 235, runs: 9, avgPace: '5:13' },
        { month: 'Oct', distance: 245, runs: 10, avgPace: '5:12' },
      ],
    }
  };

  // Chart options
  monthlyChartOptions: any;
  weeklyChartOptions: any;
  runTypeChartOptions: any;

  ngOnInit() {
    this.initCharts();
  }

  initCharts() {
    this.updateWeeklyChart();
  }

  handleRunnerClick(runner: Runner) {
    this.selectedRunner = runner;
    this.selectedMonth = null;
    this.view = 'detail';
    this.updateMonthlyChart();
    this.updateRunTypeChart();
  }

  handleBackToOverview() {
    this.selectedRunner = null;
    this.selectedMonth = null;
    this.view = 'overview';
  }

  handleMonthClick(event: any) {
    if (event && event.name) {
      this.selectedMonth = event.name;
      this.updateRunTypeChart();
    }
  }

  clearFilter() {
    this.selectedMonth = null;
    this.updateRunTypeChart();
  }

  updateWeeklyChart() {
    this.weeklyChartOptions = {
      legend: { data: ['Distance (km)', 'Total Runs'] },
      xAxis: { type: 'category', data: this.weeklyData.map(d => d.week) },
      yAxis: [
        { type: 'value', name: 'Distance (km)' },
        { type: 'value', name: 'Runs', position: 'right' }
      ],
      series: [
        {
          name: 'Distance (km)',
          type: 'line',
          data: this.weeklyData.map(d => d.distance),
          smooth: true,
          itemStyle: { color: '#3b82f6' }
        },
        {
          name: 'Total Runs',
          type: 'line',
          data: this.weeklyData.map(d => d.runs),
          yAxisIndex: 1,
          smooth: true,
          itemStyle: { color: '#8b5cf6' }
        }
      ]
    };
  }

  updateMonthlyChart() {
    if (!this.selectedRunner) return;
    
    const data = this.runnerDetailData[this.selectedRunner.id].monthlyProgress;
    
    this.monthlyChartOptions = {
      xAxis: { type: 'category', data: data.map((d: MonthData) => d.month) },
      yAxis: { type: 'value', name: 'Distance (km)' },
      series: [{
        data: data.map((d: MonthData) => d.distance),
        type: 'bar',
        itemStyle: { color: '#3b82f6' }
      }],
      tooltip: { trigger: 'axis' }
    };
  }

  updateRunTypeChart() {
    if (!this.selectedRunner) return;

    const details = this.runnerDetailData[this.selectedRunner.id];
    let runs = details.recentRuns;

    if (this.selectedMonth) {
      runs = runs.filter((run: Run) => run.month === this.selectedMonth);
    }

    const runTypeCounts: { [key: string]: number } = {};
    runs.forEach((run: Run) => {
      runTypeCounts[run.type] = (runTypeCounts[run.type] || 0) + 1;
    });

    const runTypeData = Object.entries(runTypeCounts).map(([name, value]) => ({ name, value }));

    this.runTypeChartOptions = {
      tooltip: { trigger: 'item' },
      legend: { orient: 'vertical', left: 'left' },
      series: [{
        type: 'pie',
        radius: '70%',
        data: runTypeData,
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }]
    };
  }

  getFilteredData() {
    if (!this.selectedRunner) return null;

    const details = this.runnerDetailData[this.selectedRunner.id];

    if (this.selectedMonth) {
      const monthData = details.monthlyProgress.find((m: MonthData) => m.month === this.selectedMonth);
      const monthRuns = details.recentRuns.filter((run: Run) => run.month === this.selectedMonth);
      const avgKmPerWeek = (monthData.distance / 4).toFixed(1);

      return {
        distance: monthData.distance,
        runs: monthData.runs,
        avgPace: monthData.avgPace,
        avgKmPerWeek,
        recentRuns: monthRuns
      };
    }

    return {
      distance: this.selectedRunner.totalDistance,
      runs: this.selectedRunner.totalRuns,
      avgPace: this.selectedRunner.avgPace,
      avgKmPerWeek: this.selectedRunner.currentWeek.toFixed(1),
      recentRuns: details.recentRuns.slice(0, 8)
    };
  }

  getProgressPercentage(runner: Runner): number {
    console.log(Math.min((runner.currentWeek / runner.weeklyGoal) * 100, 100));
    return Math.min((runner.currentWeek / runner.weeklyGoal) * 100, 100);
  }

  getProgressColor(runner: Runner): string {
    const progress = (runner.currentWeek / runner.weeklyGoal) * 100;
    if (progress >= 100) return '#10b981';
    if (progress >= 75) return '#3b82f6';
    return '#f59e0b';
  }

  getInitials(name: string): string {
    return name.split(' ').map(n => n[0]).join('');
  }
}
