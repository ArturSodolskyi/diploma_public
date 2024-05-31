import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateReviewRequestModel } from '../../../models/wep-api/domain/reviews/createReviewRequestModel';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private apiUrl = environment.apiUrl + 'Review/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateReviewRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }
}
