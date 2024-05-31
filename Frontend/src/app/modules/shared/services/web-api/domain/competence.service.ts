import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateCompetenceRequestModel } from '../../../models/wep-api/domain/competencies/createCompetenceRequestModel';
import { UpdateCompetenceRequestModel } from '../../../models/wep-api/domain/competencies/updateCompetenceRequestModel';

@Injectable({
  providedIn: 'root'
})
export class CompetenceService {
  private apiUrl = environment.apiUrl + 'Competence/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateCompetenceRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }

  public update(model: UpdateCompetenceRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }

  public delete(id: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', { params: { id } });
  }
}
