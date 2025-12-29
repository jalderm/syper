import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {
  currentDate: Date = new Date();
  daysInMonth: Date[] = [];
  events: { [key: string]: string[] } = {};

  constructor() {}

  ngOnInit(): void {
    this.generateCalendar();
  }

  generateCalendar(): void {
    const startOfMonth = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth(), 1);
    const endOfMonth = new Date(this.currentDate.getFullYear(), this.currentDate.getMonth() + 1, 0);

    this.daysInMonth = [];
    for (let day = startOfMonth; day <= endOfMonth; day.setDate(day.getDate() + 1)) {
      this.daysInMonth.push(new Date(day));
    }
  }

  addEvent(date: Date, event: string): void {
    const dateString = date.toISOString().split('T')[0];
    if (!this.events[dateString]) {
      this.events[dateString] = [];
    }
    this.events[dateString].push(event);
  }

  getEventsForDate(date: Date): string[] {
    return this.events[date.toISOString().split('T')[0]] || [];
  }

  // Helper methods to navigate the calendar
  goToPreviousMonth(): void {
    this.currentDate.setMonth(this.currentDate.getMonth() - 1);
    this.generateCalendar();
  }

  goToNextMonth(): void {
    this.currentDate.setMonth(this.currentDate.getMonth() + 1);
    this.generateCalendar();
  }
}
