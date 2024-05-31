import { NgIf } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { filter, switchMap } from 'rxjs';
import { ReviewDetailsViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/reviewDetailsViewModel';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  standalone: true,
  imports: [NgIf]
})
export class DetailsComponent implements OnInit {
  protected data: ReviewDetailsViewModel | undefined;

  constructor(private dataService: DataService,
    private reviewsService: ReviewsService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.dataService.reviewId$
      .pipe(
        filter(x => x != undefined),
        switchMap(x => this.reviewsService.getDetails(x!)),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => this.data = x);
  }
}
