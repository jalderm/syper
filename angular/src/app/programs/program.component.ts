import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { LoadingService } from '../services/loading.service';
import { scheduled } from 'rxjs';
import { ProgramDto, ProgramService } from '../proxy/programs';
import { WeeklyScheduleDto } from '../proxy/weekly-schedules';

@Component({
  standalone: false,
  selector: 'app-program',
  templateUrl: './program.component.html',
  styleUrls: ['./program.component.scss'],
  providers: [ListService],
})
export class ProgramComponent implements OnInit {
  programs = { items: [], totalCount: 0 } as PagedResultDto<ProgramDto>;
  selectedProgram = {} as ProgramDto; // declare selectedProgram
  form: FormGroup;
  isModalOpen = false;
  isOngoing = false;

  constructor(
    public readonly list: ListService,
    private programService: ProgramService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private loading: LoadingService
  ) {}

  ngOnInit() {
    const programStreamCreator = (query) => this.programService.getList(query);

    this.list.hookToQuery(programStreamCreator).subscribe((response) => {
      this.programs = response;
    });
  }

  createProgram() {
    this.selectedProgram = {} as ProgramDto; // reset the selected program
    this.buildForm();
    this.isModalOpen = true;
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
        this.selectedProgram?.weeks?.map(scheduleWeek => this.buildScheduleWeek(scheduleWeek)) || []
      ),
    });
  }

  buildScheduleWeek(scheduleWeek: WeeklyScheduleDto) {
    return this.fb.group({
      scheduleDays: this.fb.array(
        scheduleWeek?.scheduleDays?.map(scheduleDay => this.buildScheduleDay(scheduleDay)) || []
      ),
      notes: [scheduleWeek?.notes || '']
    });
  }

  buildScheduleDay(scheduleDay) {
    return this.fb.group({
      dayOfWeek: [scheduleDay?.dayOfWeek || 0, Validators.required],
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
