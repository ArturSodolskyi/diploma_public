import { Component, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CompleteReviewRequestModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/completeReviewRequestModel';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-submit',
  templateUrl: './submit.component.html',
  styleUrls: ['./submit.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule]
})
export class SubmitComponent {
  protected comment = new FormControl<string>('', {
    nonNullable: true,
    validators: [Validators.required]
  });

  constructor(private reviewsService: ReviewsService,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  protected submit(): void {
    const model: CompleteReviewRequestModel = {
      reviewId: this.dataService.reviewId!,
      comment: this.comment.value
    };
    this.reviewsService.completeReview(model)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe();
  }
}
