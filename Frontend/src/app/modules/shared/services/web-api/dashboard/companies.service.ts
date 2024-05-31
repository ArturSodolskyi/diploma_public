import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CompanyViewModel } from '../../../models/wep-api/dashboard/companies/companyViewModel';
import { CompanyInvitationViewModel } from '../../../models/wep-api/dashboard/companies/companyInvitationViewModel';
import { RespondInvitationRequestModel } from '../../../models/wep-api/dashboard/companies/respondInvitationRequestModel';
import { UserViewModel } from '../../../models/wep-api/dashboard/companies/userViewModel';
import { GetReviewersRequestModel } from '../../../models/wep-api/dashboard/companies/getReviewersRequestModel';
import { GetJobsRequestModel } from '../../../models/wep-api/dashboard/companies/getJobsRequestModel';
import { JobViewModel } from '../../../models/wep-api/dashboard/companies/jobViewModel';
import { ReviewerViewModel } from '../../../models/wep-api/dashboard/companies/reviewerViewModel';
import { UserReviewViewModel } from '../../../models/wep-api/dashboard/companies/userReviewViewModel';

@Injectable({
  providedIn: 'root'
})
export class CompaniesService {
  private apiUrl = environment.apiUrl + 'Dashboard/Companies/';

  public readonly onRespondInvitation$ = new Subject<void>();

  constructor(private httpClient: HttpClient) { }

  public get(): Observable<CompanyViewModel[]> {
    return this.httpClient.get<CompanyViewModel[]>(this.apiUrl + 'Get');
  }

  public getInvitations(): Observable<CompanyInvitationViewModel[]> {
    return this.httpClient.get<CompanyInvitationViewModel[]>(this.apiUrl + 'GetInvitations');
  }

  public getUsers(companyId: number): Observable<UserViewModel[]> {
    return this.httpClient.get<UserViewModel[]>(this.apiUrl + 'GetUsers', {
      params: { companyId }
    });
  }

  public getJobs(model: GetJobsRequestModel): Observable<JobViewModel[]> {
    return this.httpClient.post<JobViewModel[]>(this.apiUrl + 'GetJobs', model);
  }

  public getReviewers(model: GetReviewersRequestModel): Observable<ReviewerViewModel[]> {
    return this.httpClient.post<ReviewerViewModel[]>(this.apiUrl + 'getReviewers', model);
  }

  public getUserReviews(userId: number, companyId: number): Observable<UserReviewViewModel[]> {
    return this.httpClient.get<UserReviewViewModel[]>(this.apiUrl + 'GetUserReviews', {
      params: { userId, companyId }
    });
  }

  public respondInvitation(model: RespondInvitationRequestModel) {
    return this.httpClient.post(this.apiUrl + 'RespondInvitation', model)
      .pipe(tap(_ => this.onRespondInvitation$.next()));
  }
}
