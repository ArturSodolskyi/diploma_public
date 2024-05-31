import { NgIf } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { DataService } from '../../services/data.service';
import { SubmitComponent } from './components/submit/submit.component';
import { ViewComponent } from './components/view/view.component';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss'],
  standalone: true,
  imports: [NgIf, SubmitComponent, ViewComponent]
})
export class ResultsComponent implements OnInit {
  protected showSubmit = false;
  constructor(private dataService: DataService,
    private userService: UserService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.dataService.reviewId$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => this.setShowSubmit());

    this.dataService.onReviewCompletion$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => this.setShowSubmit());
  }

  private setShowSubmit(): void {
    this.showSubmit = this.dataService.review != undefined
      && this.dataService.review.reviewerId == this.userService.userId
      && this.dataService.review.endDate == undefined;
  }
}
