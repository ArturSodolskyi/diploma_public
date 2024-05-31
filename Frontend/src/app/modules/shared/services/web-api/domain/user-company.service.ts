import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UpdateRoleRequestModel } from '../../../models/wep-api/domain/user-companies/updateRoleRequestModel';

@Injectable({
  providedIn: 'root'
})
export class UserCompanyService {
  private apiUrl = environment.apiUrl + 'UserCompany/';

  constructor(private httpClient: HttpClient) { }

  public updateRole(model: UpdateRoleRequestModel) {
    return this.httpClient.put(this.apiUrl + 'UpdateRole', model);
  }

  public delete(userId: number, companyId: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', {
      params: { userId, companyId }
    });
  }
}
