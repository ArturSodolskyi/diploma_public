import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateCompanyRequestModel } from '../../../models/wep-api/domain/companies/createCompanyRequestModel';
import { UpdateCompanyRequestModel } from '../../../models/wep-api/domain/companies/updateCompanyRequestModel';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private apiUrl = environment.apiUrl + 'Company/';

  public readonly onDelete$ = new Subject<number>();

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateCompanyRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }

  public update(model: UpdateCompanyRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }

  public delete(id: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', { params: { id } })
      .pipe(tap(_ => this.onDelete$.next(id)));
  }
}
