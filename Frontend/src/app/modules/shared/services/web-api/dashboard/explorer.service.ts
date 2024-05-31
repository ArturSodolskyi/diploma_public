import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TaskViewModel } from '../../../models/wep-api/dashboard/explorers/taskViewModel';
import { TreeViewModel } from '../../../models/wep-api/dashboard/explorers/treeViewModel';
import { CompetenceViewModel } from '../../../models/wep-api/dashboard/explorers/competenceViewModel';

@Injectable({
  providedIn: 'root'
})
export class ExplorerService {
  private apiUrl = environment.apiUrl + 'Dashboard/Explorer/';

  constructor(private httpClient: HttpClient) { }

  public getTree(): Observable<TreeViewModel> {
    return this.httpClient.get<TreeViewModel>(this.apiUrl + 'GetTree');
  }

  public getCompetencies(jobId: number): Observable<CompetenceViewModel[]> {
    return this.httpClient.get<CompetenceViewModel[]>(this.apiUrl + 'GetCompetencies', { params: { jobId } });
  }

  public getTasks(competenceId: number): Observable<TaskViewModel[]> {
    return this.httpClient.get<TaskViewModel[]>(this.apiUrl + 'GetTasks', { params: { competenceId } });
  }
}
