import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UpdateReviewTaskRequestModel } from '../../../models/wep-api/domain/review-tasks/updateReviewTaskRequestModel';

@Injectable({
  providedIn: 'root'
})
export class ReviewTaskService {
  private apiUrl = environment.apiUrl + 'ReviewTask/';

  constructor(private httpClient: HttpClient) { }

  public update(model: UpdateReviewTaskRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }
}
