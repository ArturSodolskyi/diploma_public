import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateTaskRequestModel } from '../../../models/wep-api/domain/tasks/createTaskRequestModel';
import { UpdateTaskRequestModel } from '../../../models/wep-api/domain/tasks/updateTaskRequestModel';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = environment.apiUrl + 'Task/';

  constructor(private httpClient: HttpClient) { }

  public create(model: CreateTaskRequestModel): Observable<number> {
    return this.httpClient.post<number>(this.apiUrl + 'Create', model);
  }

  public update(model: UpdateTaskRequestModel) {
    return this.httpClient.put(this.apiUrl + 'Update', model);
  }

  public delete(id: number) {
    return this.httpClient.delete(this.apiUrl + 'Delete', { params: { id } });
  }
}
