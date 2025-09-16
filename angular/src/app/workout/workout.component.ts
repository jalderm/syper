import { ListService, PagedResultDto } from '@abp/ng.core';
import { ChangeDetectorRef, Component, OnInit, QueryList, ViewChildren } from '@angular/core';
import { WorkoutDto, WorkoutService } from '../proxy/workouts';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  standalone: false,
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.scss'],
  providers: [ListService],
})
export class WorkoutComponent implements OnInit {
  // Expose public version of enum for comparison in html
  public SetQuantityType = SetQuantityType;

  workout = { items: [], totalCount: 0 } as PagedResultDto<WorkoutDto>;

  selectedWorkout = {} as WorkoutDto; // declare selectedWorkout

  form: FormGroup;
  sectionsForm: FormGroup;
  setForm: FormGroup; 
  
  isModalOpen = false;

  constructor(public readonly list: ListService,
    private workoutService: WorkoutService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private loading: LoadingService) {}

  ngOnInit() {
    const workoutStreamCreator = (query) => this.workoutService.getList(query);

    this.list.hookToQuery(workoutStreamCreator).subscribe((response) => {
      this.workout = response;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      name: [this.selectedWorkout.name || '', Validators.required],
      workoutSections: this.fb.array([], this.workoutSectionsConstraint()), 
    });
    this.sectionsForm = this.fb.group({
      title: [ '', Validators.required ],
      sections: this.fb.array([]),
    });
    this.setForm = this.fb.group({
      unit: [ 0, Validators.required ],
      unitType: [ SetUnitType.Distance, Validators.required ],
      quantity: [ '', Validators.required ],
      quantityType: [ SetQuantityType.Time, Validators.required ],
      rest: [ null ]
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
    return (fa: FormArray) => {
      return fa && fa.length >= min ? null : { minLengthArray: { valid: false } };
    };
  }

  createWorkout() {
    this.selectedWorkout = {} as WorkoutDto; // reset the selected Workout
    this.buildForm();
    this.isModalOpen = true;
  }

  editWorkout(id: string) {
    this.loading.setLoading(true);
    this.workoutService.get(id).subscribe((Workout) => {
      this.selectedWorkout = Workout;
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

  onSpace(event: KeyboardEvent) {
    event.stopPropagation();      // prevent accordion toggle
    event.preventDefault();       // prevent scrolling
    const input = event.target as HTMLInputElement;

    // Example: insert a space at the cursor position
    const start = input.selectionStart || 0;
    const end = input.selectionEnd || 0;
    input.value = input.value.slice(0, start) + ' ' + input.value.slice(end);
    input.setSelectionRange(start + 1, start + 1);

    // If using reactive forms, update the FormControl value
    const control = this.form.get('workoutSections').get((input as any).dataset.index.toString()).get('title');
    control?.setValue(input.value, { emitEvent: false });
  }


  // Workout Builder Itself
  get workoutSections(): FormArray {
    return this.form.get('workoutSections') as FormArray;
  }

  addSection() {
    this.workoutSections.push(
      this.fb.group({
        title: ['', Validators.required],
        colour: ['#cccccc'],
        workoutExercises: this.fb.array([]),
        workoutId: null
      })
    );
  }

  removeSection(i: number) {
    this.workoutSections.removeAt(i);
  }


  getExercises(sectionIndex: number): FormArray {
    return this.workoutSections.at(sectionIndex).get('workoutExercises') as FormArray;
  }

  addExercise(sectionIndex: number) {
    this.getExercises(sectionIndex).push(
    this.fb.group({
        name: ['Exercise' + this.getExercises(sectionIndex).length, Validators.required],
        exerciseId: ['xyz', Validators.required],
        sets: this.fb.array([]),
        workoutSectionId: this.workoutSections.at(sectionIndex).get('id')
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
        unit: [0, Validators.required],
        unitType: [SetUnitType.Distance, Validators.required],
        quantity: ['', Validators.required],
        quantityType: [SetQuantityType.Time, Validators.required],
        rest: [''],
      })
    );
  }

  removeSet(sectionIndex: number, exerciseIndex: number, setIndex: number) {
    this.getSets(sectionIndex, exerciseIndex).removeAt(setIndex);
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    
    console.log(this.selectedWorkout);
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
