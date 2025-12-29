import { ListService, PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { LoadingService } from '../services/loading.service';
import { scheduled } from 'rxjs';
import { ProgramDto, ProgramService } from '../proxy/programs';
import { ScheduleDayDto } from '../proxy/schedule-days';
import { WorkoutDto, WorkoutService } from '../proxy/workouts';
import { ScheduleActivityDto } from '../proxy/schedule-activities';
import { ActivityType } from '../proxy/activity-type-enum';
import { SyperConsts } from '../shared/consts';

class ScheduleBuilderActivityDto implements ScheduleActivityDto {
  id: string;
  scheduleDayId: string;
  type: ActivityType;
  workoutId: string;
  notes?: string;
  saveId?: string; // to track original activity id for edits

  constructor(init?: ScheduleBuilderActivityDto) {
    Object.assign(this, init);
  }

  workout: WorkoutDto;
  lastModificationTime?: string | Date;
  lastModifierId?: string;
  creationTime?: string | Date;
  creatorId?: string;
}

class ScheduleBuilderDay implements ScheduleDayDto {
  dayOffSet: number;
  activities: ScheduleBuilderActivityDto[];
  notes?: string;
  programId: string;
  lastModificationTime?: string | Date;
  lastModifierId?: string;
  creationTime?: string | Date;
  creatorId?: string;
  id?: string;
  isRestDay: boolean;
  isSelected: boolean;

  constructor(dayOffSet: number = 0, programId: string = SyperConsts.blankGuid) {
    this.dayOffSet = dayOffSet;
    this.activities = [];
    this.isRestDay = false;
    this.programId = programId;
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
    private loading: LoadingService,
    private changeDetector: ChangeDetectorRef
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

  // Required to make mat-select show selected saved items
  compareById(c1, c2): boolean {
    return c1.id === c2.id;
  }

  createProgram() {
    this.resetForm();
    
    this.selectedProgram = {} as ProgramDto; // reset the selected program
    this.scheduleBuilderDays = [];
    for (var i=0; i<7; i++) {
      this.addDay();
    }

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
  addDay() {
    const i = this.scheduleBuilderDays.length;
    this.scheduleBuilderDays.push(new ScheduleBuilderDay(i, this.selectedProgram.id || SyperConsts.blankGuid));
  }

  copyDays() {
    if (this.scheduleBuilderDays.length === 0) {
      return;
    }
    const daylength = this.scheduleBuilderDays.length;
    for (var i=0; i<daylength; i++) {
      if (this.scheduleBuilderDays[i].isSelected) {
        const dayToCopy = JSON.parse(JSON.stringify(this.scheduleBuilderDays[i]));
        this.scheduleBuilderDays.push(dayToCopy);
        this.scheduleBuilderDays[this.scheduleBuilderDays.length - 1].dayOffSet = this.scheduleBuilderDays.length - 1;
        this.scheduleBuilderDays[this.scheduleBuilderDays.length - 1].isSelected = false;
        this.scheduleBuilderDays[this.scheduleBuilderDays.length - 1].id = SyperConsts.blankGuid;
        this.scheduleBuilderDays[this.scheduleBuilderDays.length - 1].activities.forEach(activity => {
          activity.scheduleDayId = SyperConsts.blankGuid;
          activity.saveId = SyperConsts.blankGuid;
        });
      }
    }
  }

  removeDay(dayIndex: number) {
    this.scheduleBuilderDays.splice(dayIndex, 1);

    for (var i=0; i<this.scheduleBuilderDays.length; i++) {
      this.scheduleBuilderDays[i].dayOffSet = i;
    }
  }

  editProgram(id: string) {
    this.resetForm();

    this.loading.setLoading(true);
    this.programService.get(id).subscribe((program) => {
      this.selectedProgram = program;

      this.selectedProgram?.scheduleDays?.forEach(day => {
        this.scheduleBuilderDays.push({
          ...day,
          isRestDay: day.activities.length === 0,
          isSelected: false,
          activities: day.activities.map(activity => {
            return {
              ...activity.workout,
              saveId: activity.id, // Store the original activity id
              id: activity.workoutId, // Use workoutId as the temporary id for the builder
              scheduleDayId: activity.scheduleDayId,
              type: activity.type,
              workout: activity.workout,
              workoutId: activity.workoutId
            } as ScheduleBuilderActivityDto;
          })
        });
      });

      this.scheduleBuilderDays = this.scheduleBuilderDays.sort((a, b) => a.dayOffSet - b.dayOffSet);

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
    var ind = 0;
    this.isOngoing = false;
    this.form = this.fb.group({
      id: [this.selectedProgram.id || SyperConsts.blankGuid],
      name: [this.selectedProgram.name || '', Validators.required],
      duration: [this.selectedProgram.duration || '', Validators.required],
      shortDescription: [this.selectedProgram.shortDescription || '', Validators.required],
      goal: [this.selectedProgram.goal || '', Validators.required],
      scheduleDays: this.fb.array(
        this.scheduleBuilderDays?.map(scheduleDay => this.buildScheduleDay(scheduleDay)) || []
      ),
    });
  }

  buildScheduleDay(scheduleDay: ScheduleDayDto) {
    console.log(scheduleDay.activities);
    return this.fb.group({
      dayOffSet: [scheduleDay?.dayOffSet || 0, Validators.required],
      activities: [scheduleDay?.activities || null],
      notes: [scheduleDay?.notes || null],
      id: [scheduleDay?.id || SyperConsts.blankGuid]
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

    var offset = 0;
    this.scheduleBuilderDays.forEach(day => {
      day.dayOffSet = offset;
      offset++;
    });

    this.form.value.scheduleDays = this.scheduleBuilderDays.map(day => ({
      dayOffSet: day.dayOffSet,
      activities: day.isRestDay ? [] : day.activities.map(activity => ({
        ...activity,
        workoutId: activity.id,
        type: ActivityType.Workout,
        scheduleDayId: day.id || SyperConsts.blankGuid,
        id: activity.saveId || SyperConsts.blankGuid
      })),
      notes: day.notes,
      programId: this.selectedProgram.id || SyperConsts.blankGuid,
      id: day.id || SyperConsts.blankGuid
    }));

    const request = this.selectedProgram.id
      ? this.programService.update(this.selectedProgram.id, this.form.value)
      : this.programService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  resetForm() {
    this.form?.reset();
    this.selectedProgram = {} as ProgramDto
    this.scheduleBuilderDays = [];
    this.isOngoing = false;
  }
}
