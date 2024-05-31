import { NgFor } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSliderModule } from '@angular/material/slider';
import { ActivatedRoute } from '@angular/router';
import { Subject, debounceTime, switchMap, take } from 'rxjs';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { TaskViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/taskViewModel';
import { UpdateReviewTaskRequestModel } from 'src/app/modules/shared/models/wep-api/domain/review-tasks/updateReviewTaskRequestModel';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';
import { ReviewTaskService } from 'src/app/modules/shared/services/web-api/domain/review-task.service';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  standalone: true,
  imports: [MatExpansionModule, NgFor, MatSliderModule, FormsModule, MatCardModule]
})
export class TasksComponent implements OnInit {
  private competenceId!: number;
  protected disableSliders = true;

  protected elements: TaskViewModel[] = [];
  protected updateValue$ = new Subject<TaskViewModel>();

  constructor(private reviewsService: ReviewsService,
    private reviewTaskService: ReviewTaskService,
    private userService: UserService,
    private dataService: DataService,
    private route: ActivatedRoute,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.route.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(params => {
        this.competenceId = params[PARAMS_MAP.Id];
        this.loadData();
      });

    //TODO: refactor
    this.dataService.reviews$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => {
        if (!this.dataService.reviewId || !this.dataService.review) {
          return;
        }

        this.loadData();
        this.setDisableSliders();
      });

    this.dataService.onReviewCompletion$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => this.setDisableSliders());

    this.updateValue$
      .pipe(
        debounceTime(500),
        switchMap(x => this.getUpdateValueRequest(x)),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe();
  }

  private loadData(): void {
    if (!this.dataService.reviewId || !this.competenceId) {
      return;
    }

    this.reviewsService.getTasks(this.dataService.reviewId, this.competenceId)
      .pipe(take(1))
      .subscribe(x => this.elements = x);
  }

  private setDisableSliders(): void {
    this.disableSliders = this.dataService.review?.endDate != undefined
      || this.dataService.review?.reviewerId != this.userService.userId;
  }

  protected getUpdateValueRequest(element: TaskViewModel) {
    const model: UpdateReviewTaskRequestModel = {
      reviewId: this.dataService.review!.id,
      taskId: element.id,
      value: element.value
    };
    return this.reviewTaskService.update(model);
  }
}
