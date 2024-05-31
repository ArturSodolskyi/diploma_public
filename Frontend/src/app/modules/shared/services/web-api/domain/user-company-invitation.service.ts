import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CreateUserCompanyInvitationRequestModel } from '../../../models/wep-api/domain/user-company-invitations/createUserCompanyInvitationRequestModel';

@Injectable({
  providedIn: 'root'
})
export class UserCompanyInvitationService {
  private apiUrl = environment.apiUrl + 'UserCompanyInvitation/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateUserCompanyInvitationRequestModel) {
    return this.httpClient.post(this.apiUrl + 'Create', model);
  }
}
