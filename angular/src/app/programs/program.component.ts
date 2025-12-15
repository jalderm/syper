import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { LoadingService } from '../services/loading.service';
import { scheduled } from 'rxjs';
import { ProgramDto, ProgramService } from '../proxy/programs';
import { ScheduleDayDto } from '../proxy/schedule-days';
import { WorkoutDto, WorkoutService } from '../proxy/workouts';
import { ScheduleActivityDto } from '../proxy/schedule-activities';

class ScheduleBuilderDay implements ScheduleDayDto {
  dayOffSet: number;
  activities: ScheduleActivityDto[];
  notes?: string;
  programId: string;
  lastModificationTime?: string | Date;
  lastModifierId?: string;
  creationTime?: string | Date;
  creatorId?: string;
  id?: string;
  isRestDay: boolean;

  constructor(dayOffSet: number = 0) {
    this.dayOffSet = dayOffSet;
    this.activities = [];
    this.isRestDay = false;
  }
}

@Component({
  standalone: false,
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.scss'],
  providers: [ListService],
})
export class ProgramComponent implements OnInit {
  programs = { items: [], totalCount: 0 } as PagedResultDto<ProgramDto>;
  availableWorkouts = { items: [], totalCount: 0 } as PagedResultDto<WorkoutDto>;
  selectedProgram = {} as ProgramDto; // declare selectedProgram
  scheduleBuilderDays: ScheduleBuilderDay[] = [];
  form: FormGroup;
  isModalOpen = false;
  isOngoing = false;

  constructor(
    public readonly list: ListService,
    private programService: ProgramService,
    private workoutService: WorkoutService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private loading: LoadingService
  ) {}

  ngOnInit() {
    const programStreamCreator = (query) => this.programService.getList(query);
    const workoutStreamCreator = (query) => this.workoutService.getList(query);

    this.list.hookToQuery(workoutStreamCreator).subscribe((response) => {
      this.availableWorkouts = response;
    });

    this.list.hookToQuery(programStreamCreator).subscribe((response) => {
      this.programs = response;
    });
  }

  createProgram() {
    this.selectedProgram = {} as ProgramDto; // reset the selected program
    this.scheduleBuilderDays = [];
    this.scheduleBuilderDays.push(new ScheduleBuilderDay());
    this.scheduleBuilderDays.push(new ScheduleBuilderDay(1));

    this.buildForm();
    this.isModalOpen = true;
  }

  getDayName(dayOffSet: number): string {
    const daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
    return daysOfWeek[dayOffSet % 7]; // This handles the circular nature of the week
  }


  onOngoingChange(checked: boolean) {
    this.isOngoing = checked;
    const duration = this.form.get('duration');
    if (checked) {
      duration?.disable();
    } else {
      duration?.enable();
    }
  }

  editProgram(id: string) {
    this.loading.setLoading(true);
    this.programService.get(id).subscribe((program) => {
      this.selectedProgram = program;

      this.selectedProgram.programScheduleDays.forEach(day => {
        this.scheduleBuilderDays.push({
          ...day,
          isRestDay: day.activities.length === 0
        });
      });
      
      this.buildForm();
      this.isModalOpen = true;
      this.loading.setLoading(false);
    },
    (error) => {
      this.loading.setLoading(false);
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.programService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    this.isOngoing = false;
    this.form = this.fb.group({
      name: [this.selectedProgram.name || '', Validators.required],
      duration: [this.selectedProgram.duration || '', Validators.required],
      shortDescription: [this.selectedProgram.shortDescription || '', Validators.required],
      goal: [this.selectedProgram.goal || '', Validators.required],
      weeklySchedule: this.fb.array(
        this.selectedProgram?.programScheduleDays?.map(scheduleDay => this.buildScheduleDay(scheduleDay)) || []
      ),
    });
  }

  buildScheduleDay(scheduleDay: ScheduleDayDto) {
    return this.fb.group({
      dayOffSet: [scheduleDay?.dayOffSet || 0, Validators.required],
      activities: [scheduleDay?.activities || null, Validators.required],
      notes: [scheduleDay?.notes || null]
    });
  }

  buildActivity(activity) {
    return this.fb.group({
      type: [activity?.type || 
        // ActivityType.Rest, 
        0,
        Validators.required],
      workoutId: [activity?.workoutId || null]
    });
  }

  // change the save method
  save() {
    if (this.form.invalid) {
      return;
    }

    const request = this.selectedProgram.id
      ? this.programService.update(this.selectedProgram.id, this.form.value)
      : this.programService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }
}
