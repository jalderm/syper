import { ListService, PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef, Component, forwardRef, Input, NgModule, OnInit, Output, QueryList, ViewChildren } from '@angular/core';
import { CreateUpdateWorkoutDto, WorkoutDto, WorkoutService } from '../proxy/workouts';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { AbstractControl, ControlValueAccessor, FormArray, FormBuilder, FormGroup, NG_VALUE_ACCESSOR, ValidationErrors, Validators } from '@angular/forms';
import { LoadingService } from '../services/loading.service';
import { WorkoutSectionDto } from '../proxy/workout-sections';
import {
  CdkDrag,
  CdkDragDrop,
  CdkDropList,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { NgbAccordionItem } from '@ng-bootstrap/ng-bootstrap';
import { SetQuantityType, SetUnitType } from '../proxy/sets';
import { ExerciseDto, ExerciseService } from '../proxy/exercises';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { EventEmitter } from '@angular/core';
import { WorkoutExerciseDto } from '../proxy/workout-exercises';
import { WorkoutExercises } from '../proxy';
import { SyperConsts } from '../shared/consts';


const origCollapse = NgbAccordionItem.prototype['collapse'];
const origExpand = NgbAccordionItem.prototype['expand'];

NgbAccordionItem.prototype['collapse'] = function () {
//   console.log('Collapse prevented!');
  // comment this out if you want to block
  // return origCollapse.apply(this, arguments);
};
NgbAccordionItem.prototype['expand'] = function () {
  // console.log('Expand prevented!');
  // comment this out if you want to block
  // return origCollapse.apply(this, arguments);
};

@Component({
  selector: 'app-exercise-dropdown',
  template: `
    <ngx-select-dropdown
    [options]="options"
  [config]="config"
  [ngModel]="value"
  (ngModelChange)="onChange($event)"
  
      [multiple]="false">
    </ngx-select-dropdown>
  `,
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ExerciseDropdownComponent),
    multi: true
  }],
  standalone: false
  
})
export class ExerciseDropdownComponent implements ControlValueAccessor {
  @Input() options: any[] = [];
  @Input() config: any = {};
  @Output() selectionChange = new EventEmitter<any>();

  private onChangeFn: any = () => {};
  private onTouchedFn: any = () => {};
  value: any;

  onChange(event: any) {
    this.value = event;

    this.onChangeFn(event);
    this.selectionChange.emit(event);
  }

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChangeFn = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouchedFn = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
  }
}



@Component({
  standalone: false,
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.scss'],
  providers: [ListService],
})
export class WorkoutComponent implements OnInit {
  public SetQuantityType = SetQuantityType;

  workouts = { items: [], totalCount: 0 } as PagedResultDto<WorkoutDto>;
  exercises = { items: [], totalCount: 0 } as PagedResultDto<ExerciseDto>;

  config = {
    displayKey: "title",       // key in ExerciseDto to show in dropdown
    search: false,
    height: '200px',
    placeholder: 'Select exercises',
    customComparator: undefined,
    limitTo: 20,   // show page size worth of items
    moreText: 'more',
    noResultsFound: 'No exercises found!',
    clearOnSelection: false,
    searchOnKey: 'title'
  };
  dropdownOptions: ExerciseDto[] = [];
  dataModel: ExerciseDto;
  page = 1;
  pageSize = 20;
  totalCount = 0;


  selectedWorkout = {} as CreateUpdateWorkoutDto | WorkoutDto; // declare selectedWorkout

  form: FormGroup;
  
  isModalOpen = false;

  constructor(public readonly list: ListService,
    private workoutService: WorkoutService,
    private exerciseService: ExerciseService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private loading: LoadingService) {}

  ngOnInit() {
    const workoutStreamCreator = (query) => this.workoutService.getList(query);
    const exerciseStreamCreator = (query) => this.exerciseService.getList(query);

    this.list.hookToQuery(workoutStreamCreator).subscribe((response) => {
      this.workouts = response;
    });

    this.list.hookToQuery(exerciseStreamCreator).subscribe((response) => {
      this.exercises = response;
      this.dropdownOptions = response.items;
      this.totalCount = response.totalCount;
    });
  }

  getExerciseCount(row) {
    console.log(row);
  }

  buildForm() {
    this.form = this.fb.group({
      id: [this.selectedWorkout.id || SyperConsts.blankGuid],
      name: [this.selectedWorkout.name || '', Validators.required],
      workoutSections: this.fb.array(
        this.selectedWorkout?.workoutSections?.map(section => this.buildSection(section)) || []
      ), 
    });
  }

  buildSection(section: WorkoutSectionDto): FormGroup {
    return this.fb.group({
      id: [section.id || SyperConsts.blankGuid],
      title: [section.title || '', Validators.required],
      colour: [section.colour || '#cccccc'],
      workoutExercises: this.fb.array(section.workoutExercises?.map(exercise => this.buildExercise(exercise)) || []),
      workoutId: [section.workoutId || SyperConsts.blankGuid]
    });
  }

  buildExercise(exercise: WorkoutExerciseDto): FormGroup {
    return this.fb.group({
        id: [exercise?.id || SyperConsts.blankGuid],
        name: [exercise?.exercise?.title || 'Select an Exercise', Validators.required],
        exerciseId: [exercise?.exerciseId || '', Validators.required],
        exerciseDto: [exercise?.exercise || null, Validators.required],
        sets: this.fb.array(exercise?.sets?.map(set => this.buildSet(set)) || []),
        workoutSectionId: exercise?.workoutSectionId || SyperConsts.blankGuid
    });
  }

  buildSet(set): FormGroup {
    return this.fb.group({
      id: [set?.id || SyperConsts.blankGuid],
      unit: [set?.unit || null, Validators.required],
      unitType: [set?.unitType || SetUnitType.Distance, Validators.required],
      quantity: [set?.quantity || '', Validators.required],
      quantityType: [set?.quantityType || SetQuantityType.Time, Validators.required],
      rest: [set?.rest || null]
    });
  }

  getUnitTitle(unitType): string {
    switch (unitType) {
      case SetUnitType.Distance:
        return "Km(s)";
      case SetUnitType.Weight:
        return "Kg(s)";
      case SetUnitType.Time:
        return "Time";
      default:
        return "?";
    }
  }

  getQuantityTitle(unitType): string {
    switch (unitType) {
      case SetQuantityType.Distance:
        return "Km(s)";
      case SetQuantityType.Reps:
        return "Reps";
      case SetQuantityType.Time:
        return "Time";
      default:
        return "?";
    }
  }

  workoutSectionsConstraint() {
    const min = 1;
    return (c: AbstractControl): ValidationErrors | null => {
      const formArray = c as FormArray;
      return formArray.controls.length >= min ? null : { minLengthArray: true };
    };
  }

  createWorkout() {
    this.selectedWorkout = {} as CreateUpdateWorkoutDto | WorkoutDto; // reset the selected Workout
    this.buildForm();
    this.isModalOpen = true;
  }

  editWorkout(id: string) {
    this.loading.setLoading(true);
    this.workoutService.get(id).subscribe((workout: CreateUpdateWorkoutDto) => {
      this.selectedWorkout = workout;
      this.selectedWorkout.id = id;
      this.normaliseSets(this.selectedWorkout, true);
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
        this.workoutService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  selectionExerciseChanged(event: any, sectionIndex: number, exerciseIndex: number) {
    if (event == null || event.length === 0) {
      return;
    }
    const control = this.form.get('workoutSections').get(sectionIndex.toString()).get('workoutExercises').get(exerciseIndex.toString());
    control.setValue({
        name: event.title,
        exerciseId: event.id,
        exerciseDto: event,
        sets: this.fb.array([]),
        workoutSectionId: this.form.get('workoutSections').get('id')?.value ?? SyperConsts.blankGuid
    });
  }


  onSpace(event: KeyboardEvent) {
    event.stopPropagation();      // prevent accordion toggle
    event.preventDefault();       // prevent scrolling
    const input = event.target as HTMLInputElement;

    // Example: insert a space at the cursor position
    const start = input.selectionStart || 0;
    const end = input.selectionEnd || 0;
    input.value = input.value.slice(0, start) + ' ' + input.value.slice(end);
    input.setSelectionRange(start + 1, start + 1);
  }


  // Workout Builder Itself
  workoutSections(): FormArray {
    return this.form.get('workoutSections') as FormArray;
  }

  addSection() {
    this.workoutSections().push(
      this.fb.group({
        title: ['', Validators.required],
        colour: ['#cccccc'],
        workoutExercises: this.fb.array([]),
        workoutId: SyperConsts.blankGuid
      })
    );
  }

  removeSection(i: number) {
    this.workoutSections().removeAt(i);
  }


  getExercises(sectionIndex: number): FormArray {
    return this.workoutSections().at(sectionIndex).get('workoutExercises') as FormArray;
  }

  addExercise(sectionIndex: number) {
    console.log(this.getExercises(sectionIndex));
    this.getExercises(sectionIndex).push(
    this.fb.group({
        name: ['Select an Exercise', Validators.required],
        exerciseId: ['', Validators.required],
        exerciseDto: [null, Validators.required],
        sets: this.fb.array([]),
        workoutSectionId: this.workoutSections().at(sectionIndex).get('id') ?? SyperConsts.blankGuid
    })
  );
  }

  removeExercise(sectionIndex: number, exerciseIndex: number) {
    this.getExercises(sectionIndex).removeAt(exerciseIndex);
  }

  getSets(sectionIndex: number, exerciseIndex: number): FormArray {
    return this.getExercises(sectionIndex).at(exerciseIndex).get('sets') as FormArray;
  }

  addSet(sectionIndex: number, exerciseIndex: number) {
    this.getSets(sectionIndex, exerciseIndex).push(
      this.fb.group({
        unit: [null, Validators.required],
        unitType: [SetUnitType.Distance, Validators.required],
        quantity: ['', Validators.required],
        quantityType: [SetQuantityType.Time, Validators.required],
        rest: [null],
      })
    );
  }

  removeSet(sectionIndex: number, exerciseIndex: number, setIndex: number) {
    this.getSets(sectionIndex, exerciseIndex).removeAt(setIndex);
  }

  normaliseSets(w, denormalise: boolean = false) {
    w.workoutSections.forEach(ws => {
      ws.workoutExercises.forEach(we => {
        we.sets.forEach(s => {
          if (denormalise) {
            switch (s.quantityType) {
              case SetQuantityType.Time:
              case SetQuantityType.Distance:
                if (typeof s.quantity === 'number') {
                  var quantityTimes: string[] = [];
                  quantityTimes[0] = Math.floor(s.quantity/3600).toString().padStart(2,'0');
                  var mins = Math.floor((s.quantity%3600)/60);
                  quantityTimes[1] = mins.toString().padStart(2,'0');
                  quantityTimes[2] = Math.floor(s.quantity%60).toString().padStart(2,'0');
                  s.quantity = quantityTimes.join(':');
                }
              case SetQuantityType.Reps:
              default:
                s.quantity = `${s.quantity}`;
            }
            s.unit = `${s.unit}`;
          }
          else {
            switch (s.quantityType) {
              case SetQuantityType.Time:
              case SetQuantityType.Distance:
                if (typeof s.quantity === 'string') {
                  var quantityTimes: string[] = (s.quantity as string).split(':');
                  var totalTime = +quantityTimes[0];
                  totalTime *= 60;
                  totalTime += +quantityTimes[1];
                  totalTime *= 60;
                  totalTime += +quantityTimes[2];
                  s.quantity = totalTime;
                }
              case SetQuantityType.Reps:
              default:
                s.quantity = +s.quantity;
            }
            s.unit = +s.unit;
          }
          
        })
      })
    })
  }

  save() {
    console.log("starting save");
    if (this.form.invalid) {
      return;
    }

    console.log(this.form.value);

    var w = this.form.value;

    console.log(w);
    console.log(this.selectedWorkout);
    
    this.normaliseSets(w);

    if (this.selectedWorkout.id) {
      w.id = this.selectedWorkout.id;
    }

    const request = this.selectedWorkout.id
      ? this.workoutService.update(this.selectedWorkout.id, w)
      : this.workoutService.create(w);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }


  sectionItems: WorkoutSectionDto[] = [];

  // Store drop list references
  @ViewChildren(CdkDropList) dropLists!: QueryList<CdkDropList>;
  // Connect current list to all other lists
  connectedDropLists(index: number): CdkDropList[] {
    return this.dropLists
      .filter((_, i) => i !== index);
  }

  drop(event: CdkDragDrop<AbstractControl[]>, sectionIndex: number) {
  if (event.previousContainer === event.container) {
    moveItemInArray(
      this.getExercises(sectionIndex).controls,
      event.previousIndex,
      event.currentIndex
    );
  } else {
    transferArrayItem(
      event.previousContainer.data,
      event.container.data,
      event.previousIndex,
      event.currentIndex
    );
  }
}
}
