import { DecimalPipe, NgIf } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter, switchMap } from 'rxjs';
import { ReviewResultViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/reviewResultViewModel';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss'],
  standalone: true,
  imports: [NgIf, DecimalPipe]
})
export class ViewComponent implements OnInit {
  protected isInProcess = true;
  protected result: ReviewResultViewModel | undefined;

  constructor(private reviewsService: ReviewsService,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.dataService.reviewId$
      .pipe(
        filter(x => x != undefined && this.dataService.review!.endDate != undefined),
        switchMap(x => this.reviewsService.getReviewResult(x!)),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => {
        this.result = x;
        this.isInProcess = false;
      });
  }
}
