import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateJobRequestModel } from '../../../models/wep-api/domain/jobs/createJobRequestModel';
import { UpdateJobRequestModel } from '../../../models/wep-api/domain/jobs/updateJobRequestModel';


@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiUrl = environment.apiUrl + 'Job/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateJobRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }

  public update(model: UpdateJobRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }

  public delete(id: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', { params: { id } });
  }
}
