import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompleteReviewRequestModel } from '../../../models/wep-api/dashboard/reviews/completeReviewRequestModel';
import { ReviewResultViewModel } from '../../../models/wep-api/dashboard/reviews/reviewResultViewModel';
import { ReviewViewModel } from '../../../models/wep-api/dashboard/reviews/reviewViewModel';
import { TaskViewModel } from '../../../models/wep-api/dashboard/reviews/taskViewModel';
import { ReviewDetailsViewModel } from '../../../models/wep-api/dashboard/reviews/reviewDetailsViewModel';
import { CompetenceViewModel } from '../../../models/wep-api/dashboard/reviews/competenceViewModel';

@Injectable({
  providedIn: 'root'
})
export class ReviewsService {
  private apiUrl = environment.apiUrl + 'Dashboard/Reviews/';

  public readonly onCompleteReview$ = new Subject<CompleteReviewRequestModel>();

  constructor(private httpClient: HttpClient) { }

  public get(): Observable<ReviewViewModel[]> {
    return this.httpClient.get<ReviewViewModel[]>(this.apiUrl + 'Get');
  }

  public getDetails(reviewId: number): Observable<ReviewDetailsViewModel> {
    return this.httpClient.get<ReviewDetailsViewModel>(this.apiUrl + 'GetDetails', {
      params: { reviewId }
    });
  }

  public getCompetencies(reviewId: number): Observable<CompetenceViewModel[]> {
    return this.httpClient.get<CompetenceViewModel[]>(this.apiUrl + 'GetCompetencies', { params: { reviewId } });
  }

  public getTasks(reviewId: number, competenceId: number): Observable<TaskViewModel[]> {
    return this.httpClient.get<TaskViewModel[]>(this.apiUrl + 'GetTasks', {
      params: { reviewId, competenceId }
    });
  }

  public getReviewResult(reviewId: number): Observable<ReviewResultViewModel> {
    return this.httpClient.get<ReviewResultViewModel>(this.apiUrl + 'GetReviewResult', {
      params: { reviewId }
    });
  }

  public completeReview(model: CompleteReviewRequestModel) {
    return this.httpClient.put(this.apiUrl + 'CompleteReview', model)
      .pipe(tap(_ => this.onCompleteReview$.next(model)));
  }
}
