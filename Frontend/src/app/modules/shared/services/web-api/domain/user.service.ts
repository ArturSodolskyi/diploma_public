import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserViewModel } from '../../../models/wep-api/domain/users/userViewModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl + 'User/';

  public readonly onCompanyUpdate$ = new Subject<void>();

  constructor(private httpClient: HttpClient) { }

  public get(): Observable<UserViewModel> {
    return this.httpClient.get<UserViewModel>(this.apiUrl + 'Get');
  }

  public updateCompany(companyId: number) {
    return this.httpClient.put(this.apiUrl + 'UpdateCompany', companyId)
      .pipe(tap(_ => this.onCompanyUpdate$.next()));
  }

  public delete() {
    return this.httpClient.delete(this.apiUrl + 'Delete');
  }
}
