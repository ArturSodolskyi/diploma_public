import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { ReviewViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/reviewViewModel';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public readonly reviews$ = new BehaviorSubject<ReviewViewModel[]>([]);
  public readonly reviewId$ = new BehaviorSubject<number | undefined>(undefined);
  public readonly onReviewCompletion$ = new Subject<void>();

  constructor(private reviewsService: ReviewsService) {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.reviewsService.onCompleteReview$
      .subscribe(x => {
        let review = this.reviews$.value.find(y => y.id == x.reviewId);
        if (review) {
          review.endDate = new Date();
        }

        this.onReviewCompletion$.next();
      });
  }

  //TODO: refactor
  public get review() {
    return this.reviews$.value.find(x => x.id == this.reviewId$.value);
  }

  public get reviewId() {
    return this.reviewId$.value;
  }
}
